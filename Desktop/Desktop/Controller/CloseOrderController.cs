using Desktop.Model;
using Desktop.View;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop.Controller
{
    public class CloseOrderController
    {
        private FormCloseOrder _closeOrderView;

        private OrderDTO _activeOrder;
        private List<Meal> _orderMeals;

        public CloseOrderController(OrderDTO order)
        {
            this._activeOrder = order;
            _closeOrderView = new FormCloseOrder();
            initList();
            refreshData();
            initListeners();
            _closeOrderView.ShowDialog();
        }

        private void initListeners()
        {
            this._closeOrderView.closeBtn.Click += closeOrderBtnClick;
        }

        private void initList()
        {
            _orderMeals = new List<Meal>();
            _orderMeals.AddRange(_activeOrder.Drinks);
            _orderMeals.AddRange(_activeOrder.Foods);
        }

        private void refreshData()
        {
            BindingList<Meal> mealDataSource = new BindingList<Meal>(_orderMeals);
            _closeOrderView.closeTicketGrid.DataSource = mealDataSource;
            //this._closeOrderView.sendOrderBtn.Enabled = meals.Count > 0;

            foreach (DataGridViewColumn c in _closeOrderView.closeTicketGrid.Columns)
            {
                c.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            _closeOrderView.totalGridView.Rows.Add(_activeOrder.Total.ToString());
        }

        private void closeOrderBtnClick(object sender, EventArgs e)
        {
            createPdf();
        }

        private void createPdf()
        {
            System.Drawing.Image orderImage = (System.Drawing.Image)createBitmap(_closeOrderView.closeTicketGrid);
            System.Drawing.Image totalImage = (System.Drawing.Image)createBitmap(_closeOrderView.totalGridView);


            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "PDF Files|*.pdf";
            dlg.FilterIndex = 0;

            string fileName = string.Empty;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                iTextSharp.text.Image im1 = iTextSharp.text.Image.GetInstance(orderImage, System.Drawing.Imaging.ImageFormat.Png);
                iTextSharp.text.Image im2 = iTextSharp.text.Image.GetInstance(totalImage, System.Drawing.Imaging.ImageFormat.Png);

                im1.ScaleAbsolute(505, im1.Height/2);
                im2.ScaleAbsolute(im2.Width/2, im2.Height/2);
                im2.Alignment = iTextSharp.text.Image.ALIGN_RIGHT;

                fileName = dlg.FileName;

                Document myDocument = new Document(iTextSharp.text.PageSize.A4, 45, 45, 42, 35);
                PdfWriter.GetInstance(myDocument, new FileStream(fileName, FileMode.Create));
                myDocument.Open();

                Paragraph p = new Paragraph();
                p.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
                p.Font = FontFactory.GetFont(FontFactory.HELVETICA, 20);
                p.Add("Mesa " + _activeOrder.Table_Id.ToString() + "\n\n");

                myDocument.Add(p);
                myDocument.Add(im1);
                myDocument.Add(new Paragraph("\n"));
                myDocument.Add(im2);
                myDocument.Close();
            }
        }

        private Bitmap createBitmap(DataGridView grid)
        {
            Bitmap bitmap = new Bitmap(grid.Width, grid.Height);
            grid.DrawToBitmap(bitmap, new System.Drawing.Rectangle(0, 0, grid.Width, grid.Height));

            return bitmap;
        }
    }
}
