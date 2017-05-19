using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Desktop.View;
using Desktop.Model;
using Desktop.UserControls;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;

namespace Desktop.Controller
{
    public class TablesController
    {
        #region Properties
        #region Private properties
        // View
        private FormTables tablesView { get; }

        #endregion

        #endregion

        #region Constructor
        public TablesController()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            tablesView = new FormTables();

            WebserviceConnection.initWebSocketListener(updateTables);

            Notifications.startListeningNotifications();

            Button btn = new Button();
            btn.Location = new Point(1000, 700);
            btn.Text = "hola";
            btn.Click += (o, e) => { new LoginController(); };
            tablesView.Controls.Add(btn);

        }
        #endregion

        #region Private Helpers

        private void updateTables(string tables)
        {
            // Get tables from json string
            List<TableDTO> tablesList = (List<TableDTO>)Newtonsoft.Json.JsonConvert.DeserializeObject(tables, typeof(List<TableDTO>));
            foreach (TableDTO t in tablesList)
            {
                syncTables(t);
            }

            // Refresh the view
            tablesView.Invoke(new MethodInvoker(delegate () {
                tablesView.Refresh();
            }));

            // Refresh sidebar's table object
            SidebarTable sidebar = this.tablesView.rightPanel;
            if (sidebar != null)
            {
                TableUC t = (TableUC)tablesView.Controls["tableUC" + sidebar.Table.Table.Id.ToString()];
                sidebar.Table.Table = t.Table;
            }
            
            // Refresh sidebar
            if (tablesView.rightPanel != null)
            {
                tablesView.rightPanel.Invoke(new MethodInvoker(delegate ()
                {
                    tablesView.rightPanel.Refresh();
                }));
            }
        }
        #endregion

        

        private void syncTables(TableDTO table)
        {
            TableUC t = (TableUC)tablesView.Controls["tableUC" + table.Id];
            t.Table = table;
        }


        public void start()
        {
            this.run();
        }
        [STAThread]
        protected void run()
        {
            try
            {
                Application.Run(this.tablesView);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
