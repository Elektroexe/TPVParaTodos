package com.joshuaorellana.mobile_tpv.View;

import android.content.Context;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.drawable.BitmapDrawable;
import android.os.Bundle;
import android.os.Handler;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.View;
import android.widget.ImageButton;
import android.widget.TableLayout;
import android.widget.TableRow;

import com.joshuaorellana.mobile_tpv.Controller.SignalR;
import com.joshuaorellana.mobile_tpv.Controller.WebService;
import com.joshuaorellana.mobile_tpv.Model.TableDTO;
import com.joshuaorellana.mobile_tpv.R;
import com.squareup.picasso.Picasso;

import java.util.ArrayList;
import java.util.List;

import microsoft.aspnet.signalr.client.hubs.HubConnection;

public class Tables extends AppCompatActivity {

    public static String _URL;
    private TableLayout tableLayout;
    private TableDTO[] listTables;

    public static List<ImageButton> listButtons;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.testing);

        initComponents();
    }

    private void initComponents() {
        _URL = "http://192.168.1.108/TPVParaTodos/";
        //_URL = getString(R.string.URL);
        tableLayout = (TableLayout) findViewById(R.id.menuTableLayoutTest);
        listButtons = new ArrayList<>();

        Thread apiTables = new Thread(new Runnable() {
            @Override
            public void run() {
                try {
                    listTables = WebService.Get(TableDTO[].class);
                } catch (Exception ex){
                    ex.printStackTrace();
                }
            }
        });

        apiTables.start();

        try {
            apiTables.join();
            if (listTables != null && listTables.length > 0) {

                createTableButtons();

                SignalR sgnlR = new SignalR(listTables, getApplicationContext());

                sgnlR.getNotifications();
                sgnlR.getRefreshTable();
            }
        } catch (InterruptedException e) {
            e.printStackTrace();
        }

    }

    private void createTableButtons() {

        int i = 0;

        while (i < listTables.length) {

            TableRow tr = new TableRow(this);

            tr.setId(i + 000);
            tr.setPadding(10, 10, 10, 10);
            TableRow.LayoutParams params = new TableRow.LayoutParams(TableRow.LayoutParams.MATCH_PARENT,
                    TableRow.LayoutParams.WRAP_CONTENT);
            tr.setLayoutParams(params);

            for (int j = 0; j < 3; j++) {

                if (i < listTables.length) {

                    final ImageButton btTable = new ImageButton(this);
                    final int auxNum = i;

                    btTable.setId(listTables[i].getId());

                    android.widget.TableRow.LayoutParams p = new android.widget.TableRow.LayoutParams();
                    p.rightMargin = Tables.dpToPixel(10, getApplicationContext()); // right-margin = 10dp
                    btTable.setLayoutParams(p);

                    Picasso.with(this).load(R.drawable.table_icon).resize(250, 250).into(btTable);

                    if (listTables[i].isEmpty()) {
                        btTable.setBackgroundResource(R.drawable.buttonshape);
                    } else {
                        btTable.setBackgroundResource(R.drawable.buttonshapered);
                    }

                    listButtons.add(btTable);

                    Log.e("ButtonId --> ", String.valueOf(btTable.getId()));

                    btTable.setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(View view) {
                            Log.e("Button Table: ", listTables[auxNum].toString());
                            Intent auxIntent = new Intent(Tables.this, SelectedTable.class);
                            auxIntent.putExtra("Table", listTables[auxNum]);
                            Bitmap img = ((BitmapDrawable)btTable.getDrawable()).getBitmap();
                            Bundle extras = new Bundle();
                            extras.putParcelable("imgButton", img);
                            auxIntent.putExtras(extras);
                            startActivity(auxIntent);
                        }
                    });

                    tr.addView(btTable);
                }
                i++;
            }
            tableLayout.addView(tr, new TableLayout.LayoutParams(TableLayout.LayoutParams.MATCH_PARENT,
                    TableLayout.LayoutParams.WRAP_CONTENT));
        }
    }

    private static Float scale;
    public static int dpToPixel(int dp, Context context) {
        if (scale == null)
            scale = context.getResources().getDisplayMetrics().density;
        return (int) ((float) dp * scale);
    }

}
