package com.joshuaorellana.mobile_tpv.Model;

import java.io.Serializable;

/**
 * Created by Joshua-OC on 04/05/2017.
 */

public abstract class ProductDTO implements Serializable {

    private int Id;
    private String Name;
    private double Price;
    private String Description;

    public ProductDTO(){

    }

    public ProductDTO(int id, String name, double price, String description) {
        this.Id = id;
        this.Name = name;
        this.Price = price;
        this.Description = description;
    }

    public int getId() {
        return Id;
    }

    public void setId(int $id) {
        this.Id = $id;
    }

    public String getName() {
        return Name;
    }

    public void setName(String name) {
        this.Name = name;
    }

    public double getPrice() {
        return Price;
    }

    public void setPrice(double price) {
        this.Price = price;
    }

    public String getDescription() {
        return Description;
    }

    public void setDescription(String description) {
        this.Description = description;
    }

    @Override
    public String toString() {
        return "Product{" +
                "$id=" + Id +
                ", Name='" + Name + '\'' +
                ", Price=" + Price +
                ", Description='" + Description + '\'' +
                '}';
    }
}
