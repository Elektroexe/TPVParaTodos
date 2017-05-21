package com.joshuaorellana.mobile_tpv.controller.fragment;

import android.graphics.drawable.Drawable;
import android.os.Bundle;
import android.support.design.widget.NavigationView;
import android.support.v4.app.Fragment;
import android.support.v4.widget.DrawerLayout;
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

import com.joshuaorellana.mobile_tpv.controller.common.WebService;
import com.joshuaorellana.mobile_tpv.model.business.DrinkDTO;
import com.joshuaorellana.mobile_tpv.model.persistence.ProductsConversor;
import com.joshuaorellana.mobile_tpv.model.persistence.ProductsSQLiteHelper;
import com.joshuaorellana.mobile_tpv.R;
import com.squareup.picasso.Picasso;

import java.util.List;

import static com.joshuaorellana.mobile_tpv.controller.AddOrderActivity.Order;
import static com.joshuaorellana.mobile_tpv.controller.TablesActivity._URL;


/**
 * Created by Joshua-OC on 04/05/2017.
 */

public class DrinkFragment extends Fragment {

    private View rootView;

    private List<DrinkDTO> listDrinks;
    private TableLayout tableLayout;

    private DrawerLayout drawer;
    private NavigationView navigationView;


    private TextView tvProductName;
    private TextView tvQty;
    private ImageView imgProduct;

    public DrinkFragment() {
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {

        rootView = inflater.inflate(R.layout.fragment_product, container, false);

        initComponents();

        return rootView;

    }

    private void initComponents() {

        tableLayout = (TableLayout) rootView.findViewById(R.id.menuTableLayout_Product);
        drawer = (DrawerLayout) rootView.findViewById(R.id.drawerLayout_Product);
        navigationView = (NavigationView) rootView.findViewById(R.id.nav_view_Product);
        View navHeader = navigationView.getHeaderView(0);
        tvProductName = (TextView) navHeader.findViewById(R.id.tvProductName);
        tvQty = (TextView) navHeader.findViewById(R.id.tvQty);
        imgProduct = (ImageView) navHeader.findViewById(R.id.img_Product);
        ProductsSQLiteHelper helper = new ProductsSQLiteHelper(getActivity().getApplicationContext(), "product", null, 1);
        ProductsConversor conversor = new ProductsConversor(helper);
        listDrinks = conversor.getProducts(DrinkDTO.class);
        conversor.closeConnection();
        if (!listDrinks.isEmpty()) {
            createDrinkButtons();
            for (DrinkDTO aux : Order.getListDrinks()) {
                for (int i = 0; i < listDrinks.size(); i++) {
                    DrinkDTO auxB = listDrinks.get(i);
                    if (aux.getName().equals(auxB.getName()))
                        auxB.setQuantity(aux.getQuantity());
                }
            }
        }
    }

    private void createDrinkButtons() {
        int i = 0;
        while (i < listDrinks.size()) {
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
                        if (product.getQuantity() < 0)
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

}
