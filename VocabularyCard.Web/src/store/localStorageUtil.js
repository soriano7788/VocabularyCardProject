// const REFRESH_TOKEN = "refreshToken";
// const REFRESH_TOKEN_EXPIRED_DATE = "refreshTokenExpiredDate";
// const ACCESS_TOKEN = "accessToken";
// const ACCESS_TOKEN_EXPIRED_DATE = "accessTokenExpiredDate";

const REFRESH_TOKEN = "REFRESH_TOKEN";
const REFRESH_TOKEN_EXPIRED_DATE = "REFRESH_TOKEN_EXPIRED_DATE";
const ACCESS_TOKEN = "ACCESS_TOKEN";
const ACCESS_TOKEN_EXPIRED_DATE = "ACCESS_TOKEN_EXPIRED_DATE";

export default {
    setTokenData: (tokenData) => {
        console.log(tokenData);
        localStorage.setItem(REFRESH_TOKEN, tokenData.refreshToken);
        localStorage.setItem(ACCESS_TOKEN, tokenData.accessToken);
        localStorage.setItem(REFRESH_TOKEN_EXPIRED_DATE, tokenData.refreshTokenExpiredDateTime);
        localStorage.setItem(ACCESS_TOKEN_EXPIRED_DATE, tokenData.accessTokenExpiredDateTime);
    },
    getTokenData: () => {
        const refreshToken = localStorage.getItem(REFRESH_TOKEN);
        const accessToken = localStorage.getItem(ACCESS_TOKEN);

        const refreshTokenExpiredDateTime = localStorage.getItem(REFRESH_TOKEN_EXPIRED_DATE);
        const accessTokenExpiredDateTime = localStorage.getItem(ACCESS_TOKEN_EXPIRED_DATE);

        if (refreshToken == null) {
            return null;
        }
        if (accessTokenExpiredDateTime == null) {
            return null;
        }

        const tokenData = {
            refreshToken: refreshToken,
            accessToken: accessToken,
            refreshTokenExpiredDateTime: refreshTokenExpiredDateTime,
            accessTokenExpiredDateTime: accessTokenExpiredDateTime
        };
        return tokenData;
    },
    clearTokenData: () => {
        localStorage.removeItem(REFRESH_TOKEN);
        localStorage.removeItem(ACCESS_TOKEN);
        localStorage.removeItem(REFRESH_TOKEN_EXPIRED_DATE);
        localStorage.removeItem(ACCESS_TOKEN_EXPIRED_DATE);
    },
    clearUserData: () => {

    }
}