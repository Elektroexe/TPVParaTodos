using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop.UserControls
{
    public partial class NotificationsUC : UserControl
    {
        private int _notificationsNum;

        public NotificationsUC()
        {
            InitializeComponent();
            initPictureBox();

        }

        private void initPictureBox()
        {
            this.bellPictureBox.Paint += paintPb;
            this.bellPictureBox.Click += picClick;
            _notificationsNum = 8;
           
            Label l = new Label();
            l.Text = (_notificationsNum < 10) ? " " + _notificationsNum.ToString() : _notificationsNum.ToString();
            l.Parent = this.bellPictureBox;
            l.Location = new Point(13,1);
            l.AutoSize = true;
            l.Font = new Font("Arial Black", 9, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            l.ForeColor = Color.White;
            l.BackColor = Color.Transparent;
            this.bellPictureBox.Controls.Add(l);
        }

        private void paintPb(object sender, PaintEventArgs e)
        {
            Brush brush = new SolidBrush(Color.Red);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.FillEllipse(brush, 15, 0, 18,18);

        }

        private void picClick(object sender, EventArgs e)
        {
            //this.BackColor = Color.Gray;
            this.Width *= 10;
            this.Height *= 7;
            this.Location = new Point(this.Location.X, ((Form)this.Parent).Height - this.Height - 39);
            this.pictureBox1.Visible = true;
            this.pictureBox2.Visible = true;
            this.metroLabel1.Visible = true;
            this.BringToFront();
            this.pictureBox2.BringToFront();
        }
    }
}
