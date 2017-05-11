package com.joshuaorellana.mobile_tpv.View;

import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.widget.TextView;

import com.github.kevinsawicki.http.HttpRequest;
import com.google.gson.Gson;
import com.joshuaorellana.mobile_tpv.Model.OrderDTO;
import com.joshuaorellana.mobile_tpv.Model.Products.DrinkDTO;
import com.joshuaorellana.mobile_tpv.Model.Products.FoodDTO;
import com.joshuaorellana.mobile_tpv.Model.Products.MenuDTO;
import com.joshuaorellana.mobile_tpv.R;

import java.util.List;

import static com.joshuaorellana.mobile_tpv.View.SelectedTable.auxTable;
import static com.joshuaorellana.mobile_tpv.View.Tables._URL;

public class ViewOrder extends AppCompatActivity {

    private TextView tvText;
    private TextView tvDrinkOrder;

    private OrderDTO Order;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_view_order);

        initComponents();

    }

    private void initComponents() {


        String url = _URL + "api/Orders/Manager/" + auxTable.getId();

        tvText = (TextView) findViewById(R.id.tv_Text);
        tvDrinkOrder = (TextView) findViewById(R.id.tvDrinkOrder);

        tvText.setText("");
        tvDrinkOrder.setText("");

        new loadContent().execute(url);

    }

    private class loadContent extends AsyncTask<String, Long, String > {

        protected String doInBackground(String... urls) {

            try {
                return HttpRequest.get(urls[0]).accept("application/json").body();
            } catch (HttpRequest.HttpRequestException err) {
                Log.e("ERROR HttpRequest: ", err.toString());
            }

            return null;

        }

        protected void onPostExecute(String response) {

            Order = getTables(response);

            showOrder();

        }

    }

    private OrderDTO getTables(String json) {

        Gson gson = new Gson();

        //JsonParser parser = new JsonParser();
        //JsonObject jsonObj = parser.parse(json).getAsJsonObject();

        //Type tListType = new TypeToken<ArrayList<OrderDTO>>() {}.getType();
        return gson.fromJson(json, OrderDTO.class);

    }

    private void showOrder() {

        List<DrinkDTO> drinks = Order.getListDrinks();
        List<FoodDTO> foods = Order.getListFoods();
        List<MenuDTO> menus = Order.getListMenus();

        if (!drinks.isEmpty()) {

            tvDrinkOrder.setText("Bebidas: \n");

            for (int i = 0; i < drinks.size(); i++) {
                tvDrinkOrder.setText(tvDrinkOrder.getText() + drinks.get(i).getName() + "\t\t - \t\t Cantidad: " +
                        drinks.get(i).getQuantity()+"\n");
            }

            tvDrinkOrder.setText(tvDrinkOrder.getText() + "\n");
        }

        if (!foods.isEmpty()) {

            tvDrinkOrder.setText(tvDrinkOrder.getText() + "Comida: \n");

            for (int i = 0; i < foods.size(); i++) {
                tvDrinkOrder.setText(tvDrinkOrder.getText() + foods.get(i).getName() + "\t\t - \t\t Cantidad: " +
                        foods.get(i).getQuantity()+"\n");
            }

            tvDrinkOrder.setText(tvDrinkOrder.getText() + "\n");

        }

        if (!menus.isEmpty()) {

            tvDrinkOrder.setText(tvDrinkOrder.getText() + "Menus: \n");

            for (int i = 0; i < menus.size(); i++) {
                tvDrinkOrder.setText(tvDrinkOrder.getText() + menus.get(i).getName() + "\t\t - \t\t Cantidad: " +
                        menus.get(i).getQuantity()+"\n");
            }

            tvDrinkOrder.setText(tvDrinkOrder.getText() + "\n");

        }


    }


}
