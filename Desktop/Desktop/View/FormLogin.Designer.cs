namespace Desktop.View
{
    partial class FormLogin
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLogin));
            this.usernameTxtbox = new MetroFramework.Controls.MetroTextBox();
            this.passwordTxtbox = new MetroFramework.Controls.MetroTextBox();
            this.loginBtn = new MetroFramework.Controls.MetroButton();
            this.userLabel = new MetroFramework.Controls.MetroLabel();
            this.passwordLabel = new MetroFramework.Controls.MetroLabel();
            this.SuspendLayout();
            // 
            // usernameTxtbox
            // 
            // 
            // 
            // 
            this.usernameTxtbox.CustomButton.Image = null;
            this.usernameTxtbox.CustomButton.Location = new System.Drawing.Point(240, 2);
            this.usernameTxtbox.CustomButton.Name = "";
            this.usernameTxtbox.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.usernameTxtbox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.usernameTxtbox.CustomButton.TabIndex = 1;
            this.usernameTxtbox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.usernameTxtbox.CustomButton.UseSelectable = true;
            this.usernameTxtbox.CustomButton.Visible = false;
            this.usernameTxtbox.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.usernameTxtbox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.usernameTxtbox.Lines = new string[0];
            this.usernameTxtbox.Location = new System.Drawing.Point(193, 96);
            this.usernameTxtbox.MaxLength = 32767;
            this.usernameTxtbox.Name = "usernameTxtbox";
            this.usernameTxtbox.PasswordChar = '\0';
            this.usernameTxtbox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.usernameTxtbox.SelectedText = "";
            this.usernameTxtbox.SelectionLength = 0;
            this.usernameTxtbox.SelectionStart = 0;
            this.usernameTxtbox.ShortcutsEnabled = true;
            this.usernameTxtbox.Size = new System.Drawing.Size(268, 30);
            this.usernameTxtbox.TabIndex = 0;
            this.usernameTxtbox.UseSelectable = true;
            this.usernameTxtbox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.usernameTxtbox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // passwordTxtbox
            // 
            // 
            // 
            // 
            this.passwordTxtbox.CustomButton.Image = null;
            this.passwordTxtbox.CustomButton.Location = new System.Drawing.Point(240, 2);
            this.passwordTxtbox.CustomButton.Name = "";
            this.passwordTxtbox.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.passwordTxtbox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.passwordTxtbox.CustomButton.TabIndex = 1;
            this.passwordTxtbox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.passwordTxtbox.CustomButton.UseSelectable = true;
            this.passwordTxtbox.CustomButton.Visible = false;
            this.passwordTxtbox.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.passwordTxtbox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.passwordTxtbox.Lines = new string[0];
            this.passwordTxtbox.Location = new System.Drawing.Point(193, 158);
            this.passwordTxtbox.MaxLength = 14;
            this.passwordTxtbox.Name = "passwordTxtbox";
            this.passwordTxtbox.PasswordChar = '*';
            this.passwordTxtbox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.passwordTxtbox.SelectedText = "";
            this.passwordTxtbox.SelectionLength = 0;
            this.passwordTxtbox.SelectionStart = 0;
            this.passwordTxtbox.ShortcutsEnabled = true;
            this.passwordTxtbox.Size = new System.Drawing.Size(268, 30);
            this.passwordTxtbox.TabIndex = 1;
            this.passwordTxtbox.UseSelectable = true;
            this.passwordTxtbox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.passwordTxtbox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // loginBtn
            // 
            this.loginBtn.Enabled = false;
            this.loginBtn.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.loginBtn.Location = new System.Drawing.Point(191, 274);
            this.loginBtn.Name = "loginBtn";
            this.loginBtn.Size = new System.Drawing.Size(149, 40);
            this.loginBtn.TabIndex = 2;
            this.loginBtn.Text = "Iniciar sesión";
            this.loginBtn.UseSelectable = true;
            // 
            // userLabel
            // 
            this.userLabel.AutoSize = true;
            this.userLabel.Location = new System.Drawing.Point(90, 103);
            this.userLabel.Name = "userLabel";
            this.userLabel.Size = new System.Drawing.Size(56, 19);
            this.userLabel.TabIndex = 4;
            this.userLabel.Text = "Usuario:";
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(90, 162);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(78, 19);
            this.passwordLabel.TabIndex = 5;
            this.passwordLabel.Text = "Contraseña:";
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 380);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.userLabel);
            this.Controls.Add(this.loginBtn);
            this.Controls.Add(this.passwordTxtbox);
            this.Controls.Add(this.usernameTxtbox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormLogin";
            this.Resizable = false;
            this.Text = "Iniciar sesión";
            this.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public MetroFramework.Controls.MetroTextBox usernameTxtbox;
        public MetroFramework.Controls.MetroTextBox passwordTxtbox;
        public MetroFramework.Controls.MetroButton loginBtn;
        private MetroFramework.Controls.MetroLabel userLabel;
        private MetroFramework.Controls.MetroLabel passwordLabel;
    }
}