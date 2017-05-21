package com.joshuaorellana.mobile_tpv.controller.fragment;

import android.graphics.drawable.Drawable;
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

import com.joshuaorellana.mobile_tpv.controller.common.WebService;
import com.joshuaorellana.mobile_tpv.model.business.FoodDTO;
import com.joshuaorellana.mobile_tpv.model.persistence.ProductsConversor;
import com.joshuaorellana.mobile_tpv.model.persistence.ProductsSQLiteHelper;
import com.joshuaorellana.mobile_tpv.R;
import com.squareup.picasso.Picasso;

import java.util.ArrayList;
import java.util.List;

import static com.joshuaorellana.mobile_tpv.controller.AddOrderActivity.Order;
import static com.joshuaorellana.mobile_tpv.controller.SelectedTableActivity.auxTable;

/**
 * Created by Joshua-OC on 08/05/2017.
 */

public class FoodFragment extends Fragment {

    private String _Title;

    private View rootView;

    private List<FoodDTO> listFoods;
    private TableLayout tableLayout;

    private DrawerLayout drawer;
    private NavigationView navigationView;

    private TextView tvProductName;
    private TextView tvQty;
    private ImageView imgBgHeader, imgProduct;

    public FoodFragment(String title) {
        this._Title = title;
    }

    public void onCreate(Bundle savedInstanceState, String title) {
        super.onCreate(savedInstanceState);
        this._Title = title;
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
        imgBgHeader = (ImageView) navHeader.findViewById(R.id.img_header_bg);
        imgProduct = (ImageView) navHeader.findViewById(R.id.img_Product);
        listFoods = new ArrayList<>();
        ProductsSQLiteHelper helper = new ProductsSQLiteHelper(getActivity().getApplicationContext(), "product", null, 1);
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
                    Picasso.with(getActivity().getApplicationContext()).load(WebService.PathImage(listFoods.get(i).getId())).resize(250, 250).into(btMeat);
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
                        auxTable.setEmpty(false);
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
}
