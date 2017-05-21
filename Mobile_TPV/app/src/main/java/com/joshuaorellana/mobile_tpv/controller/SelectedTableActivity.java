package com.joshuaorellana.mobile_tpv.controller;

import android.app.Activity;
import android.content.Intent;
import android.graphics.Bitmap;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;

import com.joshuaorellana.mobile_tpv.controller.common.WebService;
import com.joshuaorellana.mobile_tpv.model.business.TableDTO;
import com.joshuaorellana.mobile_tpv.R;

public class SelectedTableActivity extends AppCompatActivity {

    private ImageView imgTable;

    private TextView txtNumTable;

    private Button btAddOrder;
    private Button btViewOrder;
    private Button btModifyOrder;
    private Button btCloseOrder;
    private Button btExit;

    public static TableDTO auxTable;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_selected_table);
        initComponents();
    }

    private void initComponents() {
        txtNumTable = (TextView) findViewById(R.id.tvNumTable);
        imgTable = (ImageView) findViewById(R.id.imgTable);
        btAddOrder = (Button) findViewById(R.id.btnAddOrder);
        btViewOrder = (Button) findViewById(R.id.btnViewOrder);
        btModifyOrder = (Button) findViewById(R.id.btnModifyOrder);
        btCloseOrder = (Button) findViewById(R.id.btnCloseOrder);
        btExit = (Button) findViewById(R.id.btExit);
        Bundle extras = getIntent().getExtras();
        auxTable = (TableDTO) extras.getSerializable("Table");
        txtNumTable.setText(String.valueOf(auxTable.getId()));
        Bitmap bmp = extras.getParcelable("imgButton");
        imgTable.setImageBitmap(bmp);
        checkEmpty();
        setListeners();
    }

    private void setListeners() {
        btAddOrder.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent auxIntent = new Intent(SelectedTableActivity.this, AddOrderActivity.class);
                auxIntent.putExtra("modify", false);

                startActivityForResult(auxIntent, 0);
            }
        });
        btViewOrder.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent auxIntent = new Intent(SelectedTableActivity.this, ViewOrderActivity.class);
                startActivity(auxIntent);
            }
        });
        btModifyOrder.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent auxIntent = new Intent(SelectedTableActivity.this, AddOrderActivity.class);
                auxIntent.putExtra("modify", true);
                startActivityForResult(auxIntent, 0);
            }
        });
        btCloseOrder.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Thread closeOrder = new Thread(new Runnable() {
                    @Override
                    public void run() {
                        WebService.CloseOrder(auxTable.getId());
                    }
                });
                closeOrder.start();
                try {
                    closeOrder.join();
                } catch (InterruptedException e) {
                    e.printStackTrace();
                }
                auxTable.setEmpty(true);
                checkEmpty();
            }
        });
        btExit.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                ((Activity) view.getContext()).finish();
            }
        });
    }

    private void checkEmpty() {
        if (auxTable.isEmpty()) {
            btAddOrder.setEnabled(true);
            btModifyOrder.setEnabled(false);
            btCloseOrder.setEnabled(false);
            btViewOrder.setEnabled(false);

        } else {
            btAddOrder.setEnabled(false);
            btModifyOrder.setEnabled(true);
            btCloseOrder.setEnabled(true);
            btViewOrder.setEnabled(true);
        }
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        checkEmpty();
    }
}
