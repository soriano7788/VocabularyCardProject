<template>
  <div id="app">
    <app-header></app-header>
    <router-view></router-view>
  </div>
</template>

<script>
import Header from "./components/Header";

export default {
  name: "app",
  components: {
    appHeader: Header,
  },
  created: function() {
    // todo: check localStorage, try if can switch to has been in login status

    // 是否驗證通過
    if (!this.$store.getters.isAuthenticated) {
      // 目前是否 route 到 signin or register page
      if (!this.$store.getters.isInSignInOrRegisterPage) {
        // 因為尚未驗證，且並非在 登入 或 註冊 頁
        this.$router.push("/SignIn");
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
</style>
