package com.joshuaorellana.mobile_tpv.View;

import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.widget.TextView;

import com.github.kevinsawicki.http.HttpRequest;
import com.google.gson.Gson;
import com.joshuaorellana.mobile_tpv.Controller.WebService;
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

    private TextView tvDrinkList;
    private TextView tvFoodList;
    private TextView tvMenuList;

    private OrderDTO Order;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_view_order);

        initComponents();

    }

    private void initComponents() {

//        String url = _URL + "api/Orders/Manager/" + auxTable.getId();

        tvDrinkList = (TextView) findViewById(R.id.tv_DrinkList);
        tvFoodList = (TextView) findViewById(R.id.tv_FoodList);
        tvMenuList = (TextView) findViewById(R.id.tv_MenuList);


//        new loadContent().execute(url);

        Thread getOrder = new Thread(new Runnable() {
            @Override
            public void run() {
                Order = WebService.GetOrder(auxTable.getId());
            }
        });
        getOrder.start();
        try {
            getOrder.join();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
        showOrder();

    }

//    private class loadContent extends AsyncTask<String, Long, String > {
//
//        protected String doInBackground(String... urls) {
//
//            try {
//                return HttpRequest.get(urls[0]).accept("application/json").body();
//            } catch (HttpRequest.HttpRequestException err) {
//                Log.e("ERROR HttpRequest: ", err.toString());
//            }
//
//            return null;
//        }
//
//        protected void onPostExecute(String response) {
//
//            Order = getTables(response);
//
//            showOrder();
//
//        }
//    }
//
//    private OrderDTO getTables(String json) {
//
//        Gson gson = new Gson();
//        return gson.fromJson(json, OrderDTO.class);
//
//    }

    private void showOrder() {

        List<DrinkDTO> drinks = Order.getListDrinks();
        List<FoodDTO> foods = Order.getListFoods();
        List<MenuDTO> menus = Order.getListMenus();

        if (!drinks.isEmpty()) {

            for (int i = 0; i < drinks.size(); i++) {

                DrinkDTO auxD = drinks.get(i);

                tvDrinkList.setText(tvDrinkList.getText() + auxD.getName() + "\t\t -- \t\t" + auxD.getQuantity() + "\n");
            }
        } else {
            tvDrinkList.setText("No hay.");
        }

        if (!foods.isEmpty()) {

            for (int i = 0; i < foods.size(); i++) {
                FoodDTO auxF = foods.get(i);

                tvFoodList.setText(tvFoodList.getText() + auxF.getName() + "\t\t -- \t\t" + auxF.getQuantity() + "\n");
            }
        } else {
            tvFoodList.setText("No hay.");
        }

        if (!menus.isEmpty()) {

            for (int i = 0; i < menus.size(); i++) {
                MenuDTO auxM = menus.get(i);

                tvMenuList.setText(tvMenuList.getText() + auxM.getName() + "\t\t -- \t\t" + auxM.getQuantity() + "\n");
            }
        } else {
            tvMenuList.setText("No hay.");
        }

    }
}
