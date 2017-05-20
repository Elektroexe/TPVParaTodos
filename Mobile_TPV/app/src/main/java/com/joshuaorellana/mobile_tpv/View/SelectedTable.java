package com.joshuaorellana.mobile_tpv.View;

import android.content.Intent;
import android.graphics.Bitmap;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import com.joshuaorellana.mobile_tpv.Controller.WebService;
import com.joshuaorellana.mobile_tpv.Model.OrderDTO;
import com.joshuaorellana.mobile_tpv.Model.TableDTO;
import com.joshuaorellana.mobile_tpv.R;

import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.RequestBody;

import static com.joshuaorellana.mobile_tpv.View.AddOrder.Order;
import static com.joshuaorellana.mobile_tpv.View.Tables._URL;

public class SelectedTable extends AppCompatActivity {

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

        Log.e("auxTable --> ", auxTable.toString());

        Bitmap bmp = (Bitmap) extras.getParcelable("imgButton");

        imgTable.setImageBitmap(bmp);

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

        setListeners();

    }

    private void setListeners() {

        btAddOrder.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent auxIntent = new Intent(SelectedTable.this, AddOrder.class);

                auxIntent.putExtra("modify", false);

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

        btModifyOrder.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent auxIntent = new Intent(SelectedTable.this, AddOrder.class);


                auxIntent.putExtra("modify", true);

                startActivity(auxIntent);

            }
        });

        btCloseOrder.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {

//                String url = _URL + "api/Orders/Close/" +auxTable.getId();
//
//                new sendClose().execute(url);

                Thread closeOrder = new Thread(new Runnable() {
                    @Override
                    public void run() {
                        WebService.CloseOrder(auxTable.getId());
                    }
                });

                closeOrder.start();

                try {
                    closeOrder.join();
                    finishActivity(RESULT_OK);
                } catch (InterruptedException e) {
                    e.printStackTrace();
                }

            }
        });

        btExit.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                onBackPressed();
            }
        });

    }

//    private class sendClose extends AsyncTask<String, Void, String> {
//        @Override
//        protected String doInBackground(String... urls) {
//            return post(urls[0], Order);
//        }
//
//        @Override
//        protected void onPostExecute(String result) {
//            Toast.makeText(SelectedTable.this, "OK!", Toast.LENGTH_SHORT).show();
//        }
//    }
//
//    private static String post(String url, OrderDTO order) {
//
//        try {
//
//            OkHttpClient client = new OkHttpClient();
//
//            RequestBody reqbody = RequestBody.create(null, new byte[0]);
//            Request.Builder formBody = new Request.Builder().url(url).method("POST",reqbody).header("Content-Length", "0");
//            client.newCall(formBody.build()).execute();
//
//
//        } catch (Exception err) {
//
//            Log.e("Error", err.toString());
//
//        }
//
//        return null;
//
//    }
}
