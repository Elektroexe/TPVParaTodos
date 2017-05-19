using Desktop.View;
using MetroFramework.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Desktop.Controller
{
    public class LoginController
    {
        private FormLogin _loginView;
        private Form _principal;

        public LoginController(Form principal)
        {
            _principal = principal;
            _loginView = new FormLogin();
            initListeners();
            _loginView.ShowDialog();
        }


        private void initListeners()
        {
            foreach (MetroTextBox t in this._loginView.Controls.OfType<MetroTextBox>().ToList())
            {

                t.TextChanged += changedTextBoxText;
            }
            this._loginView.loginBtn.Click += doLogin;
            this._loginView.FormClosed += formClosed;


        }

        private void formClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
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
                this._loginView.FormClosed -= formClosed;
                this._loginView.Close();
                _principal.Visible = true;
            }
            else
            {
                (new FormPopUp(false, "Usuario y/o contraseña incorrectos")).ShowDialog();
            }
        }

        public string GetResourceTextFile()
        {
            XElement resource = XElement.Parse(Properties.Resources.user1);
            return (resource.FirstNode != null) ? resource.FirstNode.ToString() : "";
        }

    }
}
