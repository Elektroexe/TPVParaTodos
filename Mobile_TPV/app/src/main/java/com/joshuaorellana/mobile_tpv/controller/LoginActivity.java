package com.joshuaorellana.mobile_tpv.controller;

import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import com.joshuaorellana.mobile_tpv.R;
import com.joshuaorellana.mobile_tpv.controller.common.WebService;

public class LoginActivity extends AppCompatActivity {

    private Button btAccess;
    private EditText edtUsername;
    private EditText edtPassword;
    private TextView tvIncorrect;

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
        tvIncorrect = (TextView) findViewById(R.id.tv_Incorrect);
        edtUsername.setText("ManagerTest");
        edtPassword.setText("Manager");
        btAccess.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                tvIncorrect.setText("");
                final String user = edtUsername.getText().toString();
                final String pwd = edtPassword.getText().toString();
                Thread connecting = new Thread(new Runnable() {
                    @Override
                    public void run() {
                        token = WebService.Login(user, pwd);
                    }
                });
                connecting.start();
                try {
                    connecting.join();
                } catch (Exception err) {
                    err.printStackTrace();
                }
                if (token != null) {
                    WebService.token = "bearer " + token;
                    Intent auxIntent = new Intent(LoginActivity.this, TablesActivity.class);
                    startActivityForResult(auxIntent, 0);
                } else {
                    tvIncorrect.setText("Usuario o contraseña incorrectos!");
                }
            }
        });
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        edtPassword.setText("");
    }
}
