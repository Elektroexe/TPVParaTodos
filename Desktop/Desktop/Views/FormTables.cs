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

namespace Desktop
{
    public partial class FormTables : MetroForm
    {
        private Timer ti;
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
            //UserControls.SidebarTable sidebarTable = new UserControls.SidebarTable();
            //Views.FormOpacity formOpac = new Views.FormOpacity(this, sidebarTable);
            this.rightPanel = new UserControls.SidebarTable(this);
            this.rightPanel.Location = new System.Drawing.Point(this.Width, 5);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.TabIndex = 1;
            this.Controls.Add(this.rightPanel);

            ti = new Timer();
            ti.Interval = 10;
            ti.Tick += Timer_tick;
            ti.Start();

            Transition t = new Transition(new TransitionType_Linear(1000));
            t.add(this.rightPanel, "Left", this.Width - this.rightPanel.Width);
            t.run();

        }

        private void Timer_tick(object sender, EventArgs e)
        {
            if (Opacity < 0.6)
            {
                Opacity += 0.01;
            }
            else
            {
                ti.Stop();
            }
        }


    }
}
