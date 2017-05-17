package com.joshuaorellana.mobile_tpv.Controller;

import android.util.Log;

import com.google.gson.Gson;
import com.joshuaorellana.mobile_tpv.Model.Login;

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
    static final String URL = "http://172.16.100.15:1550/api/";
    public static String token;

    public static String Login(String username, String password){
        try {
            OkHttpClient client = new OkHttpClient();
            MediaType mediaType = MediaType.parse("application/x-www-form-urlencoded");
            RequestBody body = RequestBody.create(mediaType, "grant_type=password&username=" + username + "&password=" + password);
            Request request = new Request.Builder()
                    .url("http://172.16.100.15:1550/token")
                    .post(body)
                    .addHeader("content-type", "application/x-www-form-urlencoded")
                    .build();
            Response response = client.newCall(request).execute();
            if (response.isSuccessful()){
                Gson gson = new Gson();
                Login login = gson.fromJson(response.body().string(), Login.class);
                return login.getAccess_token();
            } else {
                return null;
            }
        } catch (Exception ex){
            Log.e("Error!", ex.toString());
            return null;
        }
    }

    public static <T> T[] Get(Class<T[]> classType) {
        //if (token != null && token.length() > 0) {
            String urlAPI = URL + classType.getSimpleName().replace("DTO[]", "s");
            T[] products;
            try {
                OkHttpClient client = new OkHttpClient();
                Request request = new Request.Builder()
                        .url(urlAPI)
                        .get()
                        //.addHeader("authorization", token)
                        .build();
                Response response = client.newCall(request).execute();
                Gson gson = new Gson();
                products = gson.fromJson(response.body().string(), classType);
            } catch (Exception ex) {
                return null;
            }
            return products;
        //}
        //return null;
    }
}
