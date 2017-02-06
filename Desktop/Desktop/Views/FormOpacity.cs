using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop.Views
{
    public partial class FormOpacity : Form
    {
        private Timer timerOpacity;
        
        public FormOpacity(Form formParent, UserControl uControl)
        {
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            BackColor = Color.Black;
            Opacity = 0.05;
            StartPosition = FormStartPosition.Manual;
            Location = formParent.PointToScreen(Point.Empty);
            Size = formParent.Size;
            ClientSize = formParent.ClientSize;
            Dock = DockStyle.Fill;

            timerOpacity = new Timer();
            timerOpacity.Interval = 10;
            timerOpacity.Enabled = true;
            timerOpacity.Tick += TimerOpacity_Tick;
            
            Show();

            Form containerForm = new Form();
            containerForm.ShowInTaskbar = false;
            containerForm.StartPosition = FormStartPosition.CenterParent;
            containerForm.Height = Height;
            containerForm.Width = Width/2;
            containerForm.Location = PointToScreen(new Point(Width/2, 0));
            containerForm.StartPosition = FormStartPosition.Manual;
            containerForm.Controls.Add(uControl);
            containerForm.FormBorderStyle = FormBorderStyle.None;
            containerForm.Show();
        }

        private void FormOpacity_Load(object sender, EventArgs e)
        {
            timerOpacity.Start();
        }

        private void TimerOpacity_Tick(object sender, EventArgs e)
        {
            if (Opacity < 0.6)
            {
                Opacity += 0.01;
            }
            else
            {
                timerOpacity.Stop();
            }
        }
        
    }
}
