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
    public partial class FormOpacity : Form
    {
        private Form formChild;
        private Form formParent;
        private Timer timerOpacity;
        
        public FormOpacity(Form formParent)
        {
            InitializeComponent();

            this.formParent = formParent;

            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            BackColor = Color.Black;
            Opacity = 0.1;
            StartPosition = FormStartPosition.Manual;
            Location = formParent.PointToScreen(Point.Empty);
            Size = formParent.Size;
            ClientSize = formParent.ClientSize;


            formChild = new Form();
            formChild.Height = this.Height - 5;
            formChild.Width = 300;
            formChild.StartPosition = FormStartPosition.Manual;
            formChild.Location = new Point(Width, 5);
            formChild.FormBorderStyle = FormBorderStyle.None;
            formChild.ShowInTaskbar = false;

            UserControls.SidebarTable rightPanel = new UserControls.SidebarTable();
            formChild.Controls.Add(rightPanel);

            Show();
            formChild.Show();

            Transition t = new Transition(new TransitionType_Linear(400));
            t.add(formChild, "Left",this.Width - formChild.Width + 8);
            t.run();


            //this.Controls.Add(formChild);

            GotFocus += CloseForm;
        }

        private void FormOpacity_Load(object sender, EventArgs e)
        {
            timerOpacity = new Timer();
            timerOpacity.Interval = 1;
            timerOpacity.Tick += TimerOpacity_Tick;
            timerOpacity.Start();
        }

        private void TimerOpacity_Tick(object sender, EventArgs e)
        {
            if (Opacity < 0.6)
            {
                Opacity += 0.02;
            }
            else
            {
                timerOpacity.Stop();
            }
        }


        private void CloseForm(object sender, EventArgs e)
        {
            formChild.Activate();
            timerOpacity.Tick -= TimerOpacity_Tick;
            timerOpacity.Tick += asd2;
            timerOpacity.Start();

            this.asd();
            //this.Close();


        }

        // TODO: REFACTORIZAR
        private void asd()
        {
            Transition t = new Transition(new TransitionType_Linear(400));
            t.add(formChild, "Left", this.Width);
            t.TransitionCompletedEvent += asd3;
            t.run();
        }

        private void asd2(object sender, EventArgs e)
        {
            if (Opacity > 0)
            {
                Opacity -= 0.02;
                formChild.Opacity -= 0.03;
            }
            else
            {
                timerOpacity.Stop();
            }
        }

        private void asd3(object sender, EventArgs e)
        {
            formChild.Close();
            this.Close();
        }

        private void FormOpacity_FormClosed(object sender, FormClosedEventArgs e)
        {
            formParent.Activate();
        }
    }
}
