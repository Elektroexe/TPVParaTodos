package com.joshuaorellana.mobile_tpv.Controller;

import android.app.Notification;
import android.app.NotificationManager;
import android.content.Context;
import android.os.Handler;
import android.support.v4.app.NotificationCompat;
import android.util.Log;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import com.joshuaorellana.mobile_tpv.Model.TableDTO;
import com.joshuaorellana.mobile_tpv.R;

import java.lang.reflect.Type;
import java.util.concurrent.ExecutionException;

import microsoft.aspnet.signalr.client.Platform;
import microsoft.aspnet.signalr.client.SignalRFuture;
import microsoft.aspnet.signalr.client.http.android.AndroidPlatformComponent;
import microsoft.aspnet.signalr.client.hubs.HubConnection;
import microsoft.aspnet.signalr.client.hubs.HubProxy;
import microsoft.aspnet.signalr.client.hubs.SubscriptionHandler1;
import microsoft.aspnet.signalr.client.hubs.SubscriptionHandler3;

import static com.joshuaorellana.mobile_tpv.View.Tables.listButtons;

/**
 * Created by Joshua-OC on 16/05/2017.
 */

public class SignalR {

    final String URL = "http://192.168.1.108/TPVParaTodos/signalr/";
    //final String URL = "http://tpvpt.azurewebsites.net/signalr/";

    private TableDTO[] tables;
    private Context context;
    private Handler mHandler;

    public SignalR(TableDTO[] tables, Context context) {

        this.tables = tables;
        this.context = context;
        mHandler = new Handler();

    }

    public void getNotifications() {

        Platform.loadPlatformComponent(new AndroidPlatformComponent());

        HubConnection notifyConnection = new HubConnection(URL);

        HubProxy hubNotification = notifyConnection.createHubProxy("notificationHub");
        hubNotification.subscribe(this);

        hubNotification.on("notify", new SubscriptionHandler3<String, String, Integer>() {
            @Override
            public void run(final String title, final String message, final Integer id) {

                mHandler.post(new Runnable() {
                    @Override
                    public void run() {
                        createNotification(title, message, id);
                    }
                });

            }
        }, String.class, String.class, Integer.class);

        startConnection(notifyConnection);

    }

    private void createNotification (String title, String msg, Integer id) {

        NotificationCompat.Builder mBuilder = new NotificationCompat.Builder(this.context)
                .setSmallIcon(R.mipmap.ic_launcher_round)
                .setContentTitle(title)
                .setContentText(msg)
                .setVibrate(new long[] {1000, 1000, 1000, 1000, 1000})
                .setDefaults(Notification.DEFAULT_ALL)
                .setPriority(Notification.PRIORITY_HIGH);

        Log.e("Notification -->", "Enter");

        NotificationManager mNotificationManager = (NotificationManager) this.context.getSystemService(Context.NOTIFICATION_SERVICE);
        mNotificationManager.notify(id, mBuilder.build());

    }

    public void getRefreshTable() {

        Platform.loadPlatformComponent(new AndroidPlatformComponent());

        HubConnection refreshConnection = new HubConnection(URL);

        HubProxy hubRefresh = refreshConnection.createHubProxy("tableHub");
        hubRefresh.subscribe(this);

        hubRefresh.on("refresh", new SubscriptionHandler1<String>() {

            @Override
            public void run(final String newTables) {

                mHandler.post(new Runnable() {
                    @Override
                    public void run() {

                        TableDTO[] test = getNewTables(newTables);

                        Log.e("Test size! -->", String.valueOf(test.length));

                        for (TableDTO auxTable : test) {

                            for(int i = 0; i < listButtons.size(); i++) {

                                if (listButtons.get(i).getId() == auxTable.getId()) {
                                    if (auxTable.isEmpty()) {
                                        listButtons.get(i).setBackgroundResource(R.drawable.buttonshape);
                                    } else {
                                        listButtons.get(i).setBackgroundResource(R.drawable.buttonshapered);
                                    }
                                }
                            }

                        }

                        Log.e("newTables -->", newTables);

                    }
                });
            }
        }, String.class);

        startConnection(refreshConnection);

    }

    private void startConnection(HubConnection connection) {

        SignalRFuture<Void> awaitConnection = connection.start();

        try {
            awaitConnection.get();
        } catch (InterruptedException e) {
            Log.e("InterruptedException", e.toString());
        } catch (ExecutionException e) {
            Log.e("ExecutionException", e.toString());
        }

    }

    private TableDTO[] getNewTables(String json) {
        Gson gson = new Gson();
        Type tListType = new TypeToken<TableDTO[]>() {}.getType();
        return gson.fromJson(json, tListType);
    }

}
