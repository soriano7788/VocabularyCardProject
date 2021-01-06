export default {
    setTokenData: (tokenData) => {
        localStorage.setItem("refreshToken", tokenData.refreshToken);
        localStorage.setItem("accessToken", tokenData.accessToken);
        localStorage.setItem("refreshTokenExpirationDate", tokenData.refreshTokenExpirationDate);
        localStorage.setItem("accessTokenExpirationDate", tokenData.accessTokenExpirationDate);
    },
    getTokenData: () => {
        const tokenData = {
            refreshToken: localStorage.getItem("refreshToken"),
            accessToken: localStorage.getItem("accessToken"),
            refreshTokenExpirationDate: localStorage.getItem("refreshTokenExpirationDate"),
            accessTokenExpirationDate: localStorage.getItem("accessTokenExpirationDate")
        };
        return tokenData;
    },
    clearTokenData: () => {
        localStorage.removeItem("refreshToken");
        localStorage.removeItem("accessToken");
        localStorage.removeItem("refreshTokenExpirationDate");
        localStorage.removeItem("accessTokenExpirationDate");
    },
    clearUserData: () => {

    }
}