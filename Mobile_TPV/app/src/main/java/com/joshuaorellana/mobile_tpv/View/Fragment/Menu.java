package com.joshuaorellana.mobile_tpv.View.Fragment;

import android.os.AsyncTask;
import android.os.Bundle;
import android.support.design.widget.NavigationView;
import android.support.v4.app.Fragment;
import android.support.v4.widget.DrawerLayout;
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
import com.joshuaorellana.mobile_tpv.Model.Products.MenuDTO;
import com.joshuaorellana.mobile_tpv.R;
import com.squareup.picasso.Picasso;

import java.lang.reflect.Type;
import java.util.ArrayList;
import java.util.List;

import static com.joshuaorellana.mobile_tpv.View.Tables._URL;

/**
 * Created by Joshua-OC on 04/05/2017.
 */

public class Menu extends Fragment {

    private View rootView;

    private List<MenuDTO> listMenus;
    private TableLayout tableLayout;

    private DrawerLayout drawer;
    private NavigationView navigationView;

    private TextView tvProductName;
    private TextView tvQty;
    private ImageView imgBgHeader, imgProduct;

    public Menu() {}

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {

        rootView = inflater.inflate(R.layout.menu, container, false);

        initComponents();

        return rootView;

    }

    private void initComponents() {

        tableLayout = (TableLayout) rootView.findViewById(R.id.menuTableLayout_Menu);
        drawer = (DrawerLayout) rootView.findViewById(R.id.drawer_layout_Menu);
        navigationView = (NavigationView) rootView.findViewById(R.id.nav_view_Menu);
        View navHeader = navigationView.getHeaderView(0);

        tvProductName = (TextView) navHeader.findViewById(R.id.tvProductName);
        tvQty = (TextView) navHeader.findViewById(R.id.tvQty);

        imgBgHeader = (ImageView) navHeader.findViewById(R.id.img_header_bg);
        imgProduct = (ImageView) navHeader.findViewById(R.id.img_Product);


        listMenus = new ArrayList<>();

        String url = _URL + "api/Menus";

        new loadMenus().execute(url);

    }

    private class loadMenus extends AsyncTask<String, Long, String> {

        protected String doInBackground(String... urls) {

            try {
                return HttpRequest.get(urls[0]).accept("application/json").body();
            } catch (HttpRequest.HttpRequestException execption) {
                return null;
            }
        }

        protected void onPostExecute(String response) {

            listMenus = getMenus(response);

            if(!listMenus.isEmpty()) {
                createMenusButtons();
            }
        }
    }

    private ArrayList<MenuDTO> getMenus(String json) {
        Gson gson = new Gson();
        Type tListType = new TypeToken<ArrayList<MenuDTO>>() {}.getType();
        return gson.fromJson(json, tListType);
    }

    private void createMenusButtons() {
        int i = 0;

        while (i < listMenus.size()) {

            TableRow tr = new TableRow(getActivity().getApplicationContext());
            tr.setId(i + 25);

            tr.setLayoutParams(new TableRow.LayoutParams(TableRow.LayoutParams.MATCH_PARENT,
                    TableRow.LayoutParams.WRAP_CONTENT));

            for (int j = 0; j < 2; j++) {

                if (i < listMenus.size()) {

                    final ImageButton btMeat = new ImageButton(getActivity().getApplicationContext());
                    final int auxNum = i;

                    String url = _URL + "Image/Product/" + listMenus.get(i).getId();

                    Picasso.with(getActivity().getApplicationContext()).load(url).resize(250, 250).into(btMeat);
                    btMeat.setScaleType(ImageButton.ScaleType.CENTER_INSIDE);

                    btMeat.setId(listMenus.get(i).getId());

                    btMeat.setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(View view) {

//                            setUpNavigationView(listMenus.get(auxNum));
//
//                            drawer.openDrawer(Gravity.LEFT);
//
//                            loadNavigationHeader(listMenus.get(auxNum), btMeat.getDrawable());

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

//    private void setUpNavigationView(final MenuDTO product) {
//        navigationView.setNavigationItemSelectedListener(new NavigationView.OnNavigationItemSelectedListener() {
//            @Override
//            public boolean onNavigationItemSelected(MenuItem menuItem) {
//
//                switch (menuItem.getItemId()) {
//                    case R.id.nav_add:
//
//                        product.setQty(product.getQty() + 1);
//
//                        tvQty.setText("Cantidad: " + product.getQty());
//
//                        break;
//                    case R.id.nav_remove:
//
//                        product.setQty(product.getQty() - 1);
//
//                        if (product.getQty() < 0 )
//                            product.setQty(0);
//
//                        tvQty.setText("Cantidad: " + product.getQty());
//
//                        break;
//
//                }
//
//                if (menuItem.isChecked()) {
//                    menuItem.setChecked(false);
//                } else {
//                    menuItem.setChecked(true);
//                }
//                menuItem.setChecked(true);
//
//                return true;
//            }
//
//        });
//    }
//
//    private void loadNavigationHeader(MenuDTO productName, Drawable img) {
//
//        tvProductName.setText(productName.getName());
//        tvQty.setText("Cantidad: " + productName.getQty());
//
//        imgProduct.setImageDrawable(img);
//        imgProduct.setScaleType(ImageView.ScaleType.CENTER_INSIDE);
//
//    }


}
