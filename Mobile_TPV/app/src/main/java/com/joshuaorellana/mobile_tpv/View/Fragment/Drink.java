package com.joshuaorellana.mobile_tpv.View.Fragment;

import android.graphics.drawable.Drawable;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.design.widget.NavigationView;
import android.support.v4.app.Fragment;
import android.support.v4.widget.DrawerLayout;
import android.util.Log;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.TableLayout;
import android.widget.TableRow;
import android.widget.TextView;
import android.widget.Toast;

import com.github.kevinsawicki.http.HttpRequest;
import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import com.joshuaorellana.mobile_tpv.Model.OrderDTO;
import com.joshuaorellana.mobile_tpv.Model.Products.DrinkDTO;
import com.joshuaorellana.mobile_tpv.R;
import com.squareup.picasso.Picasso;

import java.io.InputStream;
import java.lang.reflect.Type;
import java.util.ArrayList;
import java.util.List;

import okhttp3.MediaType;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.RequestBody;
import okhttp3.Response;

import static com.joshuaorellana.mobile_tpv.View.AddOrder.Order;


/**
 * Created by Joshua-OC on 04/05/2017.
 */

public class Drink extends Fragment {

    private String _URL;
    private View rootView;

    private List<DrinkDTO> listDrinks;
    private TableLayout tableLayout;

    private DrawerLayout drawer;
    private NavigationView navigationView;

    private TextView tvProductName;
    private TextView tvQty;
    private ImageView imgBgHeader, imgProduct;

    public Drink() {}

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {

        rootView = inflater.inflate(R.layout.drink_test, container, false);

        initComponents();

        return rootView;

    }

    private void initComponents() {

        //_URL = getString(R.string.URL_localGRANDE);
        //_URL = getString(R.string.URLlocalhostPEQUENA);
        _URL = getString(R.string.URL_localPEQUENA);

        tableLayout = (TableLayout) rootView.findViewById(R.id.menuTableLayout_Drink);
        drawer = (DrawerLayout) rootView.findViewById(R.id.drawer_layout_Drink);
        navigationView = (NavigationView) rootView.findViewById(R.id.nav_view_Drink);
        View navHeader = navigationView.getHeaderView(0);

        tvProductName = (TextView) navHeader.findViewById(R.id.tvProductName);
        tvQty = (TextView) navHeader.findViewById(R.id.tvQty);

        imgBgHeader = (ImageView) navHeader.findViewById(R.id.img_header_bg);
        imgProduct = (ImageView) navHeader.findViewById(R.id.img_Product);

        listDrinks = new ArrayList<>();

        String url = _URL + "api/Drinks";

        new loadDrinks().execute(url);

    }

    private class loadDrinks extends AsyncTask<String, Long, String> {

        protected String doInBackground(String... urls) {

            try {
                return HttpRequest.get(urls[0]).accept("application/json").body();
            } catch (HttpRequest.HttpRequestException exception) {
                return null;
            }
        }

        protected void onPostExecute(String response) {

            Log.e("Response", response);

            listDrinks = getDrinks(response);

            if (!listDrinks.isEmpty()) {
                createDrinkButtons();
            }
        }
    }

    private ArrayList<DrinkDTO> getDrinks(String json) {
        Gson gson = new Gson();
        Type tListType = new TypeToken<ArrayList<DrinkDTO>>() {}.getType();
        Log.e("getDrinks --> ", "SALE");
        return gson.fromJson(json, tListType);

    }

    private void createDrinkButtons() {

        int i = 0;

        while ( i < listDrinks.size()) {

            TableRow tr = new TableRow(getActivity().getApplicationContext());
            tr.setId(i + 25);

            tr.setLayoutParams(new TableRow.LayoutParams(TableRow.LayoutParams.MATCH_PARENT,
                    TableRow.LayoutParams.WRAP_CONTENT));

            for (int j = 0; j < 2; j++) {

                if (i < listDrinks.size()) {

                    final ImageButton btDrink = new ImageButton(getActivity().getApplicationContext());
                    final int auxNum = i;

                    String url = _URL + "Image/Product/" + listDrinks.get(i).getId();

                    Picasso.with(getActivity().getApplicationContext()).load(url).resize(250, 250).into(btDrink);
                    btDrink.setScaleType(ImageButton.ScaleType.CENTER_INSIDE);

                    btDrink.setId(listDrinks.get(i).getId());

                    btDrink.setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(View view) {

                            setUpNavigationView(listDrinks.get(auxNum));

                            drawer.openDrawer(Gravity.LEFT);

                            loadNavigationHeader(listDrinks.get(auxNum), btDrink.getDrawable());

                        }
                    });

                    tr.addView(btDrink);

                }

                i++;

            }

            tableLayout.addView(tr, new TableLayout.LayoutParams(TableLayout.LayoutParams.MATCH_PARENT,
                    TableLayout.LayoutParams.WRAP_CONTENT));

        }

    }

    private void setUpNavigationView(final DrinkDTO product) {
        navigationView.setNavigationItemSelectedListener(new NavigationView.OnNavigationItemSelectedListener() {
            @Override
            public boolean onNavigationItemSelected(MenuItem menuItem) {

                switch (menuItem.getItemId()) {
                    case R.id.nav_add:
                        product.setQuantity(product.getQuantity() + 1);

                        tvQty.setText("Cantidad: " + product.getQuantity());

                        break;
                    case R.id.nav_remove:

                        if (product.getQuantity() < 0 )
                            product.setQuantity(0);

                        tvQty.setText("Cantidad: " + product.getQuantity());

                        break;

                    case R.id.nav_accept:

                        Order.addDrink(product);

                        break;

                    case R.id.nav_decline:

                        product.setQuantity(0);

                        break;

                    case R.id.nav_sendorder:

                        String url = _URL + "api/Orders/Manager";

                        new sendOrder().execute(url);






                }

                if (menuItem.isChecked()) {
                    menuItem.setChecked(false);
                } else {
                    menuItem.setChecked(true);
                }
                menuItem.setChecked(true);

                for (int i = 0; i < Order.getListDrinks().size(); i++) {
                    Log.e("Order listDrinks --> ", String.valueOf(Order.getListDrinks().size()));
                }

                return true;
            }

        });
    }

    private void loadNavigationHeader(DrinkDTO productName, Drawable img) {

        tvProductName.setText(productName.getName());
        tvQty.setText("Cantidad: " + productName.getQuantity());

        imgProduct.setImageDrawable(img);
        imgProduct.setScaleType(ImageView.ScaleType.CENTER_INSIDE);

    }

    private static String post(String url, OrderDTO order) {

        InputStream inputStream = null;
        String result="";

        Log.e("URL post --->", url);

        try {

            String json;

            Gson gson = new Gson();
            json = gson.toJson(order);

            OkHttpClient client = new OkHttpClient();

            MediaType mediaType = MediaType.parse("application/json");
            RequestBody body = RequestBody.create(mediaType, json);
            Request request = new Request.Builder()
                    .url(url)
                    .post(body)
                    .addHeader("content-type", "application/json")
                    .build();

            Response response = client.newCall(request).execute();

        } catch (Exception err) {

            Log.e("Error", err.toString());

        }

        return null;

    }

    private class sendOrder extends AsyncTask<String, Void, String> {
        @Override
        protected String doInBackground(String... urls) {


            return post(urls[0], Order);
        }
        // onPostExecute displays the results of the AsyncTask.
        @Override
        protected void onPostExecute(String result) {
            //Toast.makeText(getBaseContext(), "Data Sent!", Toast.LENGTH_LONG).show();
            Toast.makeText(getActivity(), "OK!", Toast.LENGTH_SHORT).show();
        }
    }



}
