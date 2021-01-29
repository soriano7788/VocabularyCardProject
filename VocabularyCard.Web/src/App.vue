<template>
  <div id="app" class="container-fluid">
    <app-header></app-header>
    <div class="row">
      <router-view></router-view>
    </div>
    <div v-if="showLoading" class="full-screem">
      <div class="full-screem loading-mask"></div>
      <div
        class="spinner-border text-dark"
        role="status"
        style="height:5rem; width: 5rem;"
      >
        <span class="visually-hidden">Loading...</span>
      </div>
    </div>
  </div>
</template>

<script>
import Header from "./components/Header";

export default {
  name: "app",
  components: {
    appHeader: Header,
  },
  computed: {
    showLoading: function() {
      return this.$store.getters.showLoadingSpinner;
    },
  },
  created: function() {
    // this.$store.dispatch("autoLogin");
    // todo: check localStorage, try if can switch to has been in login status

    // 是否驗證通過
    if (!this.$store.getters.isAuthenticated) {
      // 目前是否 route 到 signin or register page
      if (!this.$store.getters.isInSignInOrRegisterPage) {
        // 因為尚未驗證，且並非在 登入 或 註冊 頁
        this.$router.push("/SignIn");
        //this.$store.dispatch("logout");
      }
    }
  },
};
</script>

<style>
#app {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
.full-screem {
  position: absolute;
  left: 0;
  top: 0;
  right: 0;
  bottom: 0;
  display: flex;
  align-items: center; /* 垂直置中 */
  justify-content: center; /* 水平置中 */
}
.loading-mask {
  background-color: black;
  opacity: 0.25;
}
/* .test:before {
  content: "";
  height: 100%;
  display: inline-block;
  vertical-align: middle;
} */
</style>
