package tpv_paratodos.mobile_tpv;

import android.app.ActivityOptions;
import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.ImageButton;

public class Tables extends AppCompatActivity {

    private ImageButton btnTable = null;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_tables);

        initComponents();

        btnTable.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                Intent auxIntent = new Intent(Tables.this, SelectedTable.class);

                Bundle animation = ActivityOptions.makeCustomAnimation(getApplicationContext(),
                        R.anim.open1, R.anim.open2).toBundle();

                startActivityForResult(auxIntent, RESULT_OK, animation);

            }
        });

    }

    private void initComponents() {

        btnTable = (ImageButton) findViewById(R.id.iBtn_Table);

    }
}
