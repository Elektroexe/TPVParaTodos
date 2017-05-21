package com.joshuaorellana.mobile_tpv.model.business;

import android.content.ContentValues;
import android.database.Cursor;

/**
 * Created by Joshua-OC on 08/05/2017.
 */

public class MenuDTO extends ProductDTO {

    private int PeopleNumber;
    private int Quantity;

    public MenuDTO() {
    }

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

    @Override
    public ProductDTO fromCursor(Cursor cursor) {
        MenuDTO product = new MenuDTO();
        product.setId(cursor.getInt(0));
        product.setPeopleNumber(cursor.getInt(1));
        product.setName(cursor.getString(2));
        product.setPrice(cursor.getDouble(3));
        product.setDescription(cursor.getString(4));
        return product;
    }
}
