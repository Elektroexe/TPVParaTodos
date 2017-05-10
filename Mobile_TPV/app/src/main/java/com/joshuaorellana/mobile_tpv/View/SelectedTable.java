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
import android.widget.Toast;

import com.joshuaorellana.mobile_tpv.Model.OrderDTO;
import com.joshuaorellana.mobile_tpv.Model.TableDTO;
import com.joshuaorellana.mobile_tpv.R;

import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.RequestBody;

import static com.joshuaorellana.mobile_tpv.View.AddOrder.Order;

public class SelectedTable extends AppCompatActivity {

    private ImageView imgTable;
    private Button btAddOrder;
    private Button btViewOrder;
    private Button btModifyOrder;
    private Button btCloseOrder;
    private Button btExit;

    public static TableDTO auxTable;
    private String _URL;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_selected_table);

        initComponents();

    }

    private void initComponents() {

        _URL = getString(R.string.URL_localPEQUENA);

        imgTable = (ImageView) findViewById(R.id.imgTable);
        btAddOrder = (Button) findViewById(R.id.btnAddOrder);
        btViewOrder = (Button) findViewById(R.id.btnViewOrder);
        btModifyOrder = (Button) findViewById(R.id.btnModifyOrder);
        btCloseOrder = (Button) findViewById(R.id.btnCloseOrder);
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

        btCloseOrder.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {

                String url = _URL + "api/Orders/Close/" +auxTable.getId();

                new sendClose().execute(url);


            }
        });

        btExit.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {




            }
        });

        btExit.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                onBackPressed();
            }
        });

    }

    private class sendClose extends AsyncTask<String, Void, String> {
        @Override
        protected String doInBackground(String... urls) {
            return post(urls[0], Order);
        }
        // onPostExecute displays the results of the AsyncTask.
        @Override
        protected void onPostExecute(String result) {
            //Toast.makeText(getBaseContext(), "Data Sent!", Toast.LENGTH_LONG).show();
            Toast.makeText(SelectedTable.this, "OK!", Toast.LENGTH_SHORT).show();
        }
    }

    private static String post(String url, OrderDTO order) {

        try {

            Log.e("url -->", url );

//            String json;
//
//            Gson gson = new Gson();
//            json = gson.toJson(order);

            OkHttpClient client = new OkHttpClient();

            RequestBody reqbody = RequestBody.create(null, new byte[0]);
            Request.Builder formBody = new Request.Builder().url(url).method("POST",reqbody).header("Content-Length", "0");
            client.newCall(formBody.build()).execute();

//            Request request = new Request.Builder()
//                    .url(url)
//                    .post()
//                    .build();
//
//            client.newCall(request).execute();

            //Log.e("JSON --> ", json);


        } catch (Exception err) {

            Log.e("Error", err.toString());


        }

        return null;

    }
}
