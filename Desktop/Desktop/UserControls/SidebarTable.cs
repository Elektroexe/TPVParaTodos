using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Controls;
using MetroFramework.Forms;

namespace Desktop.UserControls
{
    public partial class SidebarTable : MetroUserControl
    {
        public SidebarTable() // pasar width y height
        {
            InitializeComponent();
            initPosition();
        }

        private void initPosition()
        {
            this.Dock = DockStyle.Fill;
            this.mainPanel.Dock = DockStyle.Fill;
            this.mainPanel.Paint += paintPanel;
            this.pictureBox1.Size = new Size(200,229);
            this.pictureBox1.Location = new Point(50, 50);
            this.pictureBox1.Paint += paintPb;

            int ant = 300;
            for (int i = 0; i < 5; i++)
            {

                MetroButton btn = new MetroButton();
                btn.Location = new Point(10, ant);
                btn.Size = new Size(270, 45);
                btn.Name = "btn" + i;
                btn.Text = "Button " + (i+1);
                this.mainPanel.Controls.Add(btn);
                ant += 60;
            }

            Label l = new Label();
            l.Text = "1";
            l.Parent = pictureBox1;
            l.Location = new Point(88, 185);
            l.AutoSize = true;
            l.Font = new Font("Arial Black", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            pictureBox1.Controls.Add(l);
        }

        private void paintPanel(object sender, PaintEventArgs e)
        {
            Brush brush = new SolidBrush(Color.Red);
            Pen pen = new Pen(Color.Black, 5);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.DrawEllipse(pen, new Rectangle(40, 40, 220, 220));
            e.Graphics.FillEllipse(brush, new Rectangle(40, 40, 220, 220));


        }

        private void paintPb(object sender, PaintEventArgs e)
        {
            Brush brush = new SolidBrush(Color.White);
            Pen pen = new Pen(Color.Black, 5);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.DrawEllipse(pen, new Rectangle(77, 175, 50, 50));
            e.Graphics.FillEllipse(brush, new Rectangle(77, 175, 50, 50));

        }
    }
}
