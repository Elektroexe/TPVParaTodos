using Desktop.Model;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Controller
{
    public class WebserviceConnection
    {
        //private const string URI = "http://tpvparatodos.azurewebsites.net/";
        private const string URI = "http://tpvpt.azurewebsites.net/";
        //private const string URI = "http://172.16.10.20/TPVParaTodos/";
        //private const string URI = "http://172.16.100.19/TPVParaTodos/";

        #region Webservice Fields
        private const string TOKEN_TYPE = "bearer";
        public static string token;
        #endregion

        #region Hub Fields
        private const string HUB_URI = "http://tpvpt.azurewebsites.net/signalr";
        private static HubConnection webserviceConnection;
        private static IHubProxy HubProxy;
        #endregion


        #region Webservice Methods
        public static List<T> getMeal<T>(string element)
        {
            string URI = WebserviceConnection.URI + "api/" + element;
            HttpWebRequest request = WebRequest.Create(URI) as HttpWebRequest;
            request.Headers.Add("Authorization", TOKEN_TYPE + " " + WebserviceConnection.token);
            request.Method = "GET";
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string strsb = sr.ReadToEnd();

            return (List<T>)JsonConvert.DeserializeObject(strsb, typeof(List<T>));
        }

        public static Image getImage(int id)
        {
            string url = URI + "Image/Product/" + id;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream recieveStream = response.GetResponseStream();

            return Bitmap.FromStream(recieveStream);
        }

        public static int PostAndPutOrder(OrderDTO order)
        {
            string URI = WebserviceConnection.URI + "api/" + "Orders/Manager";
            HttpWebRequest request = WebRequest.Create(URI) as HttpWebRequest;
            string sb = JsonConvert.SerializeObject(order, new JsonSerializerSettings
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects
            });
            request.Method = (order.Id != 0) ? "PUT" : "POST" ;

            request.ContentType = "application/json";
            Byte[] bt = Encoding.UTF8.GetBytes(sb);
            Stream st = request.GetRequestStream();
            st.Write(bt, 0, bt.Length);
            st.Close();

            HttpWebResponse response;
            try {
                response = request.GetResponse() as HttpWebResponse;
            } catch(Exception ex)
            {
                return 400;
            }
            return (int)response.StatusCode;
        }


        public static bool getToken(string username, string password)
        {
            bool correctOperation = true;

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URI + "token");
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            string tosend = "grant_type=password&username=" + username + "&password=" + password;

            using (StreamWriter stOut = new StreamWriter(request.GetRequestStream(), Encoding.ASCII))
            {
                stOut.Write(tosend);
                stOut.Close();
            }

            try {
                var response = (HttpWebResponse)request.GetResponse();
                string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                dynamic deserializedValue = JsonConvert.DeserializeObject(responseString);
                token = (string)deserializedValue["access_token"];
            } catch(Exception ex)
            {
                correctOperation = false;
            }

            return correctOperation;

        }

        public static OrderDTO getActiveOrder(int tableId)
        {
            HttpWebRequest request = WebRequest.Create(URI + "api/Orders/Manager/" + tableId) as HttpWebRequest;
            request.Method = "GET";
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string strsb = sr.ReadToEnd();

            return (OrderDTO)Newtonsoft.Json.JsonConvert.DeserializeObject(strsb, typeof(OrderDTO));
        }

        public static void closeOrder(int tableId)
        {
            HttpWebRequest request = WebRequest.Create(URI + "api/Orders/Close/" + tableId) as HttpWebRequest;
            request.Method = "POST";
            request.ContentLength = 0;
            WebResponse response = request.GetResponse();

        }

        #endregion

        #region Hub Methods
        public static async void initWebSocketListener(Action<string> updateTables)
        {
            webserviceConnection = new HubConnection(HUB_URI);
            HubProxy = webserviceConnection.CreateHubProxy("TableHub");
            HubProxy.On<string>("refresh", list => updateTables(list));
            try
            {
                await webserviceConnection.Start();

            }
            catch (HttpRequestException)
            {
                return;
            }
            getAllTables();
        }

        public static async void getAllTables()
        {
            await HubProxy.Invoke("getall");
        }

        public static async void modifyTableStatus(int id)
        {
            await HubProxy.Invoke("changestatus", id);
        }
        #endregion

    }
}
