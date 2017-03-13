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

namespace Desktop.Controller.TablesController
{
    public class TablesController
    {
        bool testa = false;
        #region Fields
        #region Private Fields
        #region Web-service fields
        const string SERVER_URI = "http://172.16.100.19/TPVParaTodos/signalr";    // Connection string
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
        private IHubProxy HubProxy { get; set; }
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
            //WebServiceConnection.Closed += Connection_Closed;
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
            test();
        }

        private void updateTables(string tables)
        {
            List<TableDTO> tablesList = (List<TableDTO>)Newtonsoft.Json.JsonConvert.DeserializeObject(tables, typeof(List<TableDTO>));
            foreach (TableDTO t in tablesList)
            {
                MessageBox.Show(t.id.ToString() + " " + t.Empty.ToString());
            }

            if (!testa)
            {
                test2();
                testa = true;
            }
        }
        #endregion

        private async void test()
        {
            await HubProxy.Invoke("getall");

        }

        private async void test2()
        {
            await HubProxy.Invoke("changestatus", new Random().Next(4), false);
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
