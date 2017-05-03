namespace Desktop.View
{
    partial class FormPopUp
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
            this.messageTextLabel = new MetroFramework.Controls.MetroLabel();
            this.acceptBtn = new MetroFramework.Controls.MetroButton();
            this.messageIconPb = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.messageIconPb)).BeginInit();
            this.SuspendLayout();
            // 
            // messageTextLabel
            // 
            this.messageTextLabel.AutoSize = true;
            this.messageTextLabel.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.messageTextLabel.Location = new System.Drawing.Point(135, 74);
            this.messageTextLabel.Name = "messageTextLabel";
            this.messageTextLabel.Size = new System.Drawing.Size(309, 25);
            this.messageTextLabel.TabIndex = 1;
            this.messageTextLabel.Text = "El pedido se ha añadido correctamente";
            // 
            // acceptBtn
            // 
            this.acceptBtn.Location = new System.Drawing.Point(369, 126);
            this.acceptBtn.Name = "acceptBtn";
            this.acceptBtn.Size = new System.Drawing.Size(75, 23);
            this.acceptBtn.TabIndex = 2;
            this.acceptBtn.Text = "Aceptar";
            this.acceptBtn.UseSelectable = true;
            this.acceptBtn.Click += new System.EventHandler(this.acceptBtn_Click);
            // 
            // messageIconPb
            // 
            this.messageIconPb.Image = global::Desktop.Properties.Resources.correct_icon;
            this.messageIconPb.Location = new System.Drawing.Point(39, 48);
            this.messageIconPb.Name = "messageIconPb";
            this.messageIconPb.Size = new System.Drawing.Size(69, 76);
            this.messageIconPb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.messageIconPb.TabIndex = 0;
            this.messageIconPb.TabStop = false;
            // 
            // FormPopUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 172);
            this.Controls.Add(this.acceptBtn);
            this.Controls.Add(this.messageTextLabel);
            this.Controls.Add(this.messageIconPb);
            this.Name = "FormPopUp";
            ((System.ComponentModel.ISupportInitialize)(this.messageIconPb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox messageIconPb;
        private MetroFramework.Controls.MetroLabel messageTextLabel;
        private MetroFramework.Controls.MetroButton acceptBtn;
    }
}