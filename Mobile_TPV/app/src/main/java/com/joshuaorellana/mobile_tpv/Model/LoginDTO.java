package com.joshuaorellana.mobile_tpv.Model;

/**
 * Created by Joshua-OC on 15/05/2017.
 */

public class LoginDTO {

    private String access_token;
    private String token_type;
    private int expires_in;
    private String userName;

    public LoginDTO() {
    }

    public LoginDTO(String access_token, String token_type, int expires_in, String userName) {
        this.access_token = access_token;
        this.token_type = token_type;
        this.expires_in = expires_in;
        this.userName = userName;
    }

    public String getAccess_token() {
        return access_token;
    }

    public void setAccess_token(String access_token) {
        this.access_token = access_token;
    }

    public String getToken_type() {
        return token_type;
    }

    public void setToken_type(String token_type) {
        this.token_type = token_type;
    }

    public int getExpires_in() {
        return expires_in;
    }

    public void setExpires_in(int expires_in) {
        this.expires_in = expires_in;
    }

    public String getUserName() {
        return userName;
    }

    public void setUserName(String userName) {
        this.userName = userName;
    }
}
