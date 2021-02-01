import Vue from "vue";
import Vuex from "vuex";
import axios from "axios";
import localStorageUtil from "./localStorageUtil.js";
import * as statusCode from "./consts/statusCode.js";
import router from "../routes.js";

import CardSet from "./modules/cardSet.js";
import Card from "./modules/card.js";

import axiosAuth from "../axios-auth.js";

Vue.use(Vuex);

export default new Vuex.Store({
    state: {
        // token: {
        //     refreshToken: "",
        //     accessToken: "",
        //     refreshTokenExpiredIn: 0,
        //     accessTokenExpiredIn: 0
        // },
        showLoadingSpinner: false,
        token: null,
        userData: null,
    },
    getters: {
        showLoadingSpinner: state => {
            return state.showLoadingSpinner;
        },
        token: state => {
            return state.token;
        },
        isAuthenticated: state => {
            return state.token != null;
        },
        isInSignInOrRegisterPage: state => {
            const curFullPath = router.currentRoute.fullPath.toLowerCase();
            const inSignInPage = curFullPath === "/SignIn".toLowerCase();
            const inRegisterPage = curFullPath === "/Register".toLowerCase();
            return (inSignInPage || inRegisterPage);
        },
        isInRootPage: state => {
            const curFullPath = router.currentRoute.fullPath.toLowerCase();
            const isInRootPage = curFullPath === "/".toLowerCase();
            return isInRootPage;
        }
    },
    mutations: {
        setLoadingSpinnerVisibility: (state, visible) => {
            state.showLoadingSpinner = visible;
        },
        setUserData: (state, userData) => {
        },
        setTokenData: (state, payload) => {
            state.token = payload;
        },
        setAccessTokenData: (state, accessToken) => {
            var token = state.token;

            // 預防 state 還沒存放 token 的情形
            if (token == null) {
                token = localStorageUtil.getTokenData();
            }

            token.accessToken = accessToken.token;
            token.accessTokenExpiredDateTime = accessToken.expiredDateTime;
            localStorageUtil.setTokenData(token);
        },
        clearToken: state => {
            state.token = null;
        },
        clearUserData: state => {
            state.userData = null;
        }
    },
    actions: {
        setLoadingSpinnerVisibility: ({ commit }, visible) => {
            commit("setLoadingSpinnerVisibility", visible);
        },
        login: ({ commit }, loginData) => {
            axiosAuth.post("SignIn", loginData)
                .then((res) => {
                    const result = res.data;
                    if (result.statusCode == statusCode.SUCCESS && result.data.IsAuthenticated) {
                        const authResult = result.data;

                        const tokenData = {
                            refreshToken: authResult.RefreshToken,
                            accessToken: authResult.AccessToken,
                            refreshTokenExpiredDateTime: authResult.RefreshTokenExpiredDateTime,
                            accessTokenExpiredDateTime: authResult.AccessTokenExpiredDateTime
                        };
                        // state 儲存 token 資訊
                        commit("setTokenData", tokenData);
                        // localStorage 儲存 token 資訊
                        localStorageUtil.setTokenData(tokenData);

                        // 登入成功，到首頁
                        router.push("/");
                    } else {
                        console.log("error: " + result.data);
                    }

                })
                .catch(error => console.log(error));
        },
        logout: ({ commit }) => {
            commit("clearToken");
            commit("clearUserData");
            localStorageUtil.clearTokenData();
            localStorageUtil.clearUserData();
            router.push("/SignIn");
        },
        autoLogin: ({ commit, dispatch }) => {
            // autoLogin 應該只有在前端 web 初始化時，會被呼叫吧?
            // 這樣的話就先 return promise 回去，讓初始 root component 在 created evnet，
            // 先等這邊處理好，再進行下一步動作
            return new Promise((resolve, reject) => {
                // 先決定要用哪種流程處理策略
                const strategy = determineProcessStrategy();
                const tokenData = localStorageUtil.getTokenData();
                switch (strategy) {
                    case JUST_LOGIN:
                        commit("setTokenData", tokenData);
                        resolve();
                        break;
                    case RE_LOGIN:
                        resolve();
                        break;
                    case REFRESH_ACCESS_TOKEN:
                        const promise = dispatch("refreshAccessToken", tokenData.refreshToken);
                        promise
                            .then(() => {
                                commit("setTokenData", tokenData);
                                resolve();
                            })
                            .catch(error => {
                                alert("auto login refresh accessToken error");
                                alert(error);
                            });
                        break;
                    default:
                        reject();
                }

            });
        },
        refreshAccessToken: ({ commit, state }, refreshToken) => {
            //Promise.resolve
            // return new Promise((resolve, reject) => {
            // });

            return axiosAuth.post("GetAccessToken", JSON.stringify(refreshToken), { headers: { "Content-Type": "application/json" } })
                .then(res => {
                    const result = res.data;
                    // 也需要新的 accessToken 的過期時間
                    const newAccessToken = result.data;
                    if (result.statusCode == statusCode.SUCCESS) {
                        commit("setAccessTokenData", {
                            token: newAccessToken.Token,
                            expiredDateTime: newAccessToken.ExpiredDateTime
                        });
                    }

                    // resolve(newAccessToken.Token);
                    return Promise.resolve(newAccessToken.Token);
                })
                .catch(error => {
                    console.log(error);
                    // reject(error);
                    return Promise.reject(error);
                });
        }
    },
    modules: {
        CardSet,
        Card
    }
});

const JUST_LOGIN = "JUST_LOGIN";
const RE_LOGIN = "RE_LOGIN";
const REFRESH_ACCESS_TOKEN = "REFRESH_ACCESS_TOKEN";

function determineProcessStrategy() {
    // case 分類
    // 0. localStorage token is null
    // 1. access token 過期，refresh token 過期
    // 2. access token 過期，refresh token 有效
    // 3. access token 有效，refresh token 過期
    // 4. access token 有效，refresh token 有效

    // 3 和 4 的 case 都是直接 resolve 就好了，access token 還能用就好，auto login
    // 0 和 1 的 case 都是直接重新輸入帳號密碼
    // 2 的 case，必須先 ajax 更新 access token 後，才能 resolve

    // 歸納出的分類，做出對應的處理策略，策略模式?
    // JUST_LOGIN 第一種(3 和 4)， access token 有效，直接 commit("setTokenData", tokenData); 後 resolve
    // RE_LOGIN  第二種(0 和 1)， 兩種 token 都過期了，或是連資料都沒有，只能重新輸入帳密登入了
    // REFRESH_ACCESS_TOKEN 第三種(2)， access token 已過期， refresh token 還有效，所以就用 refresh token 去換新的 access token 回來就可以了

    const tokenData = localStorageUtil.getTokenData();
    // case 0
    if (tokenData == null) {
        return RE_LOGIN;
    }

    const refreshTokenExpiredDateTime = tokenData.refreshTokenExpiredDateTime;
    const accessTokenExpiredDateTime = tokenData.accessTokenExpiredDateTime;
    const now = new Date();

    const isAccessTokenInValidityPeriod = (new Date(accessTokenExpiredDateTime) - now > 0);
    const isRefreshTokenInValidityPeriod = (new Date(refreshTokenExpiredDateTime) - now > 0);

    // refresh 和 access token 都過期了，必需輸入帳密重新登入
    // case 1
    if (!isAccessTokenInValidityPeriod && !isRefreshTokenInValidityPeriod) {
        return RE_LOGIN;
    }

    // 只要 accessToken 還沒過期，就可以繼續操作，可自動登入
    // case 3 case 4
    if (isAccessTokenInValidityPeriod) {
        return JUST_LOGIN;
    }


    // 需要用 refresh token 拿新的 accesstoken
    // case 5
    return REFRESH_ACCESS_TOKEN;
}