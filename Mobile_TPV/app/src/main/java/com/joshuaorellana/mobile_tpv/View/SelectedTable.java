package com.joshuaorellana.mobile_tpv.View;

import android.content.Intent;
import android.graphics.Bitmap;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.ImageView;

import com.joshuaorellana.mobile_tpv.Model.TableDTO;
import com.joshuaorellana.mobile_tpv.R;

public class SelectedTable extends AppCompatActivity {

    private ImageView imgTable;
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

        imgTable = (ImageView) findViewById(R.id.imgTable);
        btAddOrder = (Button) findViewById(R.id.btnAddOrder);
        btViewOrder = (Button) findViewById(R.id.btnViewOrder);
        btModifyOrder = (Button) findViewById(R.id.btnModifyOrder);
        btExit = (Button) findViewById(R.id.btExit);

        Bundle extras = getIntent().getExtras();

        auxTable = (TableDTO) extras.getSerializable("Table");

        Log.e("auxTable --> ", auxTable.toString());

        Bitmap bmp = (Bitmap) extras.getParcelable("imgButton");

        imgTable.setImageBitmap(bmp);

        setListeners();

    }

    private void setListeners() {

        btAddOrder.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent auxIntent = new Intent(SelectedTable.this, AddOrder.class);
                startActivity(auxIntent);
            }
        });

        btViewOrder.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent auxIntent = new Intent(SelectedTable.this, ViewOrder.class);
                startActivity(auxIntent);
            }
        });

        btExit.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                onBackPressed();
            }
        });

    }
}
