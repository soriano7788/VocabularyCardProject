import Vue from 'vue'
import VueRouter from 'vue-router'
import App from './App'
import router from "./routes.js"
import axios from 'axios';
import axiosAuth from './axios-auth.js';
import store from "./store/store.js";


axios.defaults.baseURL = 'https://127.0.0.1/vocabulary/api';
axios.defaults.headers.get["Accepts"] = "application/json";
axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*';

// before sending request, try append access_token to header:authorization
const reqInterceptor = axios.interceptors.request.use(config => {
  const token = store.getters.token;
  // 應該也要檢查 accesstoken 是否過期
  // 假如是經過 401 後，重發 request，這邊的 authorization 沒有被換成新的 accessToken
  // 就算有下面設定也一樣....

  // 有需要用 common 嗎?  是不是被初始化一次後，就沒辦法改了????
  delete config.headers.common['authorization'];
  config.headers.common['authorization'] = "bearer " + token.accessToken;
  // alert(config.headers.common['authorization']);
  // 上面 alert 看起來有重設，但是 browser 送出去的 request，authorization 還是舊的 access token

  console.log('Request Interceptor', config);

  store.dispatch("setLoadingSpinnerVisibility", true);
  return config;

})

// client side first receive response from server
const resInterceptor = axios.interceptors.response.use(function (res) {
  store.dispatch("setLoadingSpinnerVisibility", false);
  console.log("Response Interceptor", res);

  if (res.status == 200) {
    return res;
  }
}, function (error) {
  store.dispatch("setLoadingSpinnerVisibility", false);
  console.log("Response Interceptor error", error);

  const originalRequest = error.config;
  if (error.response.status == 401 && !originalRequest._retry) {
    // todo: 還要先確定 refresh token 是否過期，假如也過期的話，就只能重新輸入帳密登入了



    originalRequest._retry = true;
    // alert("Unauthorized 401");

    // 401 可能有三種
    // 1. 帳密登入失敗
    // 2. access_token 無效 (過期 或 不存在)
    // 3. refresh_token 無效 (過期 或 不存在)

    const token = store.getters.token;
    return axiosAuth.post("GetAccessToken", JSON.stringify(token.refreshToken), { headers: { "Content-Type": "application/json" } })
      .then(res => {
        const result = res.data;

        const newAccessToken = result.data;
        if (result.statusCode == "000") {
          store.commit("setAccessTokenData", {
            token: newAccessToken.Token,
            expiredDateTime: newAccessToken.ExpiredDateTime
          });

          originalRequest.headers['authorization'] = "bearer " + newAccessToken.Token;
          return axios(originalRequest);
        }
        // Promise.resolve(newAccessToken.Token);
      })
      .catch(error => {
        console.log(error);
        Promise.reject(error);
      });
    // 這樣沒 return promise.reject() 的話，又直接跑到 then 那邊去了...


    /////////////////////

    // const token = store.getters.token;
    // const promise = store.dispatch("refreshAccessToken", token.refreshToken);
    // promise
    //   .then(newAccessToken => {
    //     const originalRequest = error.config;

    //     originalRequest.headers['authorization'] = "bearer " + newAccessToken;
    //     axios(originalRequest).then(res => {
    //       return res;
    //     });

    //   })
    //   .catch(data => {
    //   });

    ////////////////////////


    // 這邊 return error，會接到原本發出 request 的 then 裡面.....
    // 什麼都不 return 也會接到原本發出 request 的 then 裡面.....
    // return error;

    // 目前這樣寫才會接到原本發出 request 的 catch 裡面.....
  }
  return Promise.reject(error);
});




Vue.config.productionTip = false

Vue.use(VueRouter);

// const router = new VueRouter({
//   mode: "history",
//   base: "/vocabulary/",
//   routes: routes
// });

new Vue({
  el: '#app',
  router: router,
  store: store,
  render: h => h(App)
})
