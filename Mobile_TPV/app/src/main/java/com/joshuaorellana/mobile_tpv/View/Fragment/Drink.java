package com.joshuaorellana.mobile_tpv.View.Fragment;

import android.app.ProgressDialog;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.Handler;
import android.support.design.widget.NavigationView;
import android.support.v4.app.Fragment;
import android.support.v4.widget.DrawerLayout;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.TableLayout;
import android.widget.TableRow;
import android.widget.TextView;

import com.github.kevinsawicki.http.HttpRequest;
import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import com.joshuaorellana.mobile_tpv.Model.Products.DrinkDTO;
import com.joshuaorellana.mobile_tpv.R;
import com.squareup.picasso.Picasso;

import java.lang.reflect.Type;
import java.util.ArrayList;
import java.util.List;


/**
 * Created by Joshua-OC on 04/05/2017.
 */

public class Drink extends Fragment {

    private String _URL;
    private View rootView;

    private List<DrinkDTO> listDrinks;
    private TableLayout tableLayout;
    private int qty; // <-- CANTIDAD DE TESTING

    private DrawerLayout drawer;
    private NavigationView navigationView;
    private TextView tvProductName;
    private TextView tvQty;
    private ImageView imgBgHeader, imgProduct;

    private ProgressDialog dialog;
    private Handler handler;

    public Drink() {}

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {

        rootView = inflater.inflate(R.layout.drink_test, container, false);

//        tableLayout = (TableLayout) rootView.findViewById(R.id.menuTableLayout2);
//
//        for (int i = 0; i < 6; i++) {
//            TableRow tr = new TableRow(getActivity().getApplicationContext());
//
//            tr.setLayoutParams(new TableRow.LayoutParams(TableRow.LayoutParams.MATCH_PARENT,
//                    TableRow.LayoutParams.WRAP_CONTENT));
//
//            for (int j = 0; j < 7; j++) {
//
//                final ImageButton btDrink = new ImageButton(getActivity().getApplicationContext());
//                btDrink.setId(j);
//                btDrink.setImageResource(R.mipmap.ic_launcher);
//
//                tr.addView(btDrink);
//
//            }
//
//            tableLayout.addView(tr);
//
//        }

        initComponents();

        return rootView;

    }

    private void initComponents() {

        _URL = getString(R.string.URL_localGRANDE);



        tableLayout = (TableLayout) rootView.findViewById(R.id.menuTableLayout2);

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


}
