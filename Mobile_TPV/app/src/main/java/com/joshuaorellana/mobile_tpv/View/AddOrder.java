package com.joshuaorellana.mobile_tpv.View;

import android.os.AsyncTask;
import android.os.Bundle;
import android.support.design.widget.TabLayout;
import android.support.v4.view.ViewPager;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;

import com.github.kevinsawicki.http.HttpRequest;
import com.google.gson.Gson;
import com.joshuaorellana.mobile_tpv.Controller.ViewPagerAdapter;
import com.joshuaorellana.mobile_tpv.Model.OrderDTO;
import com.joshuaorellana.mobile_tpv.R;
import com.joshuaorellana.mobile_tpv.View.Fragment.Drink;
import com.joshuaorellana.mobile_tpv.View.Fragment.Food;
import com.joshuaorellana.mobile_tpv.View.Fragment.Menu;

import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.TimeZone;

import static com.joshuaorellana.mobile_tpv.View.SelectedTable.auxTable;
import static com.joshuaorellana.mobile_tpv.View.Tables._URL;

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

        Bundle auxIntent = getIntent().getExtras();

        boolean modify = auxIntent.getBoolean("modify");

        if (modify) {

            Log.e("ModifyOrder --> ", "OK!");

            String url = _URL + "api/Orders/Manager/" + auxTable.getId();

            new loadContent().execute(url);

        } else {

            Log.e("AddOrder -->", "OK!");

            Order = new OrderDTO(auxTable.getId(), date);
        }

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

        }

    }

    private OrderDTO getTables(String json) {

        Gson gson = new Gson();

        //JsonParser parser = new JsonParser();
        //JsonObject jsonObj = parser.parse(json).getAsJsonObject();

        //Type tListType = new TypeToken<ArrayList<OrderDTO>>() {}.getType();
        return gson.fromJson(json, OrderDTO.class);

    }


    private void setupViewPager(ViewPager viewPager) {

        ViewPagerAdapter adapter = new ViewPagerAdapter(getSupportFragmentManager());

        adapter.addFragment(new Drink(), "BEBIDAS");
        adapter.addFragment(new Food("Meat"), "CARNE");
        adapter.addFragment(new Food("Starter"), "ENTRANTES");
        adapter.addFragment(new Menu(), "MENÚS");

        viewPager.setAdapter(adapter);

    }
}
