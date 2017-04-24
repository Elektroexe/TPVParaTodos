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
        private const string URI = "http://172.16.100.19/TPVParaTodos/";

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

        public static Image getImage(int id, String meal)
        {
            string url = URI + "Image/" + meal + "/" + id;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream recieveStream = response.GetResponseStream();

            return Bitmap.FromStream(recieveStream);
        }

        public static int PostOrder(OrderDTO order)
        {
            string URI = WebserviceConnection.URI + "api/" + "Order";
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
    }
}
