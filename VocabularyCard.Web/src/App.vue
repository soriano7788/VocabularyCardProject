<template>
  <div id="app" class="container-fluid bg-root">
    <app-header></app-header>
    <div class="container-fluid pt-5">
      <div id="content" class="row pt-5">
        <router-view></router-view>
      </div>
      <div v-if="showLoading" class="full-screem">
        <div class="full-screem loading-mask"></div>
        <div
          class="spinner-border text-white"
          role="status"
          style="height:5rem; width: 5rem;"
        >
          <span class="visually-hidden">Loading...</span>
        </div>
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
    const promise = this.$store.dispatch("autoLogin");
    // todo: check localStorage, try if can switch to has been in login status

    promise
      .then(() => {
        // 是否驗證通過
        if (!this.$store.getters.isAuthenticated) {
          // 目前是否 route 到 signin or register page
          if (!this.$store.getters.isInSignInOrRegisterPage) {
            // 因為尚未驗證，且並非在 登入 或 註冊 頁
            this.$router.push("/SignIn");
            //this.$store.dispatch("logout");
          }
        } else {
          // 假如早就已經在首頁了，這邊就不用特意再 route 一次，
          // 應該可以寫在 beforeRouteEnter
          // if (!this.$store.getters.isInRootPage) {
          //   this.$router.push("/");
          // }
        }
      })
      .catch((error) => {
        alert("APP.vue catch");
        alert(error);
      });
  },
};
</script>

<style>
#app {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: white;
  min-height: 100vh;
  bottom: 0;
  padding-left: 0px;
  padding-right: 0px;
  padding-bottom: 10px;
}
#content {
  /* padding-left: 100px;
  padding-right: 100px; */
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
.bg-root {
  background-color: #0e0e0f;
}
.bg-main {
  background-color: #38444f;
}
.bg-create-panel {
  background-color: #4e5a66;
}
.z-index-2000 {
  z-index: 2000;
}
.z-index-3000 {
  z-index: 3000;
}
.z-index-4000 {
  z-index: 4000;
}
.z-index-5000 {
  z-index: 5000;
}
::-webkit-scrollbar {
  width: 12px;
}

::-webkit-scrollbar-track {
  -webkit-box-shadow: inset 0 0 6px rgb(5, 0, 0);
  border-radius: 10px;
}

::-webkit-scrollbar-thumb {
  border-radius: 10px;
  -webkit-box-shadow: inset 0 0 6px rgb(255, 255, 255);
}
/* .test:before {
  content: "";
  height: 100%;
  display: inline-block;
  vertical-align: middle;
} */
</style>
