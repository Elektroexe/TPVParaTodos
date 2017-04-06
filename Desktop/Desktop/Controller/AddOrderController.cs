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

        #region Constants
        private const int MEALWIDTH = 150;
        private const int MEALHEIGHT = 150;
        private const int SPACEBETWEENMEALS = 35;
        #endregion

        #endregion

        #region Static Fields
        // All of meals which take part of the order
        public static List<Meal> meals;
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
        /// <summary>
        /// Initialize all relevant items
        /// </summary>
        /// <param name="tableNumber">table to which meals will be assigned</param>
        /// <param name="activeOrder">table's active order (if it's null means no active order)</param>
        private void initControllerItems(int tableNumber, OrderDTO activeOrder)
        {
            meals = new List<Meal>();
            _addOrderView = new FormAddOrder();

            if (activeOrder != null)
            {
                fillMealsFromOrder(activeOrder);
                _addOrderView.Text = "Modificar comanda";
            }

            initMeals();
            _addOrderView.Show();
            _tableNumber = tableNumber;
            _activeOrder = activeOrder;
            initListeners();
        }

        /// <summary>
        /// Add all meals which were in the active order to meal's list
        /// </summary>
        /// <param name="activeOrder">table's active order</param>
        private void fillMealsFromOrder(OrderDTO activeOrder)
        {
            meals.AddRange(activeOrder.Foods);
            meals.AddRange(activeOrder.Drinks);

            // TO_DO: Hardcoded
            meals.ForEach(m => m.Quantity = 1);
        }

        /// <summary>
        /// Initialize all necessary view listeners
        /// </summary>
        private void initListeners()
        {
            _addOrderView.sendOrderBtn.Click += SendOrder;
        }

        /// <summary>
        /// Initialize all meals (visible objects and it's DTOs classes)
        /// </summary>
        private void initMeals()
        {   
            // Meals container size
            int twidth = _addOrderView.controlTabPanel.Width;
            int theight = _addOrderView.controlTabPanel.Height;

            // Calculate how many elements fit in each row
            int horizontalQty = twidth / (MEALWIDTH + SPACEBETWEENMEALS);
            int totalSpace = (MEALWIDTH + SPACEBETWEENMEALS) * horizontalQty + SPACEBETWEENMEALS;
            if (totalSpace > twidth) horizontalQty--;
            totalSpace = (MEALWIDTH + SPACEBETWEENMEALS) * horizontalQty + SPACEBETWEENMEALS;
            int initialX = (_addOrderView.controlTabPanel.Width - SystemInformation.VerticalScrollBarThumbHeight - totalSpace) / 2;

            // Set initial position for the first meal
            int x = _addOrderView.controlTabPanel.Location.X + SPACEBETWEENMEALS + initialX;
            int y = _addOrderView.controlTabPanel.Location.Y + SPACEBETWEENMEALS;

            // Get all meals from the database
            List<Meal> meals = new List<Meal>();
            meals.AddRange(WebserviceConnection.getMeal<FoodDTO>("food"));
            meals.AddRange(WebserviceConnection.getMeal<DrinkDTO>("drink"));

            // Create all meal object and place in its container
            this.createAndPlaceMeal(meals, x, y, initialX, horizontalQty);
        }

        /// <summary>
        /// Create all meals object and place in its container
        /// </summary>
        /// <param name="t_meals">All meals to be placed</param>
        /// <param name="t_x">Initial x position</param>
        /// <param name="t_y">Initial y position</param>
        /// <param name="initialX">Initial x without spaces</param>
        /// <param name="horizontalQty">Meals per row</param>
        private void createAndPlaceMeal(List<Meal> t_meals,
                                        int t_x,
                                        int t_y,
                                        int initialX,
                                        int horizontalQty)
        {
            int compt = 1;
            foreach (Meal m in t_meals)
            {
                // Get meal object from current order(if it's null we keep the default value)
                Meal mAux = meals.Where(x => x.Id == m.Id).FirstOrDefault() ?? m;

                // Prepare reflection string for future instanciations and invocations
                string meal = (mAux.GetType() == typeof(FoodDTO)) ? "Food" : "Drink";

                // Instanciate and initializate Meal user control with the type specified on the bellow string
                MealUC mealuc = (MealUC)Activator.CreateInstance(null, "Desktop.UserControls." + meal + "MealUC").Unwrap();
                mealuc.SetDTO(m);
                mealuc.mealPictureBox.Image = WebserviceConnection.getImage(m.Id, meal);
                mealuc.Location = new Point(t_x, t_y);
                mealuc.plusPictureBox.MouseClick += UCClick;
                mealuc.minusPictureBox.MouseClick += UCClick;
                mealuc.mealPictureBox.MouseClick += UCMainPictureClick;
                
                // Place meal uc in its container
                t_x += MEALWIDTH + SPACEBETWEENMEALS;
                if (compt % horizontalQty == 0)
                {
                    t_y += MEALHEIGHT + SPACEBETWEENMEALS;
                    t_x = _addOrderView.controlTabPanel.Location.X + SPACEBETWEENMEALS + initialX;
                }
                compt++;
                _addOrderView.controlTabPanel.Controls.Add(mealuc);
            }
        }

        /// <summary>
        /// Refresh datagridview data
        /// </summary>
        private void RefreshData()
        {
            BindingList<Meal> mealDataSource = new BindingList<Meal>(meals);
            _addOrderView.metroGrid1.DataSource = mealDataSource;

            this._addOrderView.sendOrderBtn.Enabled = meals.Count > 0;
        }


        /// <summary>
        /// Create a new order from meals added to the list
        /// </summary>
        /// <param name="fPop">form to show web connection</param>
        /// <param name="comment">user comments</param>
        private void createOrder(FormPopUp fPop, string comment)
        {
            // Create new order and set its properties
            OrderDTO order = new OrderDTO();
            order.Date = DateTime.Now;
            order.Table_Id = this._tableNumber;
            order.Total = 0;
            order.Commentary = comment;

            // Add all meals to order
            this.putMealsInOrder<DrinkDTO>("Drink", ref order);
            this.putMealsInOrder<FoodDTO>("Food", ref order);

            // Post the order via web service and get its response code
            int serverResponse = WebserviceConnection.PostOrder(order);

            // Show post status to user with a pop-up
            if ((serverResponse >= 200) && (serverResponse <= 299))
            {
                fPop.ShowDialog();
            }
        }

        /// <summary>
        /// Put all added meals to order
        /// </summary>
        /// <typeparam name="T">type of meal (can be FoodDTO or DrinkDTO)</typeparam>
        /// <param name="mealType">string with meal's type for reflection</param>
        /// <param name="order">order where we're going to add meals</param>
        private void putMealsInOrder<T>(string mealType, ref OrderDTO order)
        {
            // Get all meals of this type
            List<T> mealsAux = meals.OfType<T>().ToList();

            // Get the proper property and initializate it with reflection
            PropertyInfo prop = order.GetType().GetProperty(mealType + "s");
            prop.SetValue(order, new List<T>(), null);

            // Add meals to order
            foreach (T m in mealsAux)
            {
                for (int i = 0; i < (m as Meal).Quantity; i++)
                {
                    (prop.GetValue(order, null) as List<T>).Add(m);
                }
                order.Total += (Decimal)(m as Meal).TotalPrice;
            }
        }

        #endregion

        #region Event handlers
        private void UCMainPictureClick(object sender, EventArgs e)
        {
            List<MealUC> MealUCList = _addOrderView.controlTabPanel.Controls.OfType<MealUC>().ToList();
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
            FormPopUp fPop = new FormPopUp();

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
