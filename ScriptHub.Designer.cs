namespace TheUglyExec
{
    partial class ScriptHub
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(components);
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            LoadBtn = new CuoreUI.Controls.cuiButton();
            Exe2Btn = new CuoreUI.Controls.cuiButton();
            panel1 = new Panel();
            panel2 = new Panel();
            SuspendLayout();
            // 
            // guna2Elipse1
            // 
            guna2Elipse1.BorderRadius = 3;
            guna2Elipse1.TargetControl = this;
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.Anchor = AnchorStyles.Left;
            guna2HtmlLabel1.AutoSize = false;
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            guna2HtmlLabel1.ForeColor = Color.Gainsboro;
            guna2HtmlLabel1.Location = new Point(15, 15);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(329, 31);
            guna2HtmlLabel1.TabIndex = 0;
            guna2HtmlLabel1.Text = "ScriptName.lua";
            // 
            // LoadBtn
            // 
            LoadBtn.Anchor = AnchorStyles.Right;
            LoadBtn.CheckButton = false;
            LoadBtn.Checked = false;
            LoadBtn.CheckedBackground = Color.FromArgb(255, 106, 0);
            LoadBtn.CheckedForeColor = Color.White;
            LoadBtn.CheckedImageTint = Color.White;
            LoadBtn.CheckedOutline = Color.FromArgb(255, 106, 0);
            LoadBtn.Content = "Load";
            LoadBtn.DialogResult = DialogResult.None;
            LoadBtn.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            LoadBtn.ForeColor = Color.Gainsboro;
            LoadBtn.HoverBackground = Color.White;
            LoadBtn.HoverForeColor = Color.DimGray;
            LoadBtn.HoverImageTint = Color.DimGray;
            LoadBtn.HoverOutline = Color.FromArgb(32, 128, 128, 128);
            LoadBtn.Image = null;
            LoadBtn.ImageExpand = new Point(0, 0);
            LoadBtn.Location = new Point(447, 15);
            LoadBtn.Name = "LoadBtn";
            LoadBtn.NormalBackground = Color.FromArgb(30, 30, 30);
            LoadBtn.NormalForeColor = Color.Gainsboro;
            LoadBtn.NormalImageTint = Color.Black;
            LoadBtn.NormalOutline = Color.FromArgb(60, 60, 60);
            LoadBtn.OutlineThickness = 1F;
            LoadBtn.Padding = new Padding(12);
            LoadBtn.PressedBackground = Color.WhiteSmoke;
            LoadBtn.PressedForeColor = Color.FromArgb(32, 32, 32);
            LoadBtn.PressedImageTint = Color.FromArgb(32, 32, 32);
            LoadBtn.PressedOutline = Color.FromArgb(64, 128, 128, 128);
            LoadBtn.Rounding = new Padding(5);
            LoadBtn.Size = new Size(91, 31);
            LoadBtn.TabIndex = 2;
            LoadBtn.TextAlignment = StringAlignment.Center;
            LoadBtn.TextPadding = 12;
            LoadBtn.TextSpacing = 2;
            LoadBtn.Click += LoadBtn_Click;
            // 
            // Exe2Btn
            // 
            Exe2Btn.Anchor = AnchorStyles.Right;
            Exe2Btn.CheckButton = false;
            Exe2Btn.Checked = false;
            Exe2Btn.CheckedBackground = Color.FromArgb(255, 106, 0);
            Exe2Btn.CheckedForeColor = Color.White;
            Exe2Btn.CheckedImageTint = Color.White;
            Exe2Btn.CheckedOutline = Color.FromArgb(255, 106, 0);
            Exe2Btn.Content = "Execute";
            Exe2Btn.DialogResult = DialogResult.None;
            Exe2Btn.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Exe2Btn.ForeColor = Color.Gainsboro;
            Exe2Btn.HoverBackground = Color.White;
            Exe2Btn.HoverForeColor = Color.DimGray;
            Exe2Btn.HoverImageTint = Color.DimGray;
            Exe2Btn.HoverOutline = Color.FromArgb(32, 128, 128, 128);
            Exe2Btn.Image = null;
            Exe2Btn.ImageExpand = new Point(0, 0);
            Exe2Btn.Location = new Point(350, 15);
            Exe2Btn.Name = "Exe2Btn";
            Exe2Btn.NormalBackground = Color.FromArgb(30, 30, 30);
            Exe2Btn.NormalForeColor = Color.Gainsboro;
            Exe2Btn.NormalImageTint = Color.Black;
            Exe2Btn.NormalOutline = Color.FromArgb(60, 60, 60);
            Exe2Btn.OutlineThickness = 1F;
            Exe2Btn.Padding = new Padding(12);
            Exe2Btn.PressedBackground = Color.WhiteSmoke;
            Exe2Btn.PressedForeColor = Color.FromArgb(32, 32, 32);
            Exe2Btn.PressedImageTint = Color.FromArgb(32, 32, 32);
            Exe2Btn.PressedOutline = Color.FromArgb(64, 128, 128, 128);
            Exe2Btn.Rounding = new Padding(5);
            Exe2Btn.Size = new Size(91, 31);
            Exe2Btn.TabIndex = 3;
            Exe2Btn.TextAlignment = StringAlignment.Center;
            Exe2Btn.TextPadding = 12;
            Exe2Btn.TextSpacing = 2;
            Exe2Btn.Click += Exe2Btn_Click;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = Color.FromArgb(60, 60, 60);
            panel1.Location = new Point(-1, 59);
            panel1.Name = "panel1";
            panel1.Size = new Size(560, 1);
            panel1.TabIndex = 4;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel2.BackColor = Color.FromArgb(60, 60, 60);
            panel2.Location = new Point(3, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(560, 1);
            panel2.TabIndex = 5;
            // 
            // ScriptHub
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(Exe2Btn);
            Controls.Add(LoadBtn);
            Controls.Add(guna2HtmlLabel1);
            Name = "ScriptHub";
            Size = new Size(552, 61);
            Load += ScriptHub_Load;
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private CuoreUI.Controls.cuiButton LoadBtn;
        private Panel panel2;
        private Panel panel1;
        private CuoreUI.Controls.cuiButton Exe2Btn;
    }
}
