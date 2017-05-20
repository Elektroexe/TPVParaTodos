package com.joshuaorellana.mobile_tpv.View;

import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;

import com.joshuaorellana.mobile_tpv.Controller.WebService;
import com.joshuaorellana.mobile_tpv.R;

public class LoginActivity extends AppCompatActivity {

    private Button btAccess;
    private EditText edtUsername;
    private EditText edtPassword;

    private String token;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);
        initComponents();
    }

    private void initComponents() {

        btAccess = (Button) findViewById(R.id.bt_Access);
        edtUsername = (EditText) findViewById(R.id.et_User);
        edtPassword = (EditText) findViewById(R.id.et_Password);
        edtUsername.setText("ManagerTest");
        edtPassword.setText("Manager");

        btAccess.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {

                final String user = edtUsername.getText().toString();
                final String pwd = edtPassword.getText().toString();

                Thread connecting = new Thread(new Runnable() {
                    @Override
                    public void run() {
                        try {
                            token = WebService.Login(user, pwd);
                        } catch (Exception ex){
                            ex.printStackTrace();
                        }
                    }
                });

                connecting.start();

                try {
                    connecting.join();
                } catch (InterruptedException err) {
                    err.printStackTrace();
                }


                if (token != null) {
                    Log.e(" LOGIN --> ","Access granted!!");
                    WebService.token = "bearer  " + token;
                    Intent auxIntent = new Intent(LoginActivity.this, Tables.class);
                    startActivity(auxIntent);
                } else {
                    Log.e(" LOGIN --> ", "Access denied!!");
                }
            }
        });
    }
}
