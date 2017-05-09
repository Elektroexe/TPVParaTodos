package com.joshuaorellana.mobile_tpv.View;

import android.os.Bundle;
import android.support.design.widget.TabLayout;
import android.support.v4.view.ViewPager;
import android.support.v7.app.AppCompatActivity;

import com.joshuaorellana.mobile_tpv.Controller.ViewPagerAdapter;
import com.joshuaorellana.mobile_tpv.R;
import com.joshuaorellana.mobile_tpv.View.Fragment.Appetizer;
import com.joshuaorellana.mobile_tpv.View.Fragment.Dessert;
import com.joshuaorellana.mobile_tpv.View.Fragment.Drink;
import com.joshuaorellana.mobile_tpv.View.Fragment.Meat;
import com.joshuaorellana.mobile_tpv.View.Fragment.Menu;
import com.joshuaorellana.mobile_tpv.View.Fragment.Seafood;

public class AddOrder extends AppCompatActivity {

    private TabLayout tabLayout;
    private ViewPager viewPager;
    private ViewPagerAdapter adapter;

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


    }

    private void setupViewPager(ViewPager viewPager) {

        ViewPagerAdapter adapter = new ViewPagerAdapter(getSupportFragmentManager());
        adapter.addFragment(new Drink(), "BEBIDAS");
        adapter.addFragment(new Menu(), "MENÚS");
        adapter.addFragment(new Appetizer(), "ENTRANTES");
        adapter.addFragment(new Meat(), "CARNES");
        adapter.addFragment(new Seafood(), "PESCADOS");
        adapter.addFragment(new Dessert(), "POSTRES");

        viewPager.setAdapter(adapter);



    }
}
