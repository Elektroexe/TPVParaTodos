using Desktop.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Controller
{
    public class WebserviceConnection
    {
        //private const string URI = "http://tpvparatodos.azurewebsites.net/";
        //private const string URI = "http://tpvpt.azurewebsites.net/";
        private const string URI = "http://172.16.10.20/TPVParaTodos/";
        //private const string URI = "http://172.16.100.19/TPVParaTodos/";


        private const string TOKEN_TYPE = "bearer";
        public static string token;

        public static List<T> getMeal<T>(string element)
        {
            string URI = WebserviceConnection.URI + "api/" + element;
            HttpWebRequest request = WebRequest.Create(URI) as HttpWebRequest;
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

        public static int PostOrder(OrderDTO order)
        {
            string URI = WebserviceConnection.URI + "api/" + "Orders/Manager";
            HttpWebRequest request = WebRequest.Create(URI) as HttpWebRequest;
            string sb = JsonConvert.SerializeObject(order, new JsonSerializerSettings
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects
            });
            request.Method = "POST";

            request.ContentType = "application/json";
            Byte[] bt = Encoding.UTF8.GetBytes(sb);
            Stream st = request.GetRequestStream();
            st.Write(bt, 0, bt.Length);
            st.Close();

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            return (int)response.StatusCode;
        }

        public static bool getToken(string username, string password)
        {
            bool correctOperation = true;

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URI + "token");
            request.Method = "POST";
            //request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
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

            //tokenTest();
        }

        public static void tokenTest()
        {
            string URI = WebserviceConnection.URI + "api/Account/UserInfo";
            HttpWebRequest request = WebRequest.Create(URI) as HttpWebRequest;
            request.Method = "GET";
            request.Headers.Add("authorization", TOKEN_TYPE + " " + token + "a");
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string strsb = sr.ReadToEnd();

        }
    }
}
