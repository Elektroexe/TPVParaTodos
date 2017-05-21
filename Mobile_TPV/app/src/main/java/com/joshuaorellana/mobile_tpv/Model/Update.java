package com.joshuaorellana.mobile_tpv.model;

import com.joshuaorellana.mobile_tpv.model.business.DrinkDTO;
import com.joshuaorellana.mobile_tpv.model.business.FoodDTO;
import com.joshuaorellana.mobile_tpv.model.business.MenuDTO;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by Eduardo on 20/05/2017.
 */

public class Update {
    List<FoodDTO> Foods;
    List<DrinkDTO> Drinks;
    List<MenuDTO> Menus;

    public Update() {
        Foods = new ArrayList<>();
        Drinks = new ArrayList<>();
        Menus = new ArrayList<>();
    }

    public List<FoodDTO> getFoods() {
        return Foods;
    }

    public void setFoods(List<FoodDTO> foods) {
        Foods = foods;
    }

    public List<DrinkDTO> getDrinks() {
        return Drinks;
    }

    public void setDrinks(List<DrinkDTO> drinks) {
        Drinks = drinks;
    }

    public List<MenuDTO> getMenus() {
        return Menus;
    }

    public void setMenus(List<MenuDTO> menus) {
        Menus = menus;
    }
}
