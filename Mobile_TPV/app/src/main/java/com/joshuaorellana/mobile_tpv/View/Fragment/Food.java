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
import com.joshuaorellana.mobile_tpv.Controller.WebService;
import com.joshuaorellana.mobile_tpv.Model.OrderDTO;
import com.joshuaorellana.mobile_tpv.Model.Products.DrinkDTO;
import com.joshuaorellana.mobile_tpv.Model.Products.FoodDTO;
import com.joshuaorellana.mobile_tpv.Model.persistence.ProductsConversor;
import com.joshuaorellana.mobile_tpv.Model.persistence.ProductsSQLiteHelper;
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
import static com.joshuaorellana.mobile_tpv.View.Tables._URL;

/**
 * Created by Joshua-OC on 08/05/2017.
 */

public class Food extends Fragment {

    private String _Title;

    private View rootView;

    private List<FoodDTO> listFoods;
    private TableLayout tableLayout;

    private DrawerLayout drawer;
    private NavigationView navigationView;

    private TextView tvProductName;
    private TextView tvQty;
    private ImageView imgBgHeader, imgProduct;

    public Food(String title) {
        this._Title = title;
    }

    public void onCreate(Bundle savedInstanceState, String title) {

        super.onCreate(savedInstanceState);
        this._Title = title;

    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {

        rootView = inflater.inflate(R.layout.drink_test, container, false);

        initComponents();

        return rootView;

    }

    private void initComponents() {

        tableLayout = (TableLayout) rootView.findViewById(R.id.menuTableLayout_Drink);
        drawer = (DrawerLayout) rootView.findViewById(R.id.drawer_layout_Drink);
        navigationView = (NavigationView) rootView.findViewById(R.id.nav_view_Drink);
        View navHeader = navigationView.getHeaderView(0);

        tvProductName = (TextView) navHeader.findViewById(R.id.tvProductName);
        tvQty = (TextView) navHeader.findViewById(R.id.tvQty);

        imgBgHeader = (ImageView) navHeader.findViewById(R.id.img_header_bg);
        imgProduct = (ImageView) navHeader.findViewById(R.id.img_Product);


        listFoods = new ArrayList<>();
//
//        String url = _URL + "api/Foods";
//
//        new loadFoods().execute(url);

//        List<FoodDTO> auxList = getFood(response);

        ProductsSQLiteHelper helper = new ProductsSQLiteHelper(getActivity().getApplicationContext(), "Products", null, 1);
        ProductsConversor conversor = new ProductsConversor(helper);
        List<FoodDTO> auxList = conversor.getProducts(FoodDTO.class);
        conversor.closeConnection();

        for (FoodDTO auxFood : auxList) {
            if (auxFood.getFamilyDish().equals(_Title))
                listFoods.add(auxFood);
        }

        if (!listFoods.isEmpty()) {
            createFoodsButtons();

            for (FoodDTO aux : Order.getListFoods()) {
                for (int i = 0; i < listFoods.size(); i++) {

                    FoodDTO auxB = listFoods.get(i);
                    if (aux.getName().equals(auxB.getName()))
                        auxB.setQuantity(aux.getQuantity());

                }
            }

        }

    }

//    private class loadFoods extends AsyncTask<String, Long, String> {
//
//        protected String doInBackground(String... urls) {
//
//            try {
//                return HttpRequest.get(urls[0]).accept("application/json").body();
//            } catch (HttpRequest.HttpRequestException execption) {
//                return null;
//            }
//
//        }
//
//        protected void onPostExecute(String response) {
//
//            ArrayList<FoodDTO> auxList = getFood(response);
//
//            for (FoodDTO auxFood : auxList ) {
//                if (auxFood.getFamilyDish().equals(_Title))
//                    listFoods.add(auxFood);
//            }
//
//            if(!listFoods.isEmpty()) {
//                createFoodsButtons();
//
//                for (FoodDTO aux : Order.getListFoods()) {
//                    for (int i = 0; i < listFoods.size(); i++) {
//
//                        FoodDTO auxB = listFoods.get(i);
//                        if (aux.getName().equals(auxB.getName()))
//                            auxB.setQuantity(aux.getQuantity());
//
//                    }
//                }
//
//            }
//        }
//
//    }
//
//    private ArrayList<FoodDTO> getFood(String json) {
//        Gson gson = new Gson();
//        Type tListType = new TypeToken<ArrayList<FoodDTO>>() {}.getType();
//        return gson.fromJson(json, tListType);
//    }

    private void createFoodsButtons() {
        int i = 0;

        while (i < listFoods.size()) {

            TableRow tr = new TableRow(getActivity().getApplicationContext());
            tr.setId(i + 25);

            tr.setLayoutParams(new TableRow.LayoutParams(TableRow.LayoutParams.MATCH_PARENT,
                    TableRow.LayoutParams.WRAP_CONTENT));

            for (int j = 0; j < 2; j++) {

                if (i < listFoods.size()) {

                    final ImageButton btMeat = new ImageButton(getActivity().getApplicationContext());
                    final int auxNum = i;

                    String url = _URL + "Image/Product/" + listFoods.get(i).getId();

                    Picasso.with(getActivity().getApplicationContext()).load(url).resize(250, 250).into(btMeat);
                    btMeat.setScaleType(ImageButton.ScaleType.CENTER_INSIDE);

                    btMeat.setId(listFoods.get(i).getId());

                    btMeat.setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(View view) {

                            setUpNavigationView(listFoods.get(auxNum));

                            drawer.openDrawer(Gravity.LEFT);

                            loadNavigationHeader(listFoods.get(auxNum), btMeat.getDrawable());

                        }
                    });

                    tr.addView(btMeat);

                }

                i++;

            }

            tableLayout.addView(tr, new TableLayout.LayoutParams(TableLayout.LayoutParams.MATCH_PARENT,
                    TableLayout.LayoutParams.WRAP_CONTENT));

        }
    }

    private void setUpNavigationView(final FoodDTO product) {

        Log.e("Product", product.toString());

        navigationView.setNavigationItemSelectedListener(new NavigationView.OnNavigationItemSelectedListener() {
            @Override
            public boolean onNavigationItemSelected(MenuItem menuItem) {

                switch (menuItem.getItemId()) {
                    case R.id.nav_add:
                        product.setQuantity(product.getQuantity() + 1);

                        tvQty.setText("Cantidad: " + product.getQuantity());

                        break;
                    case R.id.nav_remove:

                        if (product.getQuantity() < 0)
                            product.setQuantity(0);

                        tvQty.setText("Cantidad: " + product.getQuantity());

                        break;

                    case R.id.nav_accept:

                        Order.addFood(product);

                        break;

                    case R.id.nav_decline:

                        product.setQuantity(0);

                        break;

                    case R.id.nav_sendorder:

//                        String url = _URL + "api/Orders/Manager";
//                        new sendOrder().execute(url);
//
//                        break;

                        Thread getOrder = new Thread(new Runnable() {
                            @Override
                            public void run() {
                                WebService.PostOrder(Order);
                            }
                        });
                        getOrder.start();
                        try {
                            getOrder.join();
                        } catch (InterruptedException e) {
                            e.printStackTrace();
                        }
                        break;
                }

                if (menuItem.isChecked()) {
                    menuItem.setChecked(false);
                } else {
                    menuItem.setChecked(true);
                }
                menuItem.setChecked(true);

                for (int i = 0; i < Order.getListFoods().size(); i++) {
                    Log.e("Foods --> ", Order.getListFoods().get(i).toString());
                }

                return true;
            }

        });
    }

    private void loadNavigationHeader(FoodDTO productName, Drawable img) {

        tvProductName.setText(productName.getName());
        tvQty.setText("Cantidad: " + productName.getQuantity());

        imgProduct.setImageDrawable(img);
        imgProduct.setScaleType(ImageView.ScaleType.CENTER_INSIDE);

    }

    private static String post(String url, OrderDTO order) {

        InputStream inputStream = null;
        String result = "";

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

            Log.e("JSON --> ", json);


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
