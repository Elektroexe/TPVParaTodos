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

namespace Desktop.Controller
{
    public class TablesController
    {
        #region Fields
        #region Private Fields
        #region Web-service fields
        const string SERVER_URI = "http://tpvparatodos.azurewebsites.net/signalr";    // Connection string
        #endregion
        #endregion
        #endregion

        #region Properties

        #region Public properties
        #endregion

        #region Private properties
        // Views
        private FormTables tablesView { get; }

        private HubConnection WebServiceConnection { get; set; }
        private static IHubProxy HubProxy { get; set; } // QUITAR ESE HORROR DE AHI <-- (static)
        #endregion

        #endregion

        #region Constructor
        public TablesController()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            tablesView = new FormTables();

            this.initWebSocketListener();

        }
        #endregion

        #region Web-Service methods

        private async void initWebSocketListener()
        {
            WebServiceConnection = new HubConnection(SERVER_URI);
            HubProxy = WebServiceConnection.CreateHubProxy("TableHub");
            HubProxy.On<string>("refresh", list => updateTables(list));
            try
            {
                await WebServiceConnection.Start();

            }
            catch (HttpRequestException)
            {
                //StatusText.Text = "Unable to connect to server: Start server before connecting clients.";
                return;
            }
            getAllTables();
        }

        private void updateTables(string tables)
        {
            List<TableDTO> tablesList = (List<TableDTO>)Newtonsoft.Json.JsonConvert.DeserializeObject(tables, typeof(List<TableDTO>));
            foreach (TableDTO t in tablesList)
            {
                syncTables(t);
            }

            tablesView.Invoke(new MethodInvoker(delegate () {
                tablesView.Refresh();
            }));

            SidebarTable sidebar = this.tablesView.rightPanel;
            if (sidebar != null)
            {
                TableUC t = (TableUC)tablesView.Controls["tableUC" + sidebar.Table.Table.Id.ToString()];
                sidebar.Table.Table = t.Table;
            }

            if (tablesView.rightPanel != null)
            {
                tablesView.rightPanel.Invoke(new MethodInvoker(delegate ()
                {
                    tablesView.rightPanel.Refresh();
                }));
            }
        }
        #endregion

        private async void getAllTables()
        {
            await HubProxy.Invoke("getall");

        }

        public static async void modifyTableStatus(int id)
        {
            await HubProxy.Invoke("changestatus", id);
        }

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
            }
        }
    }
}
