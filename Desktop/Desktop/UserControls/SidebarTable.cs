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
using Desktop.Model;
using Desktop.Controller;

namespace Desktop.UserControls
{
    public partial class SidebarTable : MetroUserControl
    {
        private TableUC table;


        public TableUC Table{
            get
            {
                return this.table;
            }
            set
            {
                this.table = value;
            }
        }
        public SidebarTable(TableUC table) // pasar width y height
        {
            InitializeComponent();
            //this.table = table;
            this.table = new TableUC(table.TableNumber, 2);
            this.table.Table = table.Table;
            this.table.BackColor = SystemColors.Control;
            this.table.BorderColor = table.BorderColor;
            this.table.Location = new Point(0, 0);
            this.table.Size = new Size(table.Size.Width * 2, table.Size.Height * 2);
            this.table.initPosition();
            initPosition();

            this.mainPanel.Controls.Add(this.table);
        }

        private void initPosition()
        {
            this.Dock = DockStyle.Fill;
            this.mainPanel.Dock = DockStyle.Fill;
            //this.mainPanel.Paint += paintPanel;
            //this.pictureBox1.Size = new Size(200,229);
            //this.pictureBox1.Location = new Point(50, 50);
            //this.pictureBox1.Paint += paintPb;

            String[] buttons = new String[] { "Añadir comanda", "Ver comanda", "Modificar comanda", "Cerrar comanda" };

            int ant = 300;
            for (int i = 0; i < 4; i++)
            {

                MetroButton btn = new MetroButton();
                btn.Location = new Point(15, ant);
                btn.Size = new Size(270, 45);
                btn.Name = "btn" + i;
                btn.Text = buttons[i];
                btn.Click += clickButton;
                this.mainPanel.Controls.Add(btn);
                ant += 60;
            }

            MetroCheckBox chck = new MetroCheckBox();
            chck.Location = new Point(15, ant);
            chck.Text = "Ocupada";
            chck.Checked = !table.Table.Empty;
            chck.CheckedChanged += CheckBoxChange;
            this.mainPanel.Controls.Add(chck);

            //Label l = new Label();
            //l.Text = "1";
            //l.Parent = pictureBox1;
            //l.Location = new Point(88, 185);
            //l.AutoSize = true;
            //l.Font = new Font("Arial Black", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //pictureBox1.Controls.Add(l);
        }

        private void clickButton(object sender, EventArgs e)
        {
            AddOrderController a = new AddOrderController(table.TableNumber);
        }

        private void CheckBoxChange(object sender, EventArgs e)
        {
            MetroCheckBox chck = (MetroCheckBox)sender;
            TableDTO t = table.Table;
            //t.Empty = !chck.Checked;
            TablesController.modifyTableStatus(t.Id);
        }

        //private void paintPanel(object sender, PaintEventArgs e)
        //{
        //    Brush brush = new SolidBrush(Color.Red);
        //    Pen pen = new Pen(Color.Black, 5);

        //    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        //    e.Graphics.DrawEllipse(pen, new Rectangle(40, 40, 220, 220));
        //    e.Graphics.FillEllipse(brush, new Rectangle(40, 40, 220, 220));


        //}

        //private void paintPb(object sender, PaintEventArgs e)
        //{
        //    Brush brush = new SolidBrush(Color.White);
        //    Pen pen = new Pen(Color.Black, 5);

        //    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        //    e.Graphics.DrawEllipse(pen, new Rectangle(77, 175, 50, 50));
        //    e.Graphics.FillEllipse(brush, new Rectangle(77, 175, 50, 50));

        //}
    }
}
