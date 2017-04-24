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
        public NotificationsV2UC(string title, string subtitle)
        {
            InitializeComponent();
            this.label1.Text = title;
            this.label2.Text = subtitle;
        }
    }
}
