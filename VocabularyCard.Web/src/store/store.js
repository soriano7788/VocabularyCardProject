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

                        // expiresIn 單位是秒，要轉成 millisecond，所以要乘 1000
                        const now = new Date();
                        const refreshTokenExpirationDate = new Date(now.getTime() + authResult.RefreshTokenExpiresIn * 1000);
                        const accessTokenExpirationDate = new Date(now.getTime() + authResult.AccessTokenExpiresIn * 1000);

                        const tokenData = {
                            refreshToken: authResult.RefreshToken,
                            accessToken: authResult.AccessToken,
                            refreshTokenExpirationDate: refreshTokenExpirationDate,
                            accessTokenExpirationDate: accessTokenExpirationDate
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
            // 先從 localStorage 找找看有無 userData
            const tokenData = localStorageUtil.getTokenData();

            if (tokenData == null) {
                return;
            }

            // new Date() 會傳回物件
            // Date() 則是字串
            const now = new Date();

            const refreshToken = tokenData.refreshToken;
            const refreshTokenExpirationDate = tokenData.refreshTokenExpirationDate;

            if (refreshToken == null) {
                return;
            }
            if (refreshTokenExpirationDate == null) {
                return;
            }
            if (now > refreshTokenExpirationDate) {
                // refresh token 過期
                return;
            }

            const accessToken = tokenData.accessToken;
            const accessTokenExpirationDate = tokenData.accessTokenExpirationDate;
            if (accessToken == null) {
                return;
            }
            if (accessTokenExpirationDate == null) {
                return;
            }

            if (now > accessTokenExpirationDate) {
                // refresh token 過期
                var promise = dispatch("refreshAccessToken", refreshToken);
                console.log();

                // return;
            } else {
                // 都有效
                commit("setTokenData", tokenData);
            }
        },
        refreshAccessToken: ({ commit, state }, refreshToken) => {
            axiosAuth.post("GetAccessToken", refreshToken)
                .then(res => {
                    const result = res.data;
                    // 也需要新的 accessToken 的過期時間
                    const newAccessToken = result.data;
                    if (result.statusCode == statusCode.SUCCESS) {
                        console.log("get success: " + result);
                    }
                })
                .catch(error => {
                    console.log(error);
                });
        }
    },
    modules: {
        CardSet,
        Card
    }
});