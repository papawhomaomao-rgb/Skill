using System.Drawing;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;

namespace TheUglyExec
{
    /// <summary>
    /// Minimal wrapper around a WebView2 control hosting the Monaco editor.
    /// Everything the editor exposes is a plain JS function on the page
    /// (SetTheme, SwitchMinimap, SetFirstTab, etc). This class gives you
    /// one generic RunAsync(js) to call any of them, plus a few named shortcuts.
    /// </summary>
    public class MonacoBridge
    {
        private readonly WebView2 _webView;
        private readonly string _rootFolder; // folder that contains index.html AND vs/
        private readonly string _htmlFile;   // relative path to index.html
        private readonly TaskCompletionSource<bool> _ready = new TaskCompletionSource<bool>();

        /// <param name="webView">Your WebView2 control from the designer.</param>
        /// <param name="rootFolder">Folder containing the html file and the vs/ folder.</param>
        /// <param name="htmlFile">Path to the html file, relative to rootFolder. E.g. "Editor/index.html".</param>
        public MonacoBridge(WebView2 webView, string rootFolder, string htmlFile = "index.html")
        {
            _webView = webView;
            _rootFolder = rootFolder;
            _htmlFile = htmlFile.Replace('\\', '/');
        }

        /// <summary>Call once (e.g. in Form_Load) before using anything else below.</summary>
        public async Task InitializeAsync(bool transparent = false)
        {
            await _webView.EnsureCoreWebView2Async();

            // Disable unnecessary features for faster rendering and lighter resource footprint
            var settings = _webView.CoreWebView2.Settings;
            settings.IsStatusBarEnabled = false;
            settings.AreDefaultContextMenusEnabled = false;

            // Map root folder to virtual host to avoid file:// worker restrictions
            _webView.CoreWebView2.SetVirtualHostNameToFolderMapping(
                "appassets", _rootFolder, CoreWebView2HostResourceAccessKind.Allow);

            if (transparent)
                _webView.DefaultBackgroundColor = Color.Transparent;

            // Hook message handler BEFORE Navigate to ensure no missed events
            _webView.CoreWebView2.WebMessageReceived += (s, e) =>
            {
                if (e.TryGetWebMessageAsString() == "ready")
                    _ready.TrySetResult(true);
            };

            _webView.CoreWebView2.Navigate($"https://appassets/{_htmlFile}");

            await _ready.Task; // Page posts this once editor + tabs exist

            if (transparent)
                await SetTransparent(true);
        }

        /// <summary>Run any JS expression/statement on the page.</summary>
        public Task<string> RunAsync(string javascript) =>
            _webView.CoreWebView2.ExecuteScriptAsync(javascript);

        // --- Everyday Shortcuts ---

        /// <summary>
        /// Clears all saved local storage (tabs, active state, titles) and resets editor.
        /// </summary>
        public Task ResetAppData() =>
            RunAsync("window.ResetAppData();");

        public Task SetTheme(string themeName) =>
            RunAsync($"SetTheme({Q(themeName)});");

        public Task SetMinimap(bool enabled) =>
            RunAsync(enabled ? "SwitchMinimap(true);" : "SwitchMinimap(false);");

        public Task SetTransparent(bool enabled) =>
            RunAsync(enabled ? "window.SetTransparent(true);" : "window.SetTransparent(false);");

        public Task SetFirstTab(string title, string text) =>
            RunAsync($"window.SetFirstTab({Q(title)}, {Q(text)});");

        public Task SetText(string text) =>
            RunAsync($"SetText({Q(text)});"); // active tab, with typing animation

        public Task SetTextInstant(string text) =>
            RunAsync($"window.SetTextInstant({Q(text)});"); // active tab, no animation

        public async Task<string> GetText()
        {
            var json = await RunAsync("GetText();");
            return string.IsNullOrEmpty(json) || json == "null"
                ? string.Empty
                : JsonSerializer.Deserialize<string>(json);
        }

        // --- String Helpers ---
        private static string Q(string s) => JsonSerializer.Serialize(s ?? "");
        private static string B(bool b) => b ? "true" : "false";
    }
}