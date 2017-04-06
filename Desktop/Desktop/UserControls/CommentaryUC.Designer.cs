namespace Desktop.UserControls
{
    partial class CommentaryUC
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
            this.commentaryLabel = new MetroFramework.Controls.MetroLabel();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.commentaryTextBox = new MetroFramework.Controls.MetroTextBox();
            this.contBtn = new MetroFramework.Controls.MetroButton();
            this.skipBtn = new MetroFramework.Controls.MetroButton();
            this.metroPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // commentaryLabel
            // 
            this.commentaryLabel.AutoSize = true;
            this.commentaryLabel.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.commentaryLabel.Location = new System.Drawing.Point(39, 33);
            this.commentaryLabel.Name = "commentaryLabel";
            this.commentaryLabel.Size = new System.Drawing.Size(112, 25);
            this.commentaryLabel.TabIndex = 0;
            this.commentaryLabel.Text = "Comentarios:";
            // 
            // metroPanel1
            // 
            this.metroPanel1.Controls.Add(this.skipBtn);
            this.metroPanel1.Controls.Add(this.contBtn);
            this.metroPanel1.Controls.Add(this.commentaryTextBox);
            this.metroPanel1.Controls.Add(this.commentaryLabel);
            this.metroPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(0, 0);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(649, 452);
            this.metroPanel1.TabIndex = 1;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // commentaryTextBox
            // 
            // 
            // 
            // 
            this.commentaryTextBox.CustomButton.Image = null;
            this.commentaryTextBox.CustomButton.Location = new System.Drawing.Point(269, 2);
            this.commentaryTextBox.CustomButton.Name = "";
            this.commentaryTextBox.CustomButton.Size = new System.Drawing.Size(295, 295);
            this.commentaryTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.commentaryTextBox.CustomButton.TabIndex = 1;
            this.commentaryTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.commentaryTextBox.CustomButton.UseSelectable = true;
            this.commentaryTextBox.CustomButton.Visible = false;
            this.commentaryTextBox.Lines = new string[0];
            this.commentaryTextBox.Location = new System.Drawing.Point(41, 76);
            this.commentaryTextBox.MaxLength = 32767;
            this.commentaryTextBox.Multiline = true;
            this.commentaryTextBox.Name = "commentaryTextBox";
            this.commentaryTextBox.PasswordChar = '\0';
            this.commentaryTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.commentaryTextBox.SelectedText = "";
            this.commentaryTextBox.SelectionLength = 0;
            this.commentaryTextBox.SelectionStart = 0;
            this.commentaryTextBox.ShortcutsEnabled = true;
            this.commentaryTextBox.Size = new System.Drawing.Size(567, 300);
            this.commentaryTextBox.TabIndex = 2;
            this.commentaryTextBox.UseSelectable = true;
            this.commentaryTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.commentaryTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // contBtn
            // 
            this.contBtn.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.contBtn.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.contBtn.Location = new System.Drawing.Point(516, 404);
            this.contBtn.Name = "contBtn";
            this.contBtn.Size = new System.Drawing.Size(92, 23);
            this.contBtn.TabIndex = 3;
            this.contBtn.Text = "Continuar >";
            this.contBtn.UseSelectable = true;
            // 
            // skipBtn
            // 
            this.skipBtn.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.skipBtn.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.skipBtn.Location = new System.Drawing.Point(428, 404);
            this.skipBtn.Name = "skipBtn";
            this.skipBtn.Size = new System.Drawing.Size(82, 23);
            this.skipBtn.TabIndex = 4;
            this.skipBtn.Text = "Saltar";
            this.skipBtn.UseSelectable = true;
            // 
            // CommentaryUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.metroPanel1);
            this.Name = "CommentaryUC";
            this.Size = new System.Drawing.Size(649, 452);
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroLabel commentaryLabel;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        public MetroFramework.Controls.MetroTextBox commentaryTextBox;
        public MetroFramework.Controls.MetroButton contBtn;
        public MetroFramework.Controls.MetroButton skipBtn;
    }
}
