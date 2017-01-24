namespace Desktop.UserControls
{
    partial class IconUC
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
            this.pbIconHelp = new System.Windows.Forms.PictureBox();
            this.pbIconNotifications = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbIconHelp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbIconNotifications)).BeginInit();
            this.SuspendLayout();
            // 
            // pbIconHelp
            // 
            this.pbIconHelp.Image = global::Desktop.Properties.Resources.help;
            this.pbIconHelp.Location = new System.Drawing.Point(15, 15);
            this.pbIconHelp.Name = "pbIconHelp";
            this.pbIconHelp.Size = new System.Drawing.Size(40, 40);
            this.pbIconHelp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbIconHelp.TabIndex = 0;
            this.pbIconHelp.TabStop = false;
            // 
            // pbIconNotifications
            // 
            this.pbIconNotifications.Image = global::Desktop.Properties.Resources.bell;
            this.pbIconNotifications.Location = new System.Drawing.Point(15, 675);
            this.pbIconNotifications.Name = "pbIconNotifications";
            this.pbIconNotifications.Size = new System.Drawing.Size(40, 40);
            this.pbIconNotifications.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbIconNotifications.TabIndex = 1;
            this.pbIconNotifications.TabStop = false;
            // 
            // IconUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pbIconNotifications);
            this.Controls.Add(this.pbIconHelp);
            this.Name = "IconUC";
            this.Size = new System.Drawing.Size(1366, 730);
            ((System.ComponentModel.ISupportInitialize)(this.pbIconHelp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbIconNotifications)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbIconHelp;
        private System.Windows.Forms.PictureBox pbIconNotifications;
    }
}
