package com.joshuaorellana.mobile_tpv.Model.persistence;

import android.content.ContentValues;
import android.database.sqlite.SQLiteDatabase;

import com.joshuaorellana.mobile_tpv.Model.ProductDTO;
import com.joshuaorellana.mobile_tpv.Model.Products.*;

import java.util.List;

/**
 * Created by Eduardo on 12/05/2017.
 */

public class ProductsConversor {
    ProductsSQLiteHelper helper;

    public ProductsConversor(ProductsSQLiteHelper helper) {
        this.helper = helper;
    }

    public void getProducts(Class classProduct){
        if (classProduct.getSuperclass() == ProductDTO.class) {
            SQLiteDatabase db = helper.getReadableDatabase();
            
            db.close();
        }
    }

    public void refreshProducts(List<ProductDTO> products, Class classProduct){
        if (classProduct.getSuperclass() == ProductDTO.class) {
            SQLiteDatabase db = helper.getReadableDatabase();
            String nameTable = classProduct.getName().replace("DTO", "S").toUpperCase();
            db.delete(nameTable, null, null);
            for (ProductDTO drink : products){
                db.insert(nameTable, null, drink.toContentValues());
            }
            db.close();
        }
    }

    public void closeConnection(){
        helper.close();
    }
}
