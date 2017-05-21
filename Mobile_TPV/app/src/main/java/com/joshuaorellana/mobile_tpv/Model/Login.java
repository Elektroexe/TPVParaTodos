package com.joshuaorellana.mobile_tpv.model;

/**
 * Created by Eduardo on 16/05/2017.
 */

public class Login {
    String access_token;
    String token_type;
    int expires_in;
    String userName;

    public Login() {}

    public String getAccess_token(){
        return access_token;
    }
}
