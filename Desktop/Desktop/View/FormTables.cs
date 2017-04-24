using Desktop.UserControls;
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

namespace Desktop.View
{
    public partial class FormTables : MetroForm
    {
        public FormOpacity formOpacity;
        private const int QTYTABLES = 12;
        private const int TABLESEPARATION = 40;
        private const int TABLES_PER_ROW = 6;
        private const int QTY_ROWS = 2;

        public  SidebarTable rightPanel;
        public FormTables()
        {
            InitializeComponent();
            InitAllTablesUC();
        }


        private void InitAllTablesUC()
        {
            //TESTING
            //Random rnd = new Random();
            //List<Color> colors = new List<Color>();

            //colors.Add(Color.Red);
            //colors.Add(Color.Green);
            //colors.Add(Color.Blue);
            //FI TESTING

            int antX = 0;
            int antY = 0;


            int halfWidth = this.Width / 2;
            int halfHeight = this.Height / 2;

            for (int i = 1; i <= QTYTABLES; i++)
            {
                TableUC table = new TableUC(i , 1);
                //table.BorderColor = colors[rnd.Next(0, 3)]; // TESTING

                int x = halfWidth - (table.Width * TABLES_PER_ROW / 2 + TABLESEPARATION * TABLES_PER_ROW / 2) + antX + TABLESEPARATION / 2;
                int y = halfHeight - (table.Height * QTY_ROWS / 2 + TABLESEPARATION * QTY_ROWS / 2) + antY + TABLESEPARATION / 2;
                table.Location = new Point(x , y);
                table.Name = "tableUC" + i;
                table.pictureBox1.Click += tableUCClick;
                table.initPosition();
                Controls.Add(table);

                antX += table.Width + TABLESEPARATION;

                if (i % TABLES_PER_ROW == 0)
                {
                    antY += table.Height + TABLESEPARATION;
                    antX = 0;
                }
            }
            
        }

        private void tableUCClick(object sender, EventArgs e)
        {
            PictureBox tablePb = (PictureBox)sender;
            TableUC table = (TableUC)tablePb.Parent;
            rightPanel = new SidebarTable(table);
            formOpacity = new FormOpacity(this, rightPanel);
        }

    }
}
