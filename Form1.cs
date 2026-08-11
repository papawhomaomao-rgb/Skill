using Newtonsoft.Json;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Controls;
using XenoUI;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace TheUglyExec
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ClientsWindow.Initialize(false);
        }

        #region XenoStuff

        [DllImport("Xeno.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        private static extern IntPtr GetClients();

        [DllImport("Xeno.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        private static extern void Execute(byte[] script, int[] PIDs, int count);

        [DllImport("Xeno.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern void Attach();

        private List<int> GetReadyClientPIDs()
        {
            var pids = new List<int>();

            try
            {
                IntPtr clientsPtr = GetClients();
                if (clientsPtr == IntPtr.Zero) return pids;

                string clientsJson = Marshal.PtrToStringAnsi(clientsPtr);
                var clientsList = JsonConvert.DeserializeObject<List<List<object>>>(clientsJson);

                if (clientsList != null)
                {
                    foreach (var client in clientsList)
                    {
                        if (client.Count >= 4)
                        {
                            int pid = Convert.ToInt32(client[0]);
                            int state = Convert.ToInt32(client[3]);

                            if (state == 3) // Ready state
                            {
                                pids.Add(pid);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle or log error
            }

            return pids;
        }

        public void ExecuteScriptOnClients(string script)
        {
            if (string.IsNullOrWhiteSpace(script))
            {
                MessageBox.Show("Script is empty.", "Empty Script", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var clientPIDs = GetReadyClientPIDs();

            if (clientPIDs.Count == 0)
            {
                MessageBox.Show("No ready clients found.\n\nMake sure you've pressed Attach and waited for injection to complete.",
                    "No Ready Clients", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                Execute(Encoding.UTF8.GetBytes(script + "\0"), clientPIDs.ToArray(), clientPIDs.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Script execution failed:\n{ex.Message}", "Execution Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        string activeTab;

        private MonacoBridge _monaco;

        private void SwitchTab(System.Windows.Forms.Panel tab)
        {
            // Hide all panels except the one we want to show
            foreach (var pnl in new[] { HomeTab, ExeTab, ScriptTab, SettingsTab })
            {
                pnl.Visible = (pnl == tab); // Only sets true for selected tab
            }

            tab.BringToFront();
            activeTab = tab.Name;
            CheckTab(); // Update button states based on the active tab)
        }

        private void CheckTab()
        {
            try
            {
                // Unhook event handlers to avoid recursion loop
                HomeBtn.CheckedChanged -= HomeBtn_Click;
                ExeBtn.CheckedChanged -= ExeBtn_Click;
                ScriptHubBtn.CheckedChanged -= ScriptHubBtn_Click;
                SetingsBtn.CheckedChanged -= SetingsBtn_Click;

                if (activeTab == "HomeTab")
                    HomeBtn.Checked = true;
                else if (activeTab == "ExeTab")
                    ExeBtn.Checked = true;
                else if (activeTab == "ScriptTab")
                    ScriptHubBtn.Checked = true;
                else if (activeTab == "SettingsTab")
                    SetingsBtn.Checked = true;
            }
            finally
            {
                // Re-hook event handlers
                HomeBtn.CheckedChanged += HomeBtn_Click;
                ExeBtn.CheckedChanged += ExeBtn_Click;
                ScriptHubBtn.CheckedChanged += ScriptHubBtn_Click;
                SetingsBtn.CheckedChanged += SetingsBtn_Click;
            }
        }

        private void LoadScripts()
        {
            flowLayoutPanel1.Controls.Clear();

            string folder = Path.Combine(AppContext.BaseDirectory, "Scripts");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
                return;
            }

            var files = Directory.GetFiles(folder, "*.*")
                .Where(f => f.EndsWith(".lua") || f.EndsWith(".txt"))
                .ToArray();

            foreach (var file in files)
            {
                var scriptControl = new ScriptHub(this);
                scriptControl.SetFile(file);
                flowLayoutPanel1.Controls.Add(scriptControl);
            }

            if (files.Length == 0)
            {
                ScriptLabel.Text = "SCRIPTS - NONE";
            }
            else
            {
                ScriptLabel.Text = "SCRIPTS - " + files.Length;
            }
        }





        private async void Form1_Load(object sender, EventArgs e)
        {
            SwitchTab(ExeTab); // Show HomeTab by default
            LoadScripts(); // Load scripts into the ScriptHub panel

            // Setup folder paths:
            string rootFolder = AppContext.BaseDirectory;   // App root folder
            string htmlFile = "Editor/index.html";          // AppFolder\Editor\index.html

            _monaco = new MonacoBridge(Editor, rootFolder, htmlFile);
            await _monaco.InitializeAsync(transparent: true);

            // Editor is fully ready here:
            await _monaco.SetTheme("Dark");
            await _monaco.SetMinimap(false);
            await _monaco.SetTransparent(true);

            LoadSettings(); // Load user settings
        }

        private void HomeBtn_Click(object sender, EventArgs e)
        {
            SwitchTab(HomeTab);
        }

        private void ExeBtn_Click(object sender, EventArgs e)
        {
            SwitchTab(ExeTab);
        }

        private void ScriptHubBtn_Click(object sender, EventArgs e)
        {
            SwitchTab(ScriptTab);
        }

        private void SetingsBtn_Click(object sender, EventArgs e)
        {
            SwitchTab(SettingsTab);
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MiniBtn_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private async void ClearBtn_Click(object sender, EventArgs e)
        {
            await Editor.ExecuteScriptAsync($"SetText(``);");
        }

        private async void OpnFileBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "All Supported Files (*.lua;*.txt)|*.lua;*.txt|Lua Files (*.lua)|*.lua|Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                dialog.DefaultExt = "lua";
                dialog.FilterIndex = 1;
                dialog.Title = "Open Script File";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string script = File.ReadAllText(dialog.FileName);

                        // Fully qualified name ensures no overload ambiguity
                        string jsonScript = System.Text.Json.JsonSerializer.Serialize(script);

                        await Editor.CoreWebView2.ExecuteScriptAsync($"editor.setValue({jsonScript});");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to open file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private async void SveFileBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog
                {
                    Filter = "Lua Files (*.lua)|*.lua|Text Files (*.txt)|*.txt",
                    DefaultExt = "lua",
                    Title = "Save Lua or Text File"
                };

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string textToSave = await Editor.ExecuteScriptAsync("GetText();");
                    string rawText = JsonConvert.DeserializeObject<string>(textToSave);
                    await Task.Run(() => File.WriteAllText(saveFileDialog1.FileName, rawText));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshBtn_Click(object sender, EventArgs e)
        {
            LoadScripts(); // Reload scripts into the ScriptHub panel
        }

        private void guna2CustomCheckBox1_Click(object sender, EventArgs e)
        {

        }

        private void SettingsTab_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TopMostTog_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = false;
            this.TopMost = TopMostTog.Checked;

            Properties.Settings.Default.TopMost = TopMostTog.Checked;
            Properties.Settings.Default.Save();
        }

        private async void guna2ToggleSwitch2_CheckedChanged(object sender, EventArgs e)
        {
            await _monaco.SetMinimap(guna2ToggleSwitch2.Checked);

            Properties.Settings.Default.MiniMap = guna2ToggleSwitch2.Checked;
            Properties.Settings.Default.Save();
        }

        private void LoadSettings()
        {
            TopMostTog.Checked = Properties.Settings.Default.TopMost;
            guna2ToggleSwitch2.Checked = Properties.Settings.Default.MiniMap;
        }

        private void AttachBtn_Click(object sender, EventArgs e)
        {
            Attach(); // Call the Attach function from Xeno.dll
        }

        private async void ExecuteBtn_Click(object sender, EventArgs e)
        {
            string scriptToExecute = await Editor.ExecuteScriptAsync("GetText();");
            string rawScript = JsonConvert.DeserializeObject<string>(scriptToExecute);
            ExecuteScriptOnClients(rawScript);
        }
    }
}
