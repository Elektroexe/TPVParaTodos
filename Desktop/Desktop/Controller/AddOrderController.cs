using System;
using System.Collections.Generic;
using System.Linq;
using Desktop.View;
using Desktop.Model;
using System.Windows.Forms;
using Desktop.UserControls;
using System.Drawing;
using System.ComponentModel;
using System.Reflection;
using MetroFramework.Controls;

namespace Desktop.Controller
{
    public class AddOrderController
    {
        #region Fields
        // Controller --> view objects
        private FormAddOrder _addOrderView;

        // Current table's order
        private OrderDTO _activeOrder;

        // Current table's number
        private int _tableNumber;

        // Notifications
        private Notifications not;

        #region Constants
        private const int MEALWIDTH = 150;
        private const int MEALHEIGHT = 150;
        private const int SPACEBETWEENMEALS = 35;
        #endregion

        #endregion

        #region Static Fields
        // All of products which take part of the order
        public static List<Product> products;
        #endregion

        #region Constructor
        public AddOrderController(int tableNumber)
        {
            initControllerItems(tableNumber, null);
        }

        public AddOrderController(int tableNumber, OrderDTO activeOrder)
        {
            initControllerItems(tableNumber, activeOrder);
            RefreshData();
        }
        #endregion

        #region Private helpers
        private void initNotifications()
        {
            not = new Notifications(_addOrderView);
            not.startListeningNotifications();
        }

        /// <summary>
        /// Initialize all relevant items
        /// </summary>
        /// <param name="tableNumber">table to which products will be assigned</param>
        /// <param name="activeOrder">table's active order (if it's null means no active order)</param>
        private void initControllerItems(int tableNumber, OrderDTO activeOrder)
        {
            products = new List<Product>();
            _addOrderView = new FormAddOrder();
            _addOrderView.FormClosed += (o, e) =>
            {
                not.finish();
            };
            initNotifications();

            if (activeOrder != null)
            {
                fillMealsFromOrder(activeOrder);
                _addOrderView.Text = "Modificar comanda";
            }

            initAllTabs();
            _addOrderView.Show();
            _tableNumber = tableNumber;
            _activeOrder = activeOrder;
            initListeners();
        }

        /// <summary>
        /// Add all products which were in the active order to meal's list
        /// </summary>
        /// <param name="activeOrder">table's active order</param>
        private void fillMealsFromOrder(OrderDTO activeOrder)
        {
            products.AddRange(activeOrder.Foods);
            products.AddRange(activeOrder.Drinks);
            products.AddRange(activeOrder.Menus);

        }

        /// <summary>
        /// Initialize all necessary view listeners
        /// </summary>
        private void initListeners()
        {
            _addOrderView.sendOrderBtn.Click += SendOrder;
        }

        private void initAllTabs()
        {
            // Get all products from the database
            List<Product> meals = new List<Product>();
            meals.AddRange(WebserviceConnection.getMeal<FoodDTO>("foods"));
            meals.AddRange(WebserviceConnection.getMeal<DrinkDTO>("drinks"));
            meals.AddRange(WebserviceConnection.getMeal<MenuDTO>("menus"));

            Dictionary<Panel, List<Product>> productsPanel = new Dictionary<Panel, List<Product>>();
            foreach (Product prod in meals)
            {
                Panel p = null;
                if (prod.GetType().Equals(typeof(FoodDTO)))
                {
                    p = (Panel)_addOrderView.panelControls[(prod as FoodDTO).FamilyDish.ToLower() + "Panel"];
                }
                else
                {
                    p = (Panel)_addOrderView.panelControls[prod.GetType().Name + "Panel"];
                }
                
                if (!productsPanel.ContainsKey(p))
                {
                    productsPanel[p] = new List<Product>();
                }

                List<Product> productsDic = productsPanel[p];

                productsDic.Add(prod);
                productsPanel[p] = productsDic;
            }

            foreach (KeyValuePair<Panel, List<Product>> entry in productsPanel)
            {
                initMeals(entry.Key, entry.Value);
            }
        }

        /// <summary>
        /// Initialize all products (visible objects and it's DTOs classes)
        /// </summary>
        private void initMeals(Panel pa, List<Product> products)
        {
            // Meals container size
            int twidth = pa.Width;
            int theight = pa.Height;

            // Calculate how many elements fit in each row
            int horizontalQty = twidth / (MEALWIDTH + SPACEBETWEENMEALS);
            int totalSpace = (MEALWIDTH + SPACEBETWEENMEALS) * horizontalQty + SPACEBETWEENMEALS;
            if (totalSpace > twidth) horizontalQty--;
            totalSpace = (MEALWIDTH + SPACEBETWEENMEALS) * horizontalQty + SPACEBETWEENMEALS;
            int initialX = (pa.Width - SystemInformation.VerticalScrollBarThumbHeight - totalSpace) / 2;

            // Set initial position for the first meal
            int x = pa.Location.X + SPACEBETWEENMEALS + initialX;
            int y = pa.Location.Y + SPACEBETWEENMEALS;

            // Create all meal object and place in its container
            this.createAndPlaceMeal(products, x, y, initialX, horizontalQty, pa);
        }

        /// <summary>
        /// Create all products object and place in its container
        /// </summary>
        /// <param name="t_meals">All products to be placed</param>
        /// <param name="t_x">Initial x position</param>
        /// <param name="t_y">Initial y position</param>
        /// <param name="initialX">Initial x without spaces</param>
        /// <param name="horizontalQty">Meals per row</param>
        private void createAndPlaceMeal(List<Product> t_meals,
                                        int t_x,
                                        int t_y,
                                        int initialX,
                                        int horizontalQty,
                                        Panel pa)
        {
            int compt = 1;
            foreach (Product m in t_meals)
            {
                // Get meal object from current order(if it's null we keep the default value)
                Product mAux = products.Where(x => x.Id == m.Id).FirstOrDefault() ?? m;

                // Prepare reflection string for future instanciations and invocations
                string meal = (mAux.GetType().Name.Replace("DTO", ""));

                // Instanciate and initializate Product user control with the type specified on the bellow string
                MealUC mealuc = (MealUC)Activator.CreateInstance(null, "Desktop.UserControls." + meal + "MealUC").Unwrap();
                mealuc.SetDTO(mAux);
                mealuc.mealPictureBox.Image = WebserviceConnection.getImage(mAux.Id);
                mealuc.Location = new Point(t_x, t_y);
                mealuc.plusPictureBox.MouseClick += UCClick;
                mealuc.minusPictureBox.MouseClick += UCClick;
                mealuc.mealPictureBox.MouseClick += UCMainPictureClick;

                // Place meal uc in its container
                t_x += MEALWIDTH + SPACEBETWEENMEALS;
                if (compt % horizontalQty == 0)
                {
                    t_y += MEALHEIGHT + SPACEBETWEENMEALS;
                    t_x = pa.Location.X + SPACEBETWEENMEALS + initialX;
                }
                compt++;
                pa.Controls.Add(mealuc);
            }
        }

        /// <summary>
        /// Refresh datagridview data
        /// </summary>
        private void RefreshData()
        {
            BindingList<Product> mealDataSource = new BindingList<Product>(products);
            _addOrderView.metroGrid1.DataSource = mealDataSource;

            _addOrderView.metroGrid1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            _addOrderView.metroGrid1.Columns[0].Width = _addOrderView.metroGrid1.Width / 3;
            _addOrderView.metroGrid1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            _addOrderView.metroGrid1.Columns[1].Width = _addOrderView.metroGrid1.Width / 3;
            _addOrderView.metroGrid1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            _addOrderView.metroGrid1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            _addOrderView.metroGrid1.Columns[2].Width = _addOrderView.metroGrid1.Width / 3;
            _addOrderView.metroGrid1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this._addOrderView.sendOrderBtn.Enabled = products.Count > 0;
        }


        /// <summary>
        /// Create a new order from products added to the list
        /// </summary>
        /// <param name="fPop">form to show web connection</param>
        /// <param name="comment">user comments</param>
        private void createOrder(FormPopUp fPop, string comment)
        {
            // Create new order and set its properties
            OrderDTO order = new OrderDTO();
            order.Id = (_activeOrder != null) ? _activeOrder.Id : 0;
            order.Date = DateTime.Now;
            order.Table_Id = this._tableNumber;
            order.Total = 0;
            order.Commentary = comment;

            // Add all products to order
            this.putMealsInOrder<DrinkDTO>("Drink", ref order);
            this.putMealsInOrder<FoodDTO>("Food", ref order);
            this.putMealsInOrder<MenuDTO>("Menu", ref order);

            // Post the order via web service and get its response code
            int serverResponse = WebserviceConnection.PostAndPutOrder(order);

            // Show post status to user with a pop-up
            if ((serverResponse >= 200) && (serverResponse <= 299))
            {
                fPop.ShowDialog();
            }
            else
            {
                fPop.messageTextLabel.Text = "Error añadiendo el pedido";
                fPop.messageIconPb.Image = Properties.Resources.error_icon;
                fPop.ShowDialog();
            }
        }

        /// <summary>
        /// Put all added products to order
        /// </summary>
        /// <typeparam name="T">type of meal (can be FoodDTO or DrinkDTO)</typeparam>
        /// <param name="mealType">string with meal's type for reflection</param>
        /// <param name="order">order where we're going to add products</param>
        private void putMealsInOrder<T>(string mealType, ref OrderDTO order)
        {
            // Get all products of this type
            List<T> mealsAux = products.OfType<T>().ToList();

            // Get the proper property and initializate it with reflection
            PropertyInfo prop = order.GetType().GetProperty(mealType + "s");
            prop.SetValue(order, new List<T>(), null);

            // Add products to order
            foreach (T m in mealsAux)
            {
                (prop.GetValue(order, null) as List<T>).Add(m);
                order.Total += (Decimal)(m as Product).TotalPrice;
            }
        }

        #endregion

        #region Event handlers
        private void UCMainPictureClick(object sender, EventArgs e)
        {
            List<Panel> panels = _addOrderView.panelControls.Values.ToList();
            List<MealUC> MealUCList = new List<MealUC>();
            panels.ForEach(x => MealUCList.AddRange(x.Controls.OfType<MealUC>()));

            foreach (MealUC m in MealUCList)
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

        private void SendOrder(object sender, EventArgs e)
        {
            FormPopUp fPop = new FormPopUp(true, "El pedido se ha añadido correctamente");

            CommentaryUC c = new CommentaryUC();

            fPop.FormClosed += (senderf, ef) =>
            {
                this._addOrderView.Close();
                
            };

            new FormOpacity(_addOrderView, c);
            c.contBtn.Click += (senderComment, eComment) => { createOrder(fPop, c.commentaryTextBox.Text); };
            c.skipBtn.Click += (senderComment, eComment) => { createOrder(fPop, null); };
        }
        #endregion

    }
}
