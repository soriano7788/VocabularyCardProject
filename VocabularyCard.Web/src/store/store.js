import Vue from "vue";
import Vuex from "vuex";
import axios from "axios";
import localStorageUtil from "./localStorageUtil.js";
import * as statusCode from "./consts/statusCode.js";
import router from "../routes.js";

import CardSet from "./modules/cardSet.js";
import Card from "./modules/card.js";

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
        login: ({ commit, dispatch }, loginData) => {
            axios.post("account/SignIn", loginData)
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
        logout: ({ commit, dispatch }) => {
            commit("clearToken");
            commit("clearUserData");
            localStorageUtil.clearTokenData();
            localStorageUtil.clearUserData();
            router.push("/SignIn");
        },
        autoLogin: ({ commit }) => {
            // 先從 localStorage 找找看有無 userData
        }
    },
    modules: {
        CardSet,
        Card
    }
});