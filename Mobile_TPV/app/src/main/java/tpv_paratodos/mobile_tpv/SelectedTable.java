package tpv_paratodos.mobile_tpv;

import android.app.ActivityOptions;
import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.telecom.Call;
import android.view.View;
import android.widget.Button;
import android.widget.ImageButton;

public class SelectedTable extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_selected_table);

        initComponents();

        btn_Back.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                onBackPressed();
            }
        });

        btn_AddOrder.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent auxIntent = new Intent(SelectedTable.this, AddOrder.class);

                startActivity(auxIntent);

            }
        });

    }

    @Override
    public void onBackPressed() {

        super.onBackPressed();
        overridePendingTransition(R.anim.close2,R.anim.close1);

    }

    private void initComponents() {

        btn_AddOrder = (Button) findViewById(R.id.btn_AddOrder);
        btn_ViewOrder = (Button) findViewById(R.id.btn_ViewOrder);
        btn_ModifyOrder = (Button) findViewById(R.id.btn_ModifyOrder);
        btn_CloseOrder = (Button) findViewById(R.id.btn_CloseOrder);
        btn_Back = (ImageButton) findViewById(R.id.iBtn_Back);



    }

    private Button btn_AddOrder = null;
    private Button btn_ViewOrder = null;
    private Button btn_ModifyOrder = null;
    private Button btn_CloseOrder = null;
    private ImageButton btn_Back = null;
}
