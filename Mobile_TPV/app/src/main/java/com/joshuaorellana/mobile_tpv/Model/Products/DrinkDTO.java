package com.joshuaorellana.mobile_tpv.Model.Products;

import com.joshuaorellana.mobile_tpv.Model.ProductDTO;

/**
 * Created by Joshua-OC on 04/05/2017.
 */

public class DrinkDTO extends ProductDTO {

    private int Capacity;
    private String TypeBottle;
    private boolean Soda;
    private boolean Alcohol;
    private int Quantity;

    public DrinkDTO(){}

    public DrinkDTO(int capacity, String typeBottle, boolean soda, boolean alcohol,
                 int id, String name, double price, String description) {

        super(id, name, price, description);
        this.Capacity = capacity;
        this.TypeBottle = typeBottle;
        this.Soda = soda;
        this.Alcohol = alcohol;
        this.Quantity = 0;
    }

    public int getCapacity() {
        return Capacity;
    }

    public void setCapacity(int capacity) {
        this.Capacity = capacity;
    }

    public String getTypeBottle() {
        return TypeBottle;
    }

    public void setTypeBottle(String typeBottle) {
        this.TypeBottle = typeBottle;
    }

    public boolean isSoda() {
        return Soda;
    }

    public void setSoda(boolean soda) {
        this.Soda = soda;
    }

    public boolean isAlcohol() {
        return Alcohol;
    }

    public void setAlcohol(boolean alcohol) {
        this.Alcohol = alcohol;
    }

    public int getQuantity() {
        return Quantity;
    }

    public void setQuantity(int qty) {
        this.Quantity = qty;
    }

    @Override
    public String toString() {
        return super.toString() + "Drink{" +
                "Capacity=" + Capacity +
                ", TypeBottle='" + TypeBottle + '\'' +
                ", Soda=" + Soda +
                ", Alcohol=" + Alcohol +
                '}';
    }
}
