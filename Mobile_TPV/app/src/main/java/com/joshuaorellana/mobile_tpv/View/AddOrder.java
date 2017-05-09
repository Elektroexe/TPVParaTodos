package com.joshuaorellana.mobile_tpv.View;

import android.os.Bundle;
import android.support.design.widget.TabLayout;
import android.support.v4.view.ViewPager;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;

import com.joshuaorellana.mobile_tpv.Controller.ViewPagerAdapter;
import com.joshuaorellana.mobile_tpv.Model.OrderDTO;
import com.joshuaorellana.mobile_tpv.R;
import com.joshuaorellana.mobile_tpv.View.Fragment.Drink;
import com.joshuaorellana.mobile_tpv.View.Fragment.Food;

import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.TimeZone;

import static com.joshuaorellana.mobile_tpv.View.SelectedTable.auxTable;

public class AddOrder extends AppCompatActivity {

    private TabLayout tabLayout;
    private ViewPager viewPager;
    private ViewPagerAdapter adapter;

    public static OrderDTO Order;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_add_order);

        initComponents();

    }

    private void initComponents() {

        viewPager = (ViewPager) findViewById(R.id.viewpager);
        setupViewPager(viewPager);

        tabLayout = (TabLayout) findViewById(R.id.tabs);
        tabLayout.setupWithViewPager(viewPager);

        viewPager.getCurrentItem();


        TimeZone tz = TimeZone.getTimeZone("UTC");
        DateFormat df = new SimpleDateFormat("yyyy-MM-dd'T'HH:mmZ"); // Quoted "Z" to indicate UTC, no timezone offset
        df.setTimeZone(tz);
        String date = df.format(new Date());

        Log.e("Date --> ", date);

        Order = new OrderDTO(auxTable.getId(), date);

    }

    private void setupViewPager(ViewPager viewPager) {

        ViewPagerAdapter adapter = new ViewPagerAdapter(getSupportFragmentManager());

        adapter.addFragment(new Drink(), "BEBIDAS");
        adapter.addFragment(new Food("Meat"), "CARNE");
        adapter.addFragment(new Food("Meat"), "CARNE");
        adapter.addFragment(new Food("Starter"), "ENTRANTES");

        //adapter.addFragment(new Menu(), "MENÚS");

        viewPager.setAdapter(adapter);

    }
}
