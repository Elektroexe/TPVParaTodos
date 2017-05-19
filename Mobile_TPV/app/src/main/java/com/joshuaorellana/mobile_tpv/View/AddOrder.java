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
import com.joshuaorellana.mobile_tpv.Controller.WebService;
import com.joshuaorellana.mobile_tpv.Model.OrderDTO;
import com.joshuaorellana.mobile_tpv.R;
import com.joshuaorellana.mobile_tpv.View.Fragment.Drink;
import com.joshuaorellana.mobile_tpv.View.Fragment.Food;
import com.joshuaorellana.mobile_tpv.View.Fragment.Menu;

import java.io.IOException;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.TimeZone;

import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.Response;

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
        DateFormat df = new SimpleDateFormat("yyyy-MM-dd'T'HH:mmZ");
        df.setTimeZone(tz);
        String date = df.format(new Date());

        Bundle auxIntent = getIntent().getExtras();

        boolean modify = auxIntent.getBoolean("modify");

        if (modify) {

            String url = _URL + "api/Orders/Manager/" + auxTable.getId();

            new loadContent().execute(url);

        } else {
            Order = new OrderDTO(auxTable.getId(), date);
        }
    }

    private class loadContent extends AsyncTask<String, Long, String > {

        protected String doInBackground(String... urls) {

            try {

                OkHttpClient client = new OkHttpClient();
                Request request = new Request.Builder()
                        .url(urls[0])
                        .get()
                        .addHeader("Authorization", WebService.token)
                        .build();
                Response response = client.newCall(request).execute();

                return response.body().string();

            } catch (HttpRequest.HttpRequestException | IOException err) {
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
        return gson.fromJson(json, OrderDTO.class);

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
