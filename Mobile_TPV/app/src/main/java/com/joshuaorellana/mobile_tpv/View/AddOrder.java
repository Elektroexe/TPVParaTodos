package com.joshuaorellana.mobile_tpv.View;

import android.content.SharedPreferences;
import android.database.Cursor;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.Debug;
import android.support.design.widget.TabLayout;
import android.support.v4.view.ViewPager;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;

import com.github.kevinsawicki.http.HttpRequest;
import com.google.gson.Gson;
import com.joshuaorellana.mobile_tpv.Controller.ViewPagerAdapter;
import com.joshuaorellana.mobile_tpv.Controller.WebService;
import com.joshuaorellana.mobile_tpv.Model.OrderDTO;
import com.joshuaorellana.mobile_tpv.Model.ProductDTO;
import com.joshuaorellana.mobile_tpv.Model.Products.DrinkDTO;
import com.joshuaorellana.mobile_tpv.Model.Products.FoodDTO;
import com.joshuaorellana.mobile_tpv.Model.Update;
import com.joshuaorellana.mobile_tpv.Model.persistence.ProductsConversor;
import com.joshuaorellana.mobile_tpv.Model.persistence.ProductsSQLiteHelper;
import com.joshuaorellana.mobile_tpv.R;
import com.joshuaorellana.mobile_tpv.View.Fragment.Drink;
import com.joshuaorellana.mobile_tpv.View.Fragment.Food;
import com.joshuaorellana.mobile_tpv.View.Fragment.Menu;

import java.lang.reflect.Array;
import java.lang.reflect.Method;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.List;
import java.util.TimeZone;

import static com.joshuaorellana.mobile_tpv.View.SelectedTable.auxTable;
import static com.joshuaorellana.mobile_tpv.View.Tables._URL;

public class AddOrder extends AppCompatActivity {

    private TabLayout tabLayout;
    private ViewPager viewPager;
    private ViewPagerAdapter adapter;

    public static OrderDTO Order;
    private int versionDB;

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
                ProductsSQLiteHelper helper = new ProductsSQLiteHelper(getApplicationContext(), "Products", null, 1);
                ProductsConversor conversor = new ProductsConversor(helper);
                Update newProducts = WebService.CheckDB(versionDB);
                setVersionDB(versionDB + newProducts.getFoods().size() + newProducts.getMenus().size() + newProducts.getDrinks().size());
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

        boolean modify = auxIntent.getBoolean("modify");

        if (modify) {

            Log.e("ModifyOrder --> ", "OK!");
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

        } else {
            Order = new OrderDTO(auxTable.getId(), date);
        }

    }

    private void setupViewPager(ViewPager viewPager) {

        ViewPagerAdapter adapter = new ViewPagerAdapter(getSupportFragmentManager());

        adapter.addFragment(new Drink(), "BEBIDAS");
        adapter.addFragment(new Food("Starter"), "ENTRANTES");
        adapter.addFragment(new Food("Main"), "PRINCIPAL");
        adapter.addFragment(new Food("Dessert"), "POSTRES");
        adapter.addFragment(new Menu(), "MENÚS");

        viewPager.setAdapter(adapter);

    }
}
