import Vue from 'vue'
import VueRouter from 'vue-router'
import App from './App'
import router from "./routes.js"
import axios from 'axios';
import store from "./store/store.js";

axios.defaults.baseURL = 'https://127.0.0.1/vocabulary/api';
axios.defaults.headers.get["Accepts"] = "application/json";
axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*';

// before sending request, try append access_token to header:authorization
const reqInterceptor = axios.interceptors.request.use(config => {
  const token = store.getters.token;
  if (store.getters.isAuthenticated) {
    config.headers.common['authorization'] = "bearer " + token.accessToken;
  }
  console.log('Request Interceptor', config)
  return config;
})

// client side first receive response from server
const resInterceptor = axios.interceptors.response.use(function (res) {
  console.log("Response Interceptor", res);

  // if (res.status == 401) {
  //   alert("Unauthorized");
  //   router.push("/SignIn");
  // }

  if (res.status == 200) {
    return res;
  }
}, function (error) {
  console.log("Response Interceptor error", error.response);
  if (error.response.status == 401) {
    // 401 可能有三種
    // 1. 帳密登入失敗
    // 2. access_token 無效 (過期 或 不存在)
    // 3. refresh_token 無效 (過期 或 不存在)


    alert("Unauthorized");
    router.push("/SignIn");
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
