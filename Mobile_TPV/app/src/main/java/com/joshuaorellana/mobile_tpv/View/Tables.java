package com.joshuaorellana.mobile_tpv.View;

import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.Color;
import android.graphics.drawable.BitmapDrawable;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.View;
import android.widget.ImageButton;
import android.widget.TableLayout;
import android.widget.TableRow;

import com.github.kevinsawicki.http.HttpRequest;
import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import com.joshuaorellana.mobile_tpv.Model.TableDTO;
import com.joshuaorellana.mobile_tpv.R;

import java.lang.reflect.Type;
import java.util.ArrayList;

public class Tables extends AppCompatActivity {

    private String _URL;

    private TableLayout tableLayout;
    private ArrayList<TableDTO> listTables;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_tables);

        initComponents();

    }

    private void initComponents() {

        _URL = getString(R.string.URL_localGRANDE);

        tableLayout = (TableLayout) findViewById(R.id.menuTableLayout);
        listTables = new ArrayList<>();


        String url = _URL + "api/Tables";

        Log.e("URL --> ", url);

        new loadContent().execute(url);

    }

    private void createTableButtons() {

        int i = 0;

        while (i < listTables.size()) {

            TableRow tr = new TableRow(this);
            tr.setId(i + 000);

            tr.setLayoutParams(new TableRow.LayoutParams(TableRow.LayoutParams.MATCH_PARENT,
                    TableRow.LayoutParams.WRAP_CONTENT));

            for (int j = 0; j < 2; j++) {

                if (i < listTables.size()) {

                    final ImageButton btTable = new ImageButton(this);
                    final int auxNum = i;
                    btTable.setId(listTables.get(i).getId());

                    btTable.setMaxHeight(50);
                    btTable.setMaxWidth(50);

                    if (listTables.get(i).isEmpty()) {
                        btTable.setBackgroundColor(Color.GREEN);
                    } else {
                        btTable.setBackgroundColor(Color.RED);
                    }

                    btTable.setImageResource(R.mipmap.ic_launcher_round);

                    btTable.setOnClickListener(new View.OnClickListener() {
                        @Override
                        public void onClick(View view) {
                            Log.e("Button Table: ", listTables.get(auxNum).toString());
                            Intent auxIntent = new Intent(Tables.this, SelectedTable.class);

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

    private class loadContent extends AsyncTask<String, Long, String> {

        protected String doInBackground(String... urls) {

            try {
                return HttpRequest.get(urls[0]).accept("application/json").body();
            } catch (HttpRequest.HttpRequestException exception) {
                return null;
            }
        }

        protected void onPostExecute(String response) {

            listTables = getTables(response);

            if (!listTables.isEmpty())
                createTableButtons();
        }
    }

    private ArrayList<TableDTO> getTables(String json) {
        Gson gson = new Gson();
        Type tListType = new TypeToken<ArrayList<TableDTO>>() {}.getType();
        return gson.fromJson(json, tListType);
    }
}
