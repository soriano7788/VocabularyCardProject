import * as types from "./consts/localStorage.js";

const actions = {
    [types.SET_TOKEN_DATA]: (context, tokenData) => {
        localStorage.setItem("refreshToken", tokenData.refreshToken);
        localStorage.setItem("accessToken", tokenData.accessToken);
        localStorage.setItem("refreshTokenExpirationDate", tokenData.refreshTokenExpirationDate);
        localStorage.setItem("accessTokenExpirationDate", tokenData.accessTokenExpirationDate);
    },
    [types.GET_TOKEN_DATA]: () => {
        const tokenData = {
            refreshToken: localStorage.getItem("refreshToken"),
            accessToken: localStorage.getItem("accessToken"),
            refreshTokenExpirationDate: localStorage.getItem("refreshTokenExpirationDate"),
            accessTokenExpirationDate: localStorage.getItem("accessTokenExpirationDate")
        };
        return tokenData;
    },
    [types.CLEAR_TOKEN_DATA]: () => {
        localStorage.removeItem("refreshToken");
        localStorage.removeItem("accessToken");
        localStorage.removeItem("refreshTokenExpirationDate");
        localStorage.removeItem("accessTokenExpirationDate");
    },
    [types.CLEAR_USER_DATA]: () => {

    }
}

export default {
    actions: actions
}