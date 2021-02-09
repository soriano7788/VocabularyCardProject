<template>
  <nav
    id="nav-header"
    class="navbar navbar-dark bg-header navbar-expand-lg position-fixed z-index-5000"
  >
    <div class="container-fluid">
      <a class="navbar-brand" href="#">Logo icon</a>
      <button
        class="navbar-toggler"
        type="button"
        data-bs-toggle="collapse"
        data-bs-target="#navbarSupportedContent"
        aria-controls="navbarSupportedContent"
        aria-expanded="false"
        aria-label="Toggle navigation"
        @click.prevent="toggleNavBar"
      >
        <span class="navbar-toggler-icon"></span>
      </button>
      <div
        class=" navbar-collapse"
        id="navbarSupportedContent"
        v-bind:class="[showNavBar]"
      >
        <ul class="navbar-nav  mb-2 mb-lg-0">
          <!-- <li class="nav-item" v-if="isAuth">
            <router-link class="nav-link active" tag="a">Home</router-link>
          </li> -->

          <router-link
            to="/"
            class="nav-item"
            activeClass="active"
            tag="li"
            v-if="isAuth"
          >
            <a class="nav-link active" aria-current="page" href="#">
              {{ $t("COMMON.HOME") }}
            </a>
          </router-link>
          <router-link
            to="/CardSets"
            class="nav-item"
            activeClass="active"
            tag="li"
            v-if="isAuth"
          >
            <a class="nav-link active" aria-current="page" href="#">{{
              $t("COMMON.CARD_SETS")
            }}</a>
          </router-link>
          <router-link
            to="/Register"
            class="nav-item"
            activeClass="active"
            tag="li"
            v-if="!isAuth"
          >
            <a class="nav-link active" aria-current="page" href="#">{{
              $t("AUTH.REGISTER")
            }}</a>
          </router-link>
          <router-link
            to="/SignIn"
            class="nav-item"
            activeClass="active"
            tag="li"
            v-if="!isAuth"
          >
            <a class="nav-link active" aria-current="page" href="#">{{
              $t("AUTH.SIGN_IN")
            }}</a>
          </router-link>

          <!-- <router-link
            to="/SignOut"
            class="nav-item"
            activeClass="active"
            tag="li"
            v-if="isAuth"
          >
            <a class="nav-link active" aria-current="page" href="#">Sign out</a>
          </router-link> -->
        </ul>
        <ul class="navbar-nav mb-2 mb-lg-0" v-if="isAuth">
          <li class="nav-item" v-if="isAuth">
            <a
              @click.prevent="signOut"
              class="nav-link active"
              aria-current="page"
              href="#"
              >{{ $t("AUTH.SIGN_OUT") }}</a
            >
          </li>
          <li class="nav-item dropdown">
            <a
              class="nav-link dropdown-toggle"
              href="#"
              id="navbarDropdown"
              role="button"
              data-bs-toggle="dropdown"
              aria-expanded="false"
            >
              User name
            </a>
            <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
              <li>
                <a class="dropdown-item" href="#">Action</a>
              </li>
              <li>
                <a class="dropdown-item" href="#">Another action</a>
              </li>
              <li><hr class="dropdown-divider" /></li>
              <li>
                <a class="dropdown-item" href="#">Something else here</a>
              </li>
            </ul>
          </li>
        </ul>
      </div>
    </div>
  </nav>
</template>

<script>
export default {
  data: function() {
    return {
      showNavBar: "collapse",
    };
  },
  computed: {
    isAuth: function() {
      return this.$store.getters.isAuthenticated;
    },
  },
  methods: {
    signOut: function() {
      this.$store.dispatch("logout");
    },
    toggleNavBar() {
      this.showNavBar = this.showNavBar == "collapse" ? "" : "collapse";
    },
  },
};
</script>

<style scoped>
.bg-header {
  background-color: #38444f;
}
#nav-header {
  min-width: 100vw;
}
</style>
