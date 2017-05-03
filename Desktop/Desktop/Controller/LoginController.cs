using Desktop.View;
using MetroFramework.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop.Controller
{
    public class LoginController
    {
        private FormLogin _loginView;

        public LoginController()
        {
            _loginView = new FormLogin();
            initListeners();
            _loginView.ShowDialog();
        }

        private void initListeners()
        {
            foreach(MetroTextBox t in this._loginView.Controls.OfType<MetroTextBox>().ToList())
            {

                t.TextChanged += changedTextBoxText;
            }

            this._loginView.loginBtn.Click += doLogin;

            this._loginView.FormClosing += formClosing;
            

        }

        private void formClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void changedTextBoxText(object sender, EventArgs e)
        {
            bool enabled = true;
            foreach (MetroTextBox t in this._loginView.Controls.OfType<MetroTextBox>().ToList())
            {
                enabled &= !string.IsNullOrEmpty(t.Text);
            }

            this._loginView.loginBtn.Enabled = enabled;
        }

        private void doLogin(object sender, EventArgs e) 
        {
            if (WebserviceConnection.getToken(this._loginView.usernameTxtbox.Text, this._loginView.passwordTxtbox.Text))
            {
                this._loginView.FormClosing -= formClosing;
                this._loginView.Close();
            }
            else
            {
                (new FormPopUp(false, "Usuario y/o contraseña incorrectos")).ShowDialog(); 
            }
        }

    }
}
