﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Controls;

namespace Desktop.UserControls
{
    public partial class TableUC : UserControl
    {
        private int _incrementFactor;
        private int _tableNumber;
        private Color _borderColor;

        public int TableNumber
        {
            get
            {
                return this._tableNumber;
            }
            set
            {
                this._tableNumber = value;
            }
        }
        public Color BorderColor
        {
            get
            {
                return this._borderColor;
            }
            set
            {
                this._borderColor = value;
            }
        }

        public TableUC(int value, int incrementFactor)
        {
            _incrementFactor = incrementFactor;
            _tableNumber = value;
            InitializeComponent();
            //initPosition(value);
        }

        public void initPosition()
        {
            //this.BackColor = Color.Black;
            //this.Dock = DockStyle.Fill;
            this.Paint += paintPanel;
            this.pictureBox1.Paint += paintPb;
            this.pictureBox1.Size = new Size(this.pictureBox1.Width * _incrementFactor, this.pictureBox1.Height * _incrementFactor);
            this.pictureBox1.Location = new Point((this.Width - this.pictureBox1.Width)/2, (this.Height - this.pictureBox1.Height) / 2);
            this.pictureBox1.BackColor = Color.Transparent;


            Label l = new Label();
            l.Text = (_tableNumber < 10) ? " " + _tableNumber.ToString() : _tableNumber.ToString();
            l.Parent = pictureBox1;
            l.Location = new Point(36 * _incrementFactor + 1, 88 * _incrementFactor - 1);
            l.AutoSize = true;
            l.Font = new Font("Arial Black", 11 * _incrementFactor, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            pictureBox1.Controls.Add(l);
        }

        private void paintPanel(object sender, PaintEventArgs e)
        {
            Brush brush = new SolidBrush(_borderColor);
            Pen pen = new Pen(Color.Black, 5);

            int size = 110 * _incrementFactor;

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.DrawEllipse(pen, new Rectangle((this.Width - size) / 2, (this.Height - size) / 2 - 7 * _incrementFactor, size, size));
            e.Graphics.FillEllipse(brush, new Rectangle((this.Width - size) / 2, (this.Height - size) / 2 - 7 * _incrementFactor, size, size));


        }

        private void paintPb(object sender, PaintEventArgs e)
        {
            Brush brush = new SolidBrush(Color.White);
            Pen pen = new Pen(Color.Black, 5);

            int size = 25 * _incrementFactor;

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.DrawEllipse(pen, new Rectangle(this.pictureBox1.Width / 2 - size/2, this.pictureBox1.Height - 30 * _incrementFactor, size, size));
            e.Graphics.FillEllipse(brush, new Rectangle(this.pictureBox1.Width / 2 - size/2, this.pictureBox1.Height - 30 * _incrementFactor , size, size));

        }
    }
}
