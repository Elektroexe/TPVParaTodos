using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Desktop.Model;
using MetroFramework.Controls;

namespace Desktop.UserControls
{

    public abstract partial class MealUC : UserControl
    {

        public MealUC()
        {
            InitializeComponent();
            this.Controls.OfType<MetroLabel>().ToList().ForEach(l => l.MouseClick += MealUC_MouseClick);
        }

        private void mealPictureBox_Click(object sender, EventArgs e)
        {
            this.mealPictureBox.Visible = false;
        }

        private void plusPictureBox_Click(object sender, EventArgs e)
        {
            this.qtyLabel.Text = AddToOrder().Quantity.ToString();
        }

        private void minusPictureBox_Click(object sender, EventArgs e)
        {
            this.qtyLabel.Text = DelFromOrder().Quantity.ToString();
        }

        private void MealUC_MouseClick(object sender, MouseEventArgs e)
        {
            this.mealPictureBox.Visible = true;
        }

        public abstract Product AddToOrder();

        public abstract Product DelFromOrder();

        public abstract void SetDTO(Product product);

        protected void RepositionLabels()
        {
            if (this.nameLabel.Width > this.Width)
            {
                string firstText = this.nameLabel.Text.Substring(0, this.nameLabel.Text.Length / 2);
                string secondText = this.nameLabel.Text.Substring(this.nameLabel.Text.Length / 2);
                firstText += secondText.Substring(0, secondText.IndexOf(" ")) + "\n";
                firstText += secondText.Substring(secondText.IndexOf(" "));
                this.nameLabel.Text = firstText;
            }
            int newX = this.Width / 2 - this.nameLabel.Width / 2;
            this.nameLabel.Location = new Point(newX, this.nameLabel.Location.Y);

            newX = this.Width / 2 - this.priceLabel.Width / 2;
            this.priceLabel.Location = new Point(newX, this.priceLabel.Location.Y);
        }

        public void toGray()
        {
            Bitmap b = this.mealPictureBox.Image as Bitmap;
            for (int i = 0; i < b.Width; i++)
            {
                for (int j = 0; j < b.Height; j++)
                {
                    Color c = b.GetPixel(i, j);

                    //Apply conversion equation
                    byte gray = (byte)(.21 * c.R + .71 * c.G + .071 * c.B);

                    //Set the color of this pixel
                    b.SetPixel(i, j, Color.FromArgb(gray, gray, gray));
                }
            }
            this.mealPictureBox.Click -= mealPictureBox_Click;
            this.mealPictureBox.Image = b;
        }

    }
}