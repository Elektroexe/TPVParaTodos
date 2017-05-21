package com.joshuaorellana.mobile_tpv.model.business;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

/**
 * Created by Joshua-OC on 09/05/2017.
 */

public class OrderDTO implements Serializable {

    private int Table_Id;
    private double Total;
    private String Date;
    private String Commentary;
    private List<DrinkDTO> Drinks;
    private List<FoodDTO> Foods;
    private List<MenuDTO> Menus;

    public OrderDTO() {}

    public OrderDTO(int Table_Id, String date) {

        this.Table_Id = Table_Id;
        this.Date = date;
        this.Total = 0;
        this.Commentary = "";
        this.Drinks = new ArrayList<>();
        this.Foods = new ArrayList<>();
        this.Menus = new ArrayList<>();

    }



    public int getTable_Id() {
        return Table_Id;
    }

    public void setTable_Id(int table_Id) {
        Table_Id = table_Id;
    }

    public double getTotal() {
        return Total;
    }

    public void setTotal(double total) {
        Total = total;
    }

    public String getDate() {
        return Date;
    }

    public void setDate(String date) {
        Date = date;
    }

    public String getCommentary() {
        return Commentary;
    }

    public void setCommentary(String commentary) {
        Commentary = commentary;
    }

    public List<DrinkDTO> getListDrinks() {
        return Drinks;
    }

    public void setListDrinks(List<DrinkDTO> listDrinks) {
        this.Drinks = listDrinks;
    }

    public List<FoodDTO> getListFoods() {
        return Foods;
    }

    public void setListFoods(List<FoodDTO> listFoods) {
        this.Foods = listFoods;
    }

    public List<MenuDTO> getListMenus() {
        return Menus;
    }

    public void setListMenus(List<MenuDTO> listMenus) {
        this.Menus = listMenus;
    }

    public void addDrink(DrinkDTO drink) {

        Drinks.add(drink);
        Total += (drink.getPrice() * drink.getQuantity());

    }

    public void addFood(FoodDTO food) {

        Foods.add(food);
        Total += (food.getPrice() * food.getQuantity());

    }

    @Override
    public String toString() {
        return "OrderDTO{" +
                "Table_Id=" + Table_Id +
                ", Total=" + Total +
                ", Date='" + Date + '\'' +
                ", Commentary='" + Commentary + '\'' +
                ", Drinks=" + Drinks +
                ", Foods=" + Foods +
                ", Menus=" + Menus +
                '}';
    }
}
