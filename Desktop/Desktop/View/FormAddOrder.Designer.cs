using MetroFramework.Controls;

namespace Desktop.View
{
    partial class FormAddOrder
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAddOrder));
            this.mealsTabControl = new MetroFramework.Controls.MetroTabControl();
            this.startersTab = new MetroFramework.Controls.MetroTabPage();
            this.mainTab = new MetroFramework.Controls.MetroTabPage();
            this.dessertsTab = new MetroFramework.Controls.MetroTabPage();
            this.drinksTab = new MetroFramework.Controls.MetroTabPage();
            this.menusTab = new MetroFramework.Controls.MetroTabPage();
            this.metroGrid1 = new MetroFramework.Controls.MetroGrid();
            this.sendOrderBtn = new MetroFramework.Controls.MetroButton();
            this.mealsTabControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.metroGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // mealsTabControl
            // 
            this.mealsTabControl.Controls.Add(this.startersTab);
            this.mealsTabControl.Controls.Add(this.mainTab);
            this.mealsTabControl.Controls.Add(this.dessertsTab);
            this.mealsTabControl.Controls.Add(this.drinksTab);
            this.mealsTabControl.Controls.Add(this.menusTab);
            this.mealsTabControl.Location = new System.Drawing.Point(299, 79);
            this.mealsTabControl.Name = "mealsTabControl";
            this.mealsTabControl.SelectedIndex = 0;
            this.mealsTabControl.Size = new System.Drawing.Size(1028, 551);
            this.mealsTabControl.Style = MetroFramework.MetroColorStyle.Silver;
            this.mealsTabControl.TabIndex = 0;
            this.mealsTabControl.UseSelectable = true;
            // 
            // startersTab
            // 
            this.startersTab.HorizontalScrollbarBarColor = true;
            this.startersTab.HorizontalScrollbarHighlightOnWheel = false;
            this.startersTab.HorizontalScrollbarSize = 10;
            this.startersTab.Location = new System.Drawing.Point(4, 38);
            this.startersTab.Name = "startersTab";
            this.startersTab.Padding = new System.Windows.Forms.Padding(3);
            this.startersTab.Size = new System.Drawing.Size(1020, 509);
            this.startersTab.TabIndex = 0;
            this.startersTab.Text = "Entrantes";
            this.startersTab.UseVisualStyleBackColor = true;
            this.startersTab.VerticalScrollbarBarColor = true;
            this.startersTab.VerticalScrollbarHighlightOnWheel = false;
            this.startersTab.VerticalScrollbarSize = 10;
            // 
            // mainTab
            // 
            this.mainTab.HorizontalScrollbarBarColor = true;
            this.mainTab.HorizontalScrollbarHighlightOnWheel = false;
            this.mainTab.HorizontalScrollbarSize = 10;
            this.mainTab.Location = new System.Drawing.Point(4, 38);
            this.mainTab.Name = "mainTab";
            this.mainTab.Padding = new System.Windows.Forms.Padding(3);
            this.mainTab.Size = new System.Drawing.Size(1020, 509);
            this.mainTab.TabIndex = 1;
            this.mainTab.Text = "Principales";
            this.mainTab.UseVisualStyleBackColor = true;
            this.mainTab.VerticalScrollbarBarColor = true;
            this.mainTab.VerticalScrollbarHighlightOnWheel = false;
            this.mainTab.VerticalScrollbarSize = 10;
            // 
            // dessertsTab
            // 
            this.dessertsTab.HorizontalScrollbarBarColor = true;
            this.dessertsTab.HorizontalScrollbarHighlightOnWheel = false;
            this.dessertsTab.HorizontalScrollbarSize = 10;
            this.dessertsTab.Location = new System.Drawing.Point(4, 38);
            this.dessertsTab.Name = "dessertsTab";
            this.dessertsTab.Size = new System.Drawing.Size(1020, 509);
            this.dessertsTab.TabIndex = 3;
            this.dessertsTab.Text = "Postres";
            this.dessertsTab.UseVisualStyleBackColor = true;
            this.dessertsTab.VerticalScrollbarBarColor = true;
            this.dessertsTab.VerticalScrollbarHighlightOnWheel = false;
            this.dessertsTab.VerticalScrollbarSize = 10;
            // 
            // drinksTab
            // 
            this.drinksTab.HorizontalScrollbarBarColor = true;
            this.drinksTab.HorizontalScrollbarHighlightOnWheel = false;
            this.drinksTab.HorizontalScrollbarSize = 10;
            this.drinksTab.Location = new System.Drawing.Point(4, 38);
            this.drinksTab.Name = "drinksTab";
            this.drinksTab.Size = new System.Drawing.Size(1020, 509);
            this.drinksTab.TabIndex = 5;
            this.drinksTab.Text = "Bebidas";
            this.drinksTab.UseVisualStyleBackColor = true;
            this.drinksTab.VerticalScrollbarBarColor = true;
            this.drinksTab.VerticalScrollbarHighlightOnWheel = false;
            this.drinksTab.VerticalScrollbarSize = 10;
            // 
            // menusTab
            // 
            this.menusTab.HorizontalScrollbarBarColor = true;
            this.menusTab.HorizontalScrollbarHighlightOnWheel = false;
            this.menusTab.HorizontalScrollbarSize = 10;
            this.menusTab.Location = new System.Drawing.Point(4, 38);
            this.menusTab.Name = "menusTab";
            this.menusTab.Size = new System.Drawing.Size(1020, 509);
            this.menusTab.TabIndex = 4;
            this.menusTab.Text = "Menús";
            this.menusTab.UseVisualStyleBackColor = true;
            this.menusTab.VerticalScrollbarBarColor = true;
            this.menusTab.VerticalScrollbarHighlightOnWheel = false;
            this.menusTab.VerticalScrollbarSize = 10;
            // 
            // metroGrid1
            // 
            this.metroGrid1.AllowUserToAddRows = false;
            this.metroGrid1.AllowUserToDeleteRows = false;
            this.metroGrid1.AllowUserToResizeColumns = false;
            this.metroGrid1.AllowUserToResizeRows = false;
            this.metroGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.metroGrid1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.metroGrid1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.metroGrid1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(139)))), ((int)(((byte)(202)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.metroGrid1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.metroGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.metroGrid1.DefaultCellStyle = dataGridViewCellStyle2;
            this.metroGrid1.EnableHeadersVisualStyles = false;
            this.metroGrid1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.metroGrid1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.metroGrid1.Location = new System.Drawing.Point(23, 87);
            this.metroGrid1.Name = "metroGrid1";
            this.metroGrid1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.metroGrid1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.metroGrid1.RowHeadersVisible = false;
            this.metroGrid1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.metroGrid1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.metroGrid1.Size = new System.Drawing.Size(242, 543);
            this.metroGrid1.TabIndex = 1;
            // 
            // sendOrderBtn
            // 
            this.sendOrderBtn.Enabled = false;
            this.sendOrderBtn.Location = new System.Drawing.Point(23, 636);
            this.sendOrderBtn.Name = "sendOrderBtn";
            this.sendOrderBtn.Size = new System.Drawing.Size(242, 38);
            this.sendOrderBtn.TabIndex = 2;
            this.sendOrderBtn.Text = "Enviar comanda";
            this.sendOrderBtn.UseSelectable = true;
            // 
            // FormAddOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 691);
            this.Controls.Add(this.sendOrderBtn);
            this.Controls.Add(this.metroGrid1);
            this.Controls.Add(this.mealsTabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormAddOrder";
            this.Style = MetroFramework.MetroColorStyle.Silver;
            this.Text = "Añadir comanda";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.mealsTabControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.metroGrid1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public MetroTabControl mealsTabControl;
        public MetroTabPage startersTab;
        public MetroTabPage mainTab;
        public MetroTabPage dessertsTab;
        public MetroTabPage menusTab;
        public MetroTabPage drinksTab;
        public MetroGrid metroGrid1;
        public MetroButton sendOrderBtn;
    }
}