package com.joshuaorellana.mobile_tpv.Model.Products;

import android.content.ContentValues;

import com.joshuaorellana.mobile_tpv.Model.ProductDTO;

/**
 * Created by Joshua-OC on 08/05/2017.
 */

public class FoodDTO extends ProductDTO {

    private String FamilyDish;
    private int Quantity;

    public FoodDTO(int id, String name, double price, String description, String familyDish) {

        super(id, name, price, description);
        this.FamilyDish = familyDish;
        this.Quantity = 0;
    }

    public String getFamilyDish() {
        return FamilyDish;
    }

    public void setFamilyDish(String familyDish) {
        FamilyDish = familyDish;
    }

    public int getQuantity() {
        return Quantity;
    }

    public void setQuantity(int qty) {
        this.Quantity = qty;
    }

    @Override
    public String toString() {
        return "FoodDTO{" +
                "FamilyDish='" + FamilyDish + '\'' +
                ", Qty=" + Quantity +
                '}';
    }

    @Override
    public ContentValues toContentValues() {
        ContentValues cv = new ContentValues();
        cv.put("Id", getId());
        cv.put("FamilyDish", FamilyDish);
        cv.put("Name", getName());
        cv.put("Price", getPrice());
        cv.put("Description", getDescription());
        return cv;
    }
}
