using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.View;
using Desktop.Model;
using System.Windows.Forms;
using Desktop.UserControls;
using System.Drawing;
using System.ComponentModel;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Desktop.Controller
{
    public class AddOrderController
    {
        private FormAddOrder _addOrderView;

        public static List<Meal> meals;

        private int _tableNumber;


        public AddOrderController(int tableNumber)
        {
            _addOrderView = new FormAddOrder();
            initMeals();
            _addOrderView.Show();
            this._tableNumber = tableNumber;

            meals = new List<Meal>();
            initListeners();
        }

        private void initListeners()
        {
            _addOrderView.sendOrderBtn.Click += SendOrder;
        }

        private void initMeals()
        {
            int twidth = _addOrderView.controlTabPanel.Width;
            int theight = _addOrderView.controlTabPanel.Height;

            int mealwidth = 150;
            int mealheight = 150;

            int space = 35;

            int horizontalQty = twidth / (mealwidth + space);
            int totalSpace = (mealwidth + space) * horizontalQty + space;
            if (totalSpace > twidth) horizontalQty--;

            totalSpace = (mealwidth + space) * horizontalQty + space;

            int initialX = (_addOrderView.controlTabPanel.Width - SystemInformation.VerticalScrollBarThumbHeight - totalSpace) / 2;

            int x = _addOrderView.controlTabPanel.Location.X + space + initialX;
            int y = _addOrderView.controlTabPanel.Location.Y + space;


            //TESTS
            List<FoodDTO> foods = this.getFoods();
            List<DrinkDTO> drinks = this.getDrinks();

            int compt = 1;
            foreach(DrinkDTO d in drinks)
            {
                DrinkMealUC uc = new DrinkMealUC(d);
                uc.Location = new Point(x, y);
                uc.plusPictureBox.MouseClick += UCClick;
                uc.minusPictureBox.MouseClick += UCClick;
                uc.mealPictureBox.MouseClick += UCMainPictureClick;

                x += mealwidth + space;
                if (compt % horizontalQty == 0)
                {
                    y += mealheight + space;
                    x = _addOrderView.controlTabPanel.Location.X + space + initialX;
                }

                compt++;

                uc.mealPictureBox.Image = getDrinkImage(d.Id);
                _addOrderView.controlTabPanel.Controls.Add(uc);
            }

            foreach (FoodDTO f in foods)
            {
                FoodMealUC uc = new FoodMealUC(f);
                uc.Location = new Point(x, y);
                uc.plusPictureBox.MouseClick += UCClick;
                uc.minusPictureBox.MouseClick += UCClick;
                uc.mealPictureBox.MouseClick += UCMainPictureClick;

                x += mealwidth + space;
                if (compt % horizontalQty == 0)
                {
                    y += mealheight + space;
                    x = _addOrderView.controlTabPanel.Location.X + space + initialX;
                }

                compt++;
                uc.mealPictureBox.Image = getFoodImage(f.Id);
                _addOrderView.controlTabPanel.Controls.Add(uc);
            }
        }

        private void UCMainPictureClick(object sender, EventArgs e)
        {
            List<MealUC> MealUCList = _addOrderView.controlTabPanel.Controls.OfType<MealUC>().ToList();
            foreach(MealUC m in MealUCList)
            {
                PictureBox p = (PictureBox)sender;
                if ((MealUC)p.Parent != m)
                {
                    m.mealPictureBox.Visible = true;
                }
            }
        }
        private void UCClick(object sender, EventArgs e)
        {
            this.RefreshData();
        }
        private void RefreshData()
        {
            BindingList<Meal> mealDataSource = new BindingList<Meal>(meals);
            _addOrderView.metroGrid1.DataSource = mealDataSource;

            this._addOrderView.sendOrderBtn.Enabled = meals.Count > 0;
        }

        private List<DrinkDTO> getDrinks()
        {
            string URI = "http://tpvparatodos.azurewebsites.net/api/drink";
            HttpWebRequest request = WebRequest.Create(URI) as HttpWebRequest;
            request.Method = "GET";
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string strsb = sr.ReadToEnd();

            return (List<DrinkDTO>)Newtonsoft.Json.JsonConvert.DeserializeObject(strsb, typeof(List<DrinkDTO>));
        }

        private List<FoodDTO> getFoods()
        {
            string URI = "http://tpvparatodos.azurewebsites.net/api/food";
            HttpWebRequest request = WebRequest.Create(URI) as HttpWebRequest;
            request.Method = "GET";
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string strsb = sr.ReadToEnd();

            return (List<FoodDTO>)Newtonsoft.Json.JsonConvert.DeserializeObject(strsb, typeof(List<FoodDTO>));
        }

        private void postOrder()
        {
            OrderDTO order = new OrderDTO();
            order.Date = DateTime.Now;
            order.Table_Id = this._tableNumber;
            order.Total = 0;

            List<DrinkDTO> drinksAux = meals.OfType<DrinkDTO>().ToList();
            order.Drinks = new List<DrinkDTO>();
            foreach(DrinkDTO d in drinksAux)
            {
                for (int i = 0; i < d.Quantity; i++)
                {
                    order.Drinks.Add(d);
                }
                order.Total += (Decimal)d.TotalPrice;
            }

            List<FoodDTO> foodsAux = meals.OfType<FoodDTO>().ToList();
            order.Foods = new List<FoodDTO>();
            foreach(FoodDTO f in foodsAux)
            {
                for(int i = 0; i <f.Quantity -1; i++)
                {
                    order.Foods.Add(f);
                }
                order.Total += (Decimal)f.TotalPrice;
            }

            string URI = "http://tpvparatodos.azurewebsites.net/api/Order";
            //string URI = "http://172.16.100.19/TPVParaTodos/api/Order";
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
        }

        private void SendOrder(object sender, EventArgs e)
        {
            postOrder();
        }

        private Image getDrinkImage(int id)
        {
            string url = "http://tpvparatodos.azurewebsites.net/Image/Drink/" + id;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream recieveStream = response.GetResponseStream();

            return Bitmap.FromStream(recieveStream);
        }

        private Image getFoodImage(int id)
        {
            string url = "http://tpvparatodos.azurewebsites.net/Image/Food/" + id;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream recieveStream = response.GetResponseStream();

            return Bitmap.FromStream(recieveStream);
        }

    }
}
