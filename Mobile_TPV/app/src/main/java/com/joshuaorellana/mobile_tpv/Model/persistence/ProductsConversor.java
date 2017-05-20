package com.joshuaorellana.mobile_tpv.Model.persistence;

import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;

import com.joshuaorellana.mobile_tpv.Model.ProductDTO;
import com.joshuaorellana.mobile_tpv.Model.Products.*;
import com.joshuaorellana.mobile_tpv.Model.Update;

import java.lang.reflect.Method;
import java.util.ArrayList;
import java.util.List;

public class ProductsConversor {
    ProductsSQLiteHelper helper;

    public ProductsConversor(ProductsSQLiteHelper helper) {
        this.helper = helper;
    }

    public <T> List<T> getProducts(Class<T> classProduct){
        if (classProduct.getSuperclass() == ProductDTO.class) {
            SQLiteDatabase db = helper.getReadableDatabase();
            Cursor cursor = db.query(true, classProduct.getSimpleName().replace("DTO", "s").toUpperCase(), null, null, null, null, null, null, null, null);
            ArrayList<T> products = new ArrayList<>();
            if (cursor.moveToFirst()){
                do {
                    Object aux;
                    try {
                        aux = Class.forName(classProduct.getName()).newInstance();
                        Method methodCursor = classProduct.getMethod("fromCursor", Cursor.class);
                        products.add((T) methodCursor.invoke(aux, cursor));
                    } catch (Exception e) {
                        return null;
                    } finally {
                        db.close();
                    }
                } while (cursor.moveToNext());
            }
            db.close();
            return products;
        }
        return null;
    }

    public void updateProducts(Update products){
        SQLiteDatabase db = helper.getReadableDatabase();
        for (FoodDTO food : products.getFoods()){
            db.insertWithOnConflict("FOODS", null, food.toContentValues(), SQLiteDatabase.CONFLICT_REPLACE);
        }
        for (DrinkDTO drink : products.getDrinks()){
            db.insertWithOnConflict("DRINKS", null, drink.toContentValues(), SQLiteDatabase.CONFLICT_REPLACE);
        }
        for (MenuDTO menu : products.getMenus()){
            db.insertWithOnConflict("MENUS", null, menu.toContentValues(), SQLiteDatabase.CONFLICT_REPLACE);
        }
        db.close();
    }

    public void closeConnection(){
        helper.close();
    }
}
