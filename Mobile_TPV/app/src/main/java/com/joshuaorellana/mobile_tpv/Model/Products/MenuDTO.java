package com.joshuaorellana.mobile_tpv.Model.Products;

import android.content.ContentValues;

import com.joshuaorellana.mobile_tpv.Model.ProductDTO;

/**
 * Created by Joshua-OC on 08/05/2017.
 */

public class MenuDTO extends ProductDTO {

    private int PeopleNumber;
    private int Quantity;

    public MenuDTO(int id, String name, double price, String description, int peopleNumber) {

        super(id, name, price, description);
        this.PeopleNumber = peopleNumber;
        this.Quantity = 0;
    }

    public int getPeopleNumber() {
        return PeopleNumber;
    }

    public void setPeopleNumber(int peopleNumber) {
        PeopleNumber = peopleNumber;
    }

    public int getQuantity() {
        return Quantity;
    }

    public void setQty(int qty) {
        this.Quantity = qty;
    }

    @Override
    public String toString() {
        return "MenuDTO{" +
                "PeopleNumber=" + PeopleNumber +
                ", Qty=" + Quantity +
                '}';
    }

    @Override
    public ContentValues toContentValues() {
        ContentValues cv = new ContentValues();
        cv.put("Id", getId());
        cv.put("PeopleNumber", PeopleNumber);
        cv.put("Name", getName());
        cv.put("Price", getPrice());
        cv.put("Description", getDescription());
        return cv;
    }
}
