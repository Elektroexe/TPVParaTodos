package com.joshuaorellana.mobile_tpv.model;

/**
 * Created by Joshua-OC on 16/05/2017.
 */

public class Notification {

    public String Title;
    public String Message;
    public int Type;



    public Notification(String title, String message, int type) {
        Title = title;
        Message = message;
        Type = type;
    }

    public String getTitle() {
        return Title;
    }

    public void setTitle(String title) {
        Title = title;
    }

    public String getMessage() {
        return Message;
    }

    public void setMessage(String message) {
        Message = message;
    }

    public int getType() {
        return Type;
    }

    public void setType(int type) {
        Type = type;
    }

    @Override
    public String toString() {
        return "Notification{" +
                "Title='" + Title + '\'' +
                ", Message='" + Message + '\'' +
                ", Type=" + Type +
                '}';
    }
}
