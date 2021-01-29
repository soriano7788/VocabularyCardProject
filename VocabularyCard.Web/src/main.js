import Vue from 'vue'
import VueRouter from 'vue-router'
import App from './App'
import router from "./routes.js"
import axios from 'axios';
// import axiosAuth from './axios-auth.js';
import store from "./store/store.js";


axios.defaults.baseURL = 'https://127.0.0.1/vocabulary/api';
axios.defaults.headers.get["Accepts"] = "application/json";
axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*';

// before sending request, try append access_token to header:authorization
const reqInterceptor = axios.interceptors.request.use(config => {
  const token = store.getters.token;
  // 應該也要檢查 accesstoken 是否過期
  alert("before sending request");

  // if (store.getters.isAuthenticated) {
  // }

  alert("append access token to request header");
  alert(token.accessToken);

  // 假如是經過 401 後，重發 request，這邊的 authorization 沒有被換成新的 accessToken
  // 就算有下面設定也一樣....
  config.headers.common['authorization'] = "bearer " + token.accessToken;
  alert(config.headers.common['authorization']);
  // 上面 alert 看起來有重設，但是 browser 送出去的 request，authorization 還是舊的 access token


  console.log('Request Interceptor', config)
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

  console.log("Response Interceptor error", error.response);
  if (error.response.status == 401) {
    // 401 可能有三種
    // 1. 帳密登入失敗
    // 2. access_token 無效 (過期 或 不存在)
    // 3. refresh_token 無效 (過期 或 不存在)
    console.log(error);
    console.log(error.response);


    // todo: 如何縮短 if 階層呢?
    const token = store.getters.token;

    // todo: 這段要改，要確定比對 的 兩個時間是同一基準點，要嘛都 +8 ，不然就全都 UTC
    var accessTokenIsExpired = (new Date() > token.accessTokenExpirationDate);
    accessTokenIsExpired = true;
    var refreshTokenIsExpired = (new Date() > token.refreshTokenExpirationDate);

    // access token 已過期
    if (accessTokenIsExpired) {
      console.log("accessTokenIsExpired");
      // refresh token 也過期了
      if (refreshTokenIsExpired) {
        console.log("refreshTokenIsExpired");
        // 登出，但不要立刻轉畫面
        // 先跳出驗證過期訊息，再看要先停在原畫面，還是
      } else {
        // access token 過期，refresh token 還沒過期，
        // 去 call api 拿新的 access token
        const promise = store.dispatch("refreshAccessToken", token.refreshToken);
        promise
          .then(newAccessToken => {
            alert("refresh success");

            // 這邊重發 request 好像就不會經過 before request interceptor 了?
            // 假如沒經過  before request interceptor，那就只能在這邊重設 header 的 access token 了
            const originalRequest = error.config;

            // can not set authorization property， 為啥????
            // originalRequest.headers.common['authorization'] = "bearer " + newAccessToken;

            axios(originalRequest);
          })
          .catch(data => {
            alert("refresh fail");
            alert(data);
          });

        // 當 a resolve，就重發 originalRequest


      }
    } else {
    }


    alert("Unauthorized");
    //store.dispatch("logout");
  }
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
