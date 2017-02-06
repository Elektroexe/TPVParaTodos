using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop
{
    public partial class FormTables : Form
    {
        public FormTables()
        {
            InitializeComponent();
        }

        private void FormTables_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserControls.SidebarTable sidebarTable = new UserControls.SidebarTable();
            Views.FormOpacity formOpac = new Views.FormOpacity(this, sidebarTable);
        }
    }
}
