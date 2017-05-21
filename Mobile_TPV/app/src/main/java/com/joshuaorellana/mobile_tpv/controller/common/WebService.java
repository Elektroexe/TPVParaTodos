package com.joshuaorellana.mobile_tpv.controller.common;

import android.util.Log;

import com.google.gson.Gson;
import com.joshuaorellana.mobile_tpv.R;
import com.joshuaorellana.mobile_tpv.model.Login;
import com.joshuaorellana.mobile_tpv.model.business.OrderDTO;
import com.joshuaorellana.mobile_tpv.model.Update;

import okhttp3.MediaType;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.RequestBody;
import okhttp3.Response;

/**
 * Created by Eduardo on 15/05/2017.
 */

public class WebService {

    static final String URL = "http://tpvpt.azurewebsites.net/";
    public static String token;

    public static String Login(String username, String password) {
        try {
            OkHttpClient client = new OkHttpClient();
            MediaType mediaType = MediaType.parse("application/x-www-form-urlencoded");
            RequestBody body = RequestBody.create(mediaType, "grant_type=password&username=" + username + "&password=" + password);
            Log.e("Body -->", body.toString());
            Request request = new Request.Builder()
                    .url(URL + "token")
                    .post(body)
                    .addHeader("content-type", "application/x-www-form-urlencoded")
                    .build();
            Response response = client.newCall(request).execute();
            if (response.isSuccessful()) {
                Gson gson = new Gson();
                Login login = gson.fromJson(response.body().string(), Login.class);
                return login.getAccess_token();
            }
        } catch (Exception ex) {
            return null;
        }
        return null;
    }

	public static boolean Logout() {
        try {
            OkHttpClient client = new OkHttpClient();
            MediaType mediaType = MediaType.parse("application/json");
            RequestBody body = RequestBody.create(mediaType, "");
            Request request = new Request.Builder()
                    .url("http://tpvpt.azurewebsites.net/api/Account/Logout")
                    .post(body)
                    .addHeader("content-type", "application/json")
                    .addHeader("authorization", token)
                    .build();
            Response response = client.newCall(request).execute();
            if (response.isSuccessful()){
                return true;
            }
        } catch (Exception err) {
            return false;
        }
        return false;
    }

    public static <T> T[] Get(Class<T[]> classType) {
        try {
            OkHttpClient client = new OkHttpClient();
            Request request = new Request.Builder()
                    .url(URL + "api/" + classType.getSimpleName().replace("DTO[]", "s"))
                    .get()
                    .addHeader("authorization", token)
                    .build();
            Response response = client.newCall(request).execute();
            Gson gson = new Gson();
            T[] products = gson.fromJson(response.body().string(), classType);
            return products;
        } catch (Exception ex) {
            return null;
        }
    }

    public static OrderDTO GetOrder(int orderId) {
        try {
            OkHttpClient client = new OkHttpClient();
            Request request = new Request.Builder()
                    .url(URL + "api/Orders/Manager/" + orderId)
                    .get()
                    .addHeader("authorization", token)
                    .build();
            Response response = client.newCall(request).execute();
            Gson gson = new Gson();
            OrderDTO order = gson.fromJson(response.body().string(), OrderDTO.class);
            return order;
        } catch (Exception ex) {
            return null;
        }
    }

    public static Update CheckDB(int versionDB) {
        try {
            OkHttpClient client = new OkHttpClient();
            Request request = new Request.Builder()
                    .url(URL + "api/CheckDB/" + versionDB)
                    .get()
                    .addHeader("authorization", token)
                    .build();
            Response response = client.newCall(request).execute();
            Gson gson = new Gson();
            Update up = gson.fromJson(response.body().string(), Update.class);
            return up;
        } catch (Exception ex) {
            return null;
        }
    }

    public static boolean PostOrder(OrderDTO order){
        try {
            Gson gson = new Gson();
            OkHttpClient client = new OkHttpClient();
            MediaType mediaType = MediaType.parse("application/json");
            RequestBody body = RequestBody.create(mediaType, gson.toJson(order));
            Request request = new Request.Builder()
                    .url(URL + "api/Orders/Manager")
                    .post(body)
                    .addHeader("content-type", "application/json")
                    .addHeader("authorization", token)
                    .build();
            Response response = client.newCall(request).execute();
            if (response.isSuccessful()){
                return true;
            }
        } catch (Exception ex) {
            return false;
        }
        return false;
    }

    public static boolean CloseOrder(int tableId){
        try {
            OkHttpClient client = new OkHttpClient();
            MediaType mediaType = MediaType.parse("application/json");
            RequestBody body = RequestBody.create(mediaType, "");
            Request request = new Request.Builder()
                    .url(URL + "api/Orders/Close/" + tableId)
                    .post(body)
                    .addHeader("content-type", "application/json")
                    .addHeader("authorization", token)
                    .build();
            Response response = client.newCall(request).execute();
            if (response.isSuccessful()){
                return true;
            }
        } catch (Exception ex) {
            return false;
        }
        return false;
    }

    public static String PathImage(int productId){
        return URL + "Image/Product/" + productId;
    }
}
