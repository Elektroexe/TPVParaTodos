package com.joshuaorellana.mobile_tpv.controller;

import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.design.widget.TabLayout;
import android.support.v4.view.ViewPager;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;

import com.joshuaorellana.mobile_tpv.controller.common.WebService;
import com.joshuaorellana.mobile_tpv.model.business.OrderDTO;
import com.joshuaorellana.mobile_tpv.model.Update;
import com.joshuaorellana.mobile_tpv.model.persistence.ProductsConversor;
import com.joshuaorellana.mobile_tpv.model.persistence.ProductsSQLiteHelper;
import com.joshuaorellana.mobile_tpv.R;
import com.joshuaorellana.mobile_tpv.controller.fragment.DrinkFragment;
import com.joshuaorellana.mobile_tpv.controller.fragment.FoodFragment;
import com.joshuaorellana.mobile_tpv.controller.fragment.MenuFragment;

import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.TimeZone;

public class AddOrderActivity extends AppCompatActivity {

    private TabLayout tabLayout;
    private ViewPager viewPager;

    public static OrderDTO Order;
    private int versionDB;

    boolean modify;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_add_order);
        updateDB();
        initComponents();
    }

    private void updateDB(){
        versionDB = getVersionDB();
        Thread update = new Thread(new Runnable() {
            @Override
            public void run() {
                ProductsSQLiteHelper helper = new ProductsSQLiteHelper(getApplicationContext(), "product", null, 1);
                ProductsConversor conversor = new ProductsConversor(helper);
                Update newProducts = WebService.CheckDB(versionDB);
                setVersionDB(newProducts.getNewVersion());
                conversor.updateProducts(newProducts);
                conversor.closeConnection();
            }
        });
        update.start();
        try {
            update.join();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
    }

    private int getVersionDB(){
        SharedPreferences shared = getSharedPreferences(getString(R.string.preferencesFile), MODE_PRIVATE);
        return shared.getInt(getString(R.string.versionDB), 0);
    }

    private void setVersionDB(int newVersionDB){
        SharedPreferences shared = getSharedPreferences(getString(R.string.preferencesFile), MODE_PRIVATE);
        SharedPreferences.Editor editor = shared.edit();
        editor.putInt(getString(R.string.versionDB), newVersionDB);
    }

    private void initComponents() {
        viewPager = (ViewPager) findViewById(R.id.viewpager);
        setupViewPager(viewPager);
        tabLayout = (TabLayout) findViewById(R.id.tabs);
        tabLayout.setupWithViewPager(viewPager);
        viewPager.getCurrentItem();
        TimeZone tz = TimeZone.getTimeZone("UTC");
        DateFormat df = new SimpleDateFormat("yyyy-MM-dd'T'HH:mmZ");
        df.setTimeZone(tz);
        String date = df.format(new Date());
        Bundle auxIntent = getIntent().getExtras();
        modify = auxIntent.getBoolean("modify");
        if (modify) {
            Log.e("ModifyOrder --> ", "OK!");
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
        } else {
            Order = new OrderDTO(SelectedTableActivity.auxTable.getId(), date);
        }
    }

    private void setupViewPager(ViewPager viewPager) {
        ViewPagerAdapter adapter = new ViewPagerAdapter(getSupportFragmentManager());
        adapter.addFragment(new DrinkFragment(modify), "BEBIDAS");
        adapter.addFragment(new FoodFragment(modify, "Starter"), "ENTRANTES");
        adapter.addFragment(new FoodFragment(modify, "Main"), "PRINCIPAL");
        adapter.addFragment(new FoodFragment(modify, "Dessert"), "POSTRES");
        adapter.addFragment(new MenuFragment(modify), "MENÚS");
        viewPager.setAdapter(adapter);
    }
}
