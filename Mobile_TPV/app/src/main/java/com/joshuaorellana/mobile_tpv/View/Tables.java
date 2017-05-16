package com.joshuaorellana.mobile_tpv.View;

import android.content.Context;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.drawable.BitmapDrawable;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.View;
import android.widget.ImageButton;
import android.widget.TableLayout;
import android.widget.TableRow;

import com.joshuaorellana.mobile_tpv.Controller.WebService;
import com.joshuaorellana.mobile_tpv.Model.TableDTO;
import com.joshuaorellana.mobile_tpv.R;
import com.squareup.picasso.Picasso;

public class Tables extends AppCompatActivity {

    public static String _URL;
    //private ArrayList<TableDTO> listTables;
    private TableLayout tableLayout;
    private TableDTO[] listTables;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.testing);
        initComponents();
    }

    private void initComponents() {
        //_URL = getString(R.string.URL_localGRANDE);
        _URL = getString(R.string.URLlocalhostGRANDE);
        //_URL = getString(R.string.URLlocalhostPEQUENA);
        //_URL = getString(R.string.URL_localPEQUENA);
        //listTables = new ArrayList<>();
        //String url = _URL + "api/Tables";
        //Log.e("URL --> ", url);
        //new loadContent().execute(url);
        tableLayout = (TableLayout) findViewById(R.id.menuTableLayoutTest);
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
            if (listTables.length > 0) createTableButtons();
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

//    private class loadContent extends AsyncTask<String, Long, String> {
//
//        protected String doInBackground(String... urls) {
//
//            try {
//                return HttpRequest.get(urls[0]).accept("application/json").body();
//            } catch (HttpRequest.HttpRequestException exception) {
//                return null;
//            }
//        }
//
//        protected void onPostExecute(String response) {
//
//            listTables = getTables(response);
//
//            if (!listTables.isEmpty())
//                createTableButtons();
//        }
//    }
//
//    private ArrayList<TableDTO> getTables(String json) {
//        Gson gson = new Gson();
//        Type tListType = new TypeToken<ArrayList<TableDTO>>() {}.getType();
//        return gson.fromJson(json, tListType);
//    }
}
