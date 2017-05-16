package com.joshuaorellana.mobile_tpv.Controller;

import android.app.Service;
import android.content.Intent;
import android.os.Binder;
import android.os.Handler;
import android.os.IBinder;
import android.os.Looper;
import android.util.Log;
import android.widget.Toast;

import com.joshuaorellana.mobile_tpv.Model.Notification;
import com.joshuaorellana.mobile_tpv.Model.TableDTO;

import java.util.concurrent.ExecutionException;

import microsoft.aspnet.signalr.client.Platform;
import microsoft.aspnet.signalr.client.SignalRFuture;
import microsoft.aspnet.signalr.client.http.android.AndroidPlatformComponent;
import microsoft.aspnet.signalr.client.hubs.HubConnection;
import microsoft.aspnet.signalr.client.hubs.HubProxy;
import microsoft.aspnet.signalr.client.hubs.SubscriptionHandler1;
import microsoft.aspnet.signalr.client.transport.ClientTransport;
import microsoft.aspnet.signalr.client.transport.ServerSentEventsTransport;

/**
 * Created by Joshua-OC on 16/05/2017.
 */

public class SignalR extends Service {

    static final String URL = "http://172.16.100.15:1550/signalr/";

    private HubConnection mHubConnection;
    private HubProxy mHubProxy;
    private Handler mHandler;
    private final IBinder mBinder = new LocalBinder();

    private TableDTO[] tables;

    public SignalR() {}

    public SignalR(TableDTO[] tables) {
        this.tables = tables;
    }

    @Override
    public void onCreate() {
        super.onCreate();
        mHandler = new Handler(Looper.getMainLooper());
    }

    @Override
    public int onStartCommand(Intent intent, int flags, int startId) {
        int result = super.onStartCommand(intent, flags, startId);
        startSignalR();
        return  result;
    }

    @Override
    public void onDestroy() {
        mHubConnection.stop();
        super.onDestroy();
    }

    @Override
    public IBinder onBind(Intent intent) {
        startSignalR();
        return mBinder;
    }

    public class LocalBinder extends Binder {
        public SignalR getService() {
            return SignalR.this;
        }
    }

    private void startSignalR() {

        Platform.loadPlatformComponent(new AndroidPlatformComponent());

        mHubConnection = new HubConnection(URL);
        mHubProxy = mHubConnection.createHubProxy("notificationHub");

        ClientTransport clientTransport = new ServerSentEventsTransport(mHubConnection.getLogger());
        SignalRFuture<Void> signalRFuture = mHubConnection.start(clientTransport);

        try {
            signalRFuture.get();
        } catch (InterruptedException | ExecutionException e) {
            e.printStackTrace();
            return;
        }

        mHubProxy.on("notify", new SubscriptionHandler1<Notification>() {
            @Override
            public void run(final Notification notify) {
                Log.e("RUN PROXY, ", "OK");
                mHandler.post(new Runnable() {

                    @Override
                    public void run() {
                        Log.e("Notify Message -->", notify.getMessage());
                        Toast.makeText(getApplicationContext(), notify.getMessage(), Toast.LENGTH_SHORT).show();
                    }
                });

            }
        }, Notification.class);


    }

//
//
//    private TableDTO[] tables;
//
//    private HubConnection connection;
//    private HubProxy hub;
//
//    public SignalR(TableDTO[] tables) {
//        this.tables = tables;
//        this.connection = new HubConnection(URL);
//    }
//
//    public void notifications() {
//
//        hub = connection.createHubProxy("notificationHub");
//        hub.subscribe(this);
//
//        hub.on("notify", new SubscriptionHandler() {
//            @Override
//            public void run() {
//                Toast.makeText(, "HOLA", Toast.LENGTH_SHORT).show();
//            }
//        });
//
//    }

}
