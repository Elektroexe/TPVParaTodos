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
            //this.Parent = parent;
            initPosition();
        }

        private void initPosition()
        {
            this.Dock = DockStyle.Fill;
            //this.Height = Parent.Height -5;
            //this.Width = 300;
            this.mainPanel.BackColor = Color.FromArgb(222, 225, 226);
            this.mainPanel.Dock = DockStyle.Fill;
        }
    }
}
