package com.joshuaorellana.mobile_tpv.View;

import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.widget.TextView;

import com.github.kevinsawicki.http.HttpRequest;
import com.google.gson.Gson;
import com.joshuaorellana.mobile_tpv.Model.OrderDTO;
import com.joshuaorellana.mobile_tpv.R;

import java.util.ArrayList;
import java.util.List;

import static com.joshuaorellana.mobile_tpv.View.SelectedTable.auxTable;

public class ViewOrder extends AppCompatActivity {

    private TextView tvText;
    private String _URL;
    private List<OrderDTO> listOrder;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_view_order);

        initComponents();

    }

    private void initComponents() {

        //_URL = getString(R.string.URLlocalhostPEQUENA);
        _URL = getString(R.string.URL_localPEQUENA);

        listOrder = new ArrayList<>();

        String url = _URL + "api/Orders/Manager/" + auxTable.getId();

        tvText = (TextView) findViewById(R.id.tv_Text);
        tvText.setText("");

        //Log.e("listOrder", String.valueOf(listOrder.size()));

        new loadContent().execute(url);

    }

    private class loadContent extends AsyncTask<String, Long, String > {

        protected String doInBackground(String... urls) {

            Log.e("URL -->", urls[0]);

            try {
                return HttpRequest.get(urls[0]).accept("application/json").body();
            } catch (HttpRequest.HttpRequestException err) {
                Log.e("ERROR HttpRequest: ", err.toString());
            }

            return null;

        }

        protected void onPostExecute(String response) {

            Log.e("onPost --> ", "DONE!");
            Log.e("Response -->", response);
            //tvText.setText(response);

            //listOrder = getTables(response);
            OrderDTO order = getTables(response);

            tvText.setText(order.toString());





//            if (listOrder.isEmpty())
//                Log.e("listOrder -->", "isEmpty!");
//            else
//                Log.e("listOrder size -->", String.valueOf(listOrder.size()));

        }

    }

    private OrderDTO getTables(String json) {
        Gson gson = new Gson();

        //JsonParser parser = new JsonParser();
        //JsonObject jsonObj = parser.parse(json).getAsJsonObject();

        //Type tListType = new TypeToken<ArrayList<OrderDTO>>() {}.getType();
        return gson.fromJson(json, OrderDTO.class);

    }



}
