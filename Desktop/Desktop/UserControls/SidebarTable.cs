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
using Desktop.View;

namespace Desktop.UserControls
{
    public partial class SidebarTable : MetroUserControl
    {

        private readonly String[] buttons = new String[] { "Añadir pedido", "Ver pedido", "Modificar pedido", "Cerrar pedido" };

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
        public SidebarTable(TableUC table)
        {
            InitializeComponent();
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

            int ant = 300;
            for (int i = 0; i < 4; i++)
            {
                MetroButton btn = new MetroButton();
                btn.Location = new Point(15, ant);
                btn.Size = new Size(270, 45);
                btn.Name = "btn" + i;
                btn.Text = buttons[i];
                btn.ForeColor = Color.Green;
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
                else if (i == 3)
                {
                    btn.Click += closeOrderButton;
                }

                if (i >= 1 && i < 4)
                {
                    if (table.Table.Empty) btn.Enabled = false;
                }
                this.mainPanel.Controls.Add(btn);
                ant += 60;
            }

        }

        private void addOrderButton(object sender, EventArgs e)
        {
            AddOrderController a = new AddOrderController(table.TableNumber);
        }

        private void modifyOrderButton(object sender, EventArgs e)
        {
            AddOrderController a = new AddOrderController(table.TableNumber, WebserviceConnection.getActiveOrder(table.Table.Id));
        }

        private void closeOrderButton(object sender, EventArgs e)
        {
            OrderDTO activeOrder = WebserviceConnection.getActiveOrder(table.Table.Id);
            new CloseOrderController(activeOrder);
        }
             
        private void viewOrderButton(object sender, EventArgs e)
        {
            this.mainPanel.Controls.OfType<MetroButton>().ToList().ForEach(b => b.Visible = false);

            MetroGrid viewGrid = new MetroGrid();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            viewGrid.AllowUserToAddRows = false;
            viewGrid.AllowUserToDeleteRows = false;
            viewGrid.AllowUserToResizeColumns = false;
            viewGrid.AllowUserToResizeRows = false;
            viewGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            viewGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            viewGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            viewGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(139)))), ((int)(((byte)(202)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            viewGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            viewGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            viewGrid.DefaultCellStyle = dataGridViewCellStyle2;
            viewGrid.EnableHeadersVisualStyles = false;
            viewGrid.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            viewGrid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            viewGrid.Location = new System.Drawing.Point(20, 300);
            viewGrid.Name = "metroGrid1";
            viewGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            viewGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            viewGrid.RowHeadersVisible = false;
            viewGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            viewGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            viewGrid.Size = new System.Drawing.Size(this.mainPanel.Width - 45, 343);
            viewGrid.TabIndex = 1;
            viewGrid.ScrollBars = ScrollBars.Vertical;
            viewGrid.BorderStyle = BorderStyle.Fixed3D;
            this.mainPanel.Controls.Add(viewGrid);

            List<Product> products = new List<Product>();

            OrderDTO order = null;
            try {
                 order = WebserviceConnection.getActiveOrder(table.Table.Id);
            }
            catch (Exception ex)
            {
                FormPopUp fPop = new FormPopUp(false, "Error obteniendo el pedido");
                fPop.ShowDialog();
                return;
            }
            if (order.Drinks != null) products.AddRange(order.Drinks);
            if (order.Foods != null) products.AddRange(order.Foods);
            if (order.Menus != null) products.AddRange(order.Menus);

            BindingList<Product> mealDataSource = new BindingList<Product>(products);
            viewGrid.DataSource = mealDataSource;


            viewGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            viewGrid.Columns[0].Width = viewGrid.Width / 3;

            viewGrid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            viewGrid.Columns[1].Width = viewGrid.Width / 3;
            viewGrid.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            viewGrid.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            viewGrid.Columns[2].Width = viewGrid.Width / 3;
            viewGrid.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            MetroButton btn = this.mainPanel.Controls.OfType<MetroButton>().Where(b => string.Equals(b.Name, "btn2")).FirstOrDefault();
            btn.Visible = true;
            btn.Location = new Point(btn.Location.X, viewGrid.Location.Y + viewGrid.Height + 20);
        }

        public void refreshButtons(bool empty)
        {

            List<MetroButton> btns = this.mainPanel.Controls.OfType<MetroButton>().ToList();
            foreach(MetroButton m in btns)
            {
                if (m.Name.Equals("btn0"))
                {
                    m.Enabled = empty;
                }
                else
                {
                    m.Enabled = !empty;
                }
            }
        }

    }
}
