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
using System.Net;
using System.IO;

namespace Desktop.UserControls
{
    public partial class SidebarTable : MetroUserControl
    {

        private readonly String[] buttons = new String[] { "Añadir comanda", "Ver comanda", "Modificar comanda", "Cerrar comanda" };

        private TableUC table;


        public TableUC Table
        {
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

            int ant = 300;
            for (int i = 0; i < 4; i++)
            {
                MetroButton btn = new MetroButton();
                btn.Location = new Point(15, ant);
                btn.Size = new Size(270, 45);
                btn.Name = "btn" + i;
                btn.Text = buttons[i];
                if (i == 0)
                {
                    btn.Click += addOrderButton;
                    if (!table.Table.Empty) btn.Enabled = false;
                }
                else if (i == 2)
                {
                    btn.Click += modifyOrderButton;
                }

                else if (i == 1)
                {
                    btn.Click += viewOrderButton;
                }

                if (i >= 1 && i < 4)
                {
                    if (table.Table.Empty) btn.Enabled = false;
                }
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

        private void addOrderButton(object sender, EventArgs e)
        {
            AddOrderController a = new AddOrderController(table.TableNumber);
        }

        private void modifyOrderButton(object sender, EventArgs e)
        {
            AddOrderController a = new AddOrderController(table.TableNumber, this.getActiveOrder());
        }

        private void viewOrderButton(object sender, EventArgs e)
        {
            this.mainPanel.Controls.OfType<MetroButton>().ToList().ForEach(b => b.Visible = false);

            MetroGrid metroGrid1 = new MetroGrid();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            metroGrid1.AllowUserToAddRows = false;
            metroGrid1.AllowUserToDeleteRows = false;
            metroGrid1.AllowUserToResizeColumns = false;
            metroGrid1.AllowUserToResizeRows = false;
            metroGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            metroGrid1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            metroGrid1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            metroGrid1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            metroGrid1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            metroGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            metroGrid1.DefaultCellStyle = dataGridViewCellStyle2;
            metroGrid1.EnableHeadersVisualStyles = false;
            metroGrid1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            metroGrid1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            metroGrid1.Location = new System.Drawing.Point(20, 300);
            metroGrid1.Name = "metroGrid1";
            metroGrid1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            metroGrid1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            metroGrid1.RowHeadersVisible = false;
            metroGrid1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            metroGrid1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            metroGrid1.Size = new System.Drawing.Size(this.mainPanel.Width - 45, 343);
            metroGrid1.TabIndex = 1;
            metroGrid1.ScrollBars = ScrollBars.Vertical;
            metroGrid1.BorderStyle = BorderStyle.Fixed3D;
            this.mainPanel.Controls.Add(metroGrid1);

            List<Meal> meals = new List<Meal>();
            OrderDTO order = this.getActiveOrder();
            meals.AddRange(order.Drinks);
            meals.AddRange(order.Foods);

            BindingList<Meal> mealDataSource = new BindingList<Meal>(meals);
            metroGrid1.DataSource = mealDataSource;


            metroGrid1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            metroGrid1.Columns[0].Width = metroGrid1.Width / 2;

            metroGrid1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            metroGrid1.Columns[1].Width = metroGrid1.Width / 3;
            metroGrid1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            metroGrid1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            metroGrid1.Columns[2].Width = metroGrid1.Width / 6 + (metroGrid1.Width /2 - (metroGrid1.Width / 6 + metroGrid1.Width / 3));
            metroGrid1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            MetroButton btn = this.mainPanel.Controls.OfType<MetroButton>().Where(b => string.Equals(b.Name, "btn2")).FirstOrDefault();
            btn.Visible = true;
            btn.Location = new Point(btn.Location.X, metroGrid1.Location.Y + metroGrid1.Height + 20);
        }

        private void CheckBoxChange(object sender, EventArgs e)
        {
            MetroCheckBox chck = (MetroCheckBox)sender;
            TableDTO t = table.Table;
            //t.Empty = !chck.Checked;
            TablesController.modifyTableStatus(t.Id);
        }

        private void createBtn()
        {

        }

        private List<DrinkDTO> getDrinks()
        {
            string URI = "http://tpvparatodos.azurewebsites.net/api/drink";
            HttpWebRequest request = WebRequest.Create(URI) as HttpWebRequest;
            request.Method = "GET";
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string strsb = sr.ReadToEnd();

            return (List<DrinkDTO>)Newtonsoft.Json.JsonConvert.DeserializeObject(strsb, typeof(List<DrinkDTO>));
        }

        private OrderDTO getActiveOrder()
        {
            string URI = "http://tpvparatodos.azurewebsites.net/api/Order/lastByTable/" + table.Table.Id;
            HttpWebRequest request = WebRequest.Create(URI) as HttpWebRequest;
            request.Method = "GET";
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string strsb = sr.ReadToEnd();

            return (OrderDTO)Newtonsoft.Json.JsonConvert.DeserializeObject(strsb, typeof(OrderDTO));
        }


    }
}
