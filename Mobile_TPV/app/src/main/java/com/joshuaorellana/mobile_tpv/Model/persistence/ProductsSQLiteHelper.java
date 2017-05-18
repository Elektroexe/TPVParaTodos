package com.joshuaorellana.mobile_tpv.Model.persistence;

import android.content.Context;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;

/**
 * Created by Eduardo on 12/05/2017.
 */

public class ProductsSQLiteHelper extends SQLiteOpenHelper {

    private final String SQL_CREATE_DRINKS = "CREATE TABLE DRINKS(" +
            "Id INTEGER PRIMARY KEY, " +
            "Capacity INTEGER, " +
            "TypeBottle TEXT, " +
            "Soda INTEGER, " +
            "Alcohol INTEGER, " +
            "Name TEXT, " +
            "Price REAL, " +
            "Description TEXT )";

    private final String SQL_CREATE_FOODS = "CREATE TABLE FOODS(" +
            "Id INTEGER PRIMARY KEY, " +
            "FamilyDish TEXT, " +
            "Name TEXT, " +
            "Price REAL, " +
            "Description TEXT )";

    private final String SQL_CREATE_MENUS = "CREATE TABLE MENUS(" +
            "Id INTEGER PRIMARY KEY, " +
            "PeopleNumber INTEGER, " +
            "Name TEXT, " +
            "Price REAL, " +
            "Description TEXT )";

    public ProductsSQLiteHelper(Context context, String name, SQLiteDatabase.CursorFactory factory, int version) {
        super(context, name, factory, version);
    }

    @Override
    public void onCreate(SQLiteDatabase sqLiteDatabase) {
        executeSQL(sqLiteDatabase);
    }

    @Override
    public void onUpgrade(SQLiteDatabase sqLiteDatabase, int i, int i1) {
        executeSQL(sqLiteDatabase);
    }

    private void executeSQL (SQLiteDatabase sqLiteDatabase){
        sqLiteDatabase.execSQL(SQL_CREATE_DRINKS);
        sqLiteDatabase.execSQL(SQL_CREATE_FOODS);
        sqLiteDatabase.execSQL(SQL_CREATE_MENUS);
    }
}
