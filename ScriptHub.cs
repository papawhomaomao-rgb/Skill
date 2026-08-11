using System;
using System.IO;
using System.Reflection.Emit;
using System.Windows.Forms;
using TheUglyExec;

namespace TheUglyExec
{
    public partial class ScriptHub : UserControl
    {
        private Form1 _parent;
        private string _filePath;

        public ScriptHub(Form1 parent)
        {
            InitializeComponent();
            _parent = parent;
        }

        public void SetFile(string filePath)
        {
            _filePath = filePath;
            guna2HtmlLabel1.Text = Path.GetFileNameWithoutExtension(filePath);
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (Parent != null)
            {
                UpdateWidth();
                Parent.SizeChanged += (_, _) => UpdateWidth();
            }
        }

        private void UpdateWidth()
        {
            if (Parent == null) return;

            int margin = 16;
            Width = Parent.ClientSize.Width - Parent.Padding.Horizontal - margin;
            Left = (Parent.ClientSize.Width - Width) / 2;
        }

        private async void LoadBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string content = File.ReadAllText(_filePath);
                string safe = System.Web.HttpUtility.JavaScriptStringEncode(content);
                await _parent.Editor.ExecuteScriptAsync($"editor.setValue('{safe}')");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load script: {ex.Message}", "ERROR");
            }
        }

        private void ScriptHub_Load(object sender, EventArgs e)
        {

        }

        private void Exe2Btn_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    string content = File.ReadAllText(_filePath);
                    _parent.ExecuteScriptOnClients(content);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to get script: {ex.Message}", "ERROR");
                }
            }
        }
    }
}