namespace Desktop.UserControls
{
    partial class MealUC
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
            this.plusPictureBox = new System.Windows.Forms.PictureBox();
            this.mealPictureBox = new System.Windows.Forms.PictureBox();
            this.minusPictureBox = new System.Windows.Forms.PictureBox();
            this.qtyLabel = new MetroFramework.Controls.MetroLabel();
            this.nameLabel = new MetroFramework.Controls.MetroLabel();
            this.priceLabel = new MetroFramework.Controls.MetroLabel();
            ((System.ComponentModel.ISupportInitialize)(this.plusPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mealPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minusPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // plusPictureBox
            // 
            this.plusPictureBox.Image = global::Desktop.Properties.Resources.plus_icon;
            this.plusPictureBox.Location = new System.Drawing.Point(16, 107);
            this.plusPictureBox.Name = "plusPictureBox";
            this.plusPictureBox.Size = new System.Drawing.Size(30, 30);
            this.plusPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.plusPictureBox.TabIndex = 1;
            this.plusPictureBox.TabStop = false;
            this.plusPictureBox.Click += new System.EventHandler(this.plusPictureBox_Click);
            // 
            // mealPictureBox
            // 
            this.mealPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mealPictureBox.Image = global::Desktop.Properties.Resources.burger;
            this.mealPictureBox.Location = new System.Drawing.Point(0, 0);
            this.mealPictureBox.Name = "mealPictureBox";
            this.mealPictureBox.Size = new System.Drawing.Size(150, 150);
            this.mealPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mealPictureBox.TabIndex = 0;
            this.mealPictureBox.TabStop = false;
            this.mealPictureBox.Click += new System.EventHandler(this.mealPictureBox_Click);
            // 
            // minusPictureBox
            // 
            this.minusPictureBox.Image = global::Desktop.Properties.Resources.minus_icon;
            this.minusPictureBox.Location = new System.Drawing.Point(105, 107);
            this.minusPictureBox.Name = "minusPictureBox";
            this.minusPictureBox.Size = new System.Drawing.Size(30, 30);
            this.minusPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.minusPictureBox.TabIndex = 2;
            this.minusPictureBox.TabStop = false;
            this.minusPictureBox.Click += new System.EventHandler(this.minusPictureBox_Click);
            // 
            // qtyLabel
            // 
            this.qtyLabel.AutoSize = true;
            this.qtyLabel.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.qtyLabel.Location = new System.Drawing.Point(65, 110);
            this.qtyLabel.Name = "qtyLabel";
            this.qtyLabel.Size = new System.Drawing.Size(21, 25);
            this.qtyLabel.TabIndex = 3;
            this.qtyLabel.Text = "0";
            this.qtyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.nameLabel.Location = new System.Drawing.Point(25, 14);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(99, 25);
            this.nameLabel.TabIndex = 4;
            this.nameLabel.Text = "Hamburger";
            // 
            // priceLabel
            // 
            this.priceLabel.AutoSize = true;
            this.priceLabel.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.priceLabel.Location = new System.Drawing.Point(55, 66);
            this.priceLabel.Name = "priceLabel";
            this.priceLabel.Size = new System.Drawing.Size(43, 25);
            this.priceLabel.TabIndex = 5;
            this.priceLabel.Text = "5,95";
            this.priceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MealUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mealPictureBox);
            this.Controls.Add(this.priceLabel);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.qtyLabel);
            this.Controls.Add(this.minusPictureBox);
            this.Controls.Add(this.plusPictureBox);
            this.Name = "MealUC";
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MealUC_MouseClick);
            ((System.ComponentModel.ISupportInitialize)(this.plusPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mealPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minusPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.PictureBox mealPictureBox;
        public System.Windows.Forms.PictureBox plusPictureBox;
        public System.Windows.Forms.PictureBox minusPictureBox;
        private MetroFramework.Controls.MetroLabel qtyLabel;
        protected MetroFramework.Controls.MetroLabel nameLabel;
        protected MetroFramework.Controls.MetroLabel priceLabel;
    }
}
