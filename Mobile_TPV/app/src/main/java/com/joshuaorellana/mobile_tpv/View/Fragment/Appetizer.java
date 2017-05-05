package com.joshuaorellana.mobile_tpv.View.Fragment;

import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.joshuaorellana.mobile_tpv.R;

/**
 * Created by Joshua-OC on 04/05/2017.
 */

public class Appetizer extends Fragment {

    public Appetizer() {}

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        return inflater.inflate(R.layout.appetizer, container, false);
    }

}
