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
    public partial class NotificationsV2UC : UserControl
    {
        public NotificationsV2UC(string title, string subtitle, bool correct)
        {
            InitializeComponent();
            this.title.Text = title;
            this.subtitle.Text = subtitle;
            if (!correct)
            {
                setErrorNotification();
            }
        }

        public void setErrorNotification()
        {
            this.BackColor = Color.FromArgb(217, 83, 79);
            this.pictureBox1.Image = new Bitmap(Properties.Resources.warning);
        }
    }
}
