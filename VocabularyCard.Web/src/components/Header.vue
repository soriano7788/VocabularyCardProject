<template>
  <nav class="navbar navbar-expand-lg navbar-light bg-light">
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
            <a class="nav-link active" aria-current="page" href="#">Home</a>
          </router-link>
          <router-link
            to="/CardSets"
            class="nav-item"
            activeClass="active"
            tag="li"
            v-if="isAuth"
          >
            <a class="nav-link active" aria-current="page" href="#">CardSets</a>
          </router-link>
          <router-link
            to="/Register"
            class="nav-item"
            activeClass="active"
            tag="li"
            v-if="!isAuth"
          >
            <a class="nav-link active" aria-current="page" href="#">Register</a>
          </router-link>
          <router-link
            to="/SignIn"
            class="nav-item"
            activeClass="active"
            tag="li"
            v-if="!isAuth"
          >
            <a class="nav-link active" aria-current="page" href="#">Sign in</a>
          </router-link>
          <li class="nav-item" v-if="isAuth">
            <a
              @click.prevent="signOut"
              class="nav-link active"
              aria-current="page"
              href="#"
              >Sign out</a
            >
          </li>
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
