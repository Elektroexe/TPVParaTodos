using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Transitions;

namespace Desktop.Views
{
    public partial class FormTables : MetroForm
    {
        private UserControls.SidebarTable rightPanel;
        public FormTables()
        {
            InitializeComponent();
        }

        private void FormTables_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormOpacity f1 = new FormOpacity(this);
            //f1.Show();

            //Transition t = new Transition(new TransitionType_Linear(1000));
            //t.add(f1, "Left", this.Width - f1.Width + 8);
            //t.run();

        }

    }
}
