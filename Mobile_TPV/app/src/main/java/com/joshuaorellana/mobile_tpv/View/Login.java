package com.joshuaorellana.mobile_tpv.View;

import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.widget.Button;

import com.joshuaorellana.mobile_tpv.R;

import java.io.IOException;

import okhttp3.MediaType;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.RequestBody;
import okhttp3.Response;
import okhttp3.ResponseBody;

public class Login extends AppCompatActivity {

    private Button button;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        button = (Button) findViewById(R.id.button);

        String url = getString(R.string.URLlocalhostGRANDE) + "token";

        new tryLogin().execute(url);
    }


    private class tryLogin extends AsyncTask<String, Void, String> {

        protected String doInBackground(String... urls) {

            ResponseBody response = null;
            String aux = "";
            String result = "";

            try {
                OkHttpClient client = new OkHttpClient();

                MediaType mediaType = MediaType.parse("application/x-www-form-urlencoded");
                RequestBody body = RequestBody.create(mediaType, "grant_type=password&username=MansagerTest&password=Manager");
                Request request = new Request.Builder()
                        .url(urls[0])
                        .post(body)
                        .addHeader("content-type", "application/x-www-form-urlencoded")
                        .build();

                response = client.newCall(request).execute().body();

                Log.e("Response --> ", response.string());

                //result = response.string();

            } catch (IOException err) {
                Log.e("Error", err.toString());
                err.printStackTrace();
            }

            return result;
        }

        protected void onPostExecute(Response result) {

            Log.e("Result", result.toString());

//            int count = 3;
//            StringBuilder sb = new StringBuilder();
//
//            for (char c : result.toCharArray()) {
//                if (c == '"') {
//                    count--;
//                }
//
//                if (count == 0)  {
//                    sb.append(c);
//                }
//
//            }
//
//            Log.e("SB --> ", sb.toString());


        }

    }


}
