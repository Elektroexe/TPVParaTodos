﻿using MetroFramework.Forms;
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
    public partial class FormPopUp : MetroForm
    {
        public FormPopUp(bool correct, string text)
        {
            InitializeComponent();
            this.messageIconPb.Image = (correct) ? Properties.Resources.correct_icon : Properties.Resources.error_icon;
            this.messageTextLabel.Text = text;
        }

        private void acceptBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
