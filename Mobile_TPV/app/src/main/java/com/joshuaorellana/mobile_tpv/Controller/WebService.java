package com.joshuaorellana.mobile_tpv.Controller;

import android.util.Log;

import com.google.gson.Gson;
import com.joshuaorellana.mobile_tpv.Model.Login;
;

import java.io.IOException;

import okhttp3.MediaType;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.RequestBody;
import okhttp3.Response;

/**
 * Created by Eduardo on 15/05/2017.
 */

public class WebService {

    //static final String URL = "http://192.168.1.108/TPVParaTodos/api/";
    //static final String URL = "http://172.16.100.15:1550/api/";
    //static final String URL = "http://172.16.10.18:1550/api/";
    static final String URL = "http://tpvpt.azurewebsites.net/api/";

    public static String token;

    public static String Login(String username, String password){
        try {

            OkHttpClient client = new OkHttpClient();
            MediaType mediaType = MediaType.parse("application/x-www-form-urlencoded");
            RequestBody body = RequestBody.create(mediaType, "grant_type=password&username=" + username + "&password=" + password);
            Log.e("Body -->", body.toString());
            Request request = new Request.Builder()
                    .url("http://tpvpt.azurewebsites.net/token")
                    .post(body)
                    .addHeader("content-type", "application/x-www-form-urlencoded")
                    .build();
            Response response = client.newCall(request).execute();
            if (response.isSuccessful()){
                Gson gson = new Gson();
                Login login = gson.fromJson(response.body().string(), Login.class);
                Log.e("login -->", login.getAccess_token());
                return "bearer " + login.getAccess_token();

            } else {
                return null;
            }
        } catch (Exception ex){
            Log.e("Error!", ex.toString());
            return null;
        }
    }

    public static void Logout() {
        try {
            OkHttpClient client = new OkHttpClient();
            Request request = new Request.Builder()
                    .url("http://tpvpt.azurewebsites.net/api/Account/Logout")
                    .post(null)
                    .addHeader("authorization", token)
                    .addHeader("content-length", "0")
                    .addHeader("content-type", "multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW")
                    .build();

            Response response = client.newCall(request).execute();


        } catch (IOException err) {
            err.printStackTrace();
        }
    }

    public static <T> T[] Get(Class<T[]> classType) {

        Log.e("token -->", token);

        if (token != null && token.length() > 0) {
            String urlAPI = URL + classType.getSimpleName().replace("DTO[]", "s");
            T[] products;
            try {
                OkHttpClient client = new OkHttpClient();
                Request request = new Request.Builder()
                        .url(urlAPI)
                        .get()
                        .addHeader("Authorization", token)
                        .build();
                Response response = client.newCall(request).execute();
                Log.e("Response -->", response.message());
                Gson gson = new Gson();
                products = gson.fromJson(response.body().string(), classType);
                Log.e("products size -->", String.valueOf(products.length));
            } catch (Exception ex) {
                Log.e(" !!ERROR!!", ex.toString());
                return null;
            }
            return products;
        }
        return null;
    }
}
