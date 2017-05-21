package com.joshuaorellana.mobile_tpv.model.business;

import java.io.Serializable;

/**
 * Created by Joshua-OC on 04/05/2017.
 */

public class TableDTO implements Serializable {

    private int Id;
    private int MaxPeople;
    private boolean Empty;
    private int ZoneId;

    public TableDTO() {
    }

    public TableDTO(int id, int maxPeople, boolean empty, int zoneId) {
        Id = id;
        MaxPeople = maxPeople;
        Empty = empty;
        ZoneId = zoneId;
    }

    public int getId() {
        return Id;
    }

    public void setId(int id) {
        Id = id;
    }

    public int getMaxPeople() {
        return MaxPeople;
    }

    public void setMaxPeople(int maxPeople) {
        MaxPeople = maxPeople;
    }

    public boolean isEmpty() {
        return Empty;
    }

    public void setEmpty(boolean empty) {
        Empty = empty;
    }

    public int getZoneId() {
        return ZoneId;
    }

    public void setZoneId(int zoneId) {
        ZoneId = zoneId;
    }

    @Override
    public String toString() {
        return "TableDTO{" +
                "Id=" + Id +
                ", MaxPeople=" + MaxPeople +
                ", Empty=" + Empty +
                ", ZoneId=" + ZoneId +
                '}';
    }
}
