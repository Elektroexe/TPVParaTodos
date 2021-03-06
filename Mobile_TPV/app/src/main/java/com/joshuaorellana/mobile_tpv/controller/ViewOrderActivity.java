package com.joshuaorellana.mobile_tpv.controller;

import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.widget.TextView;

import com.joshuaorellana.mobile_tpv.controller.common.WebService;
import com.joshuaorellana.mobile_tpv.model.business.OrderDTO;
import com.joshuaorellana.mobile_tpv.model.business.DrinkDTO;
import com.joshuaorellana.mobile_tpv.model.business.FoodDTO;
import com.joshuaorellana.mobile_tpv.model.business.MenuDTO;
import com.joshuaorellana.mobile_tpv.R;

import java.util.List;

public class ViewOrderActivity extends AppCompatActivity {

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
        tvDrinkList = (TextView) findViewById(R.id.tv_DrinkList);
        tvFoodList = (TextView) findViewById(R.id.tv_FoodList);
        tvMenuList = (TextView) findViewById(R.id.tv_MenuList);
        Thread getOrder = new Thread(new Runnable() {
            @Override
            public void run() {
                Order = WebService.GetOrder(SelectedTableActivity.auxTable.getId());
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
