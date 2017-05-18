using Desktop.UserControls;
using MetroFramework.Controls;
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

namespace Desktop.View
{
    public partial class FormAddOrder : MetroForm
    {
        public Dictionary<string, Panel> panelControls;
        private string[] panels = { "starter", "main", "dessert", "DrinkDTO", "MenuDTO" };

        public FormAddOrder()
        {
            InitializeComponent();
            panelControls = new Dictionary<string, Panel>();
            createPanels();
        }

        private void createPanels()
        {
            MetroTabControl tab = this.Controls.OfType<MetroTabControl>().FirstOrDefault();
            List<MetroTabPage> tabs = new List<MetroTabPage>();
            foreach (Control c in tab.Controls)
            {
                if(c.GetType().Equals(typeof(MetroTabPage)))
                {
                    tabs.Add((MetroTabPage)c);
                }
            }

            for (int i = 0; i < tabs.Count; i++)
            {
                createPanel(tabs.ElementAt(i), i);
            }
        }

        private void createPanel(MetroTabPage tab, int index)
        {
            Panel pan = new Panel();
            pan.AutoScroll = true;
            pan.Dock = System.Windows.Forms.DockStyle.Fill;
            pan.Location = new System.Drawing.Point(3, 3);
            pan.Name = panels[index] + "Panel";

            panelControls[pan.Name] = pan;
            tab.Controls.Add(pan);

        }
    }
}
