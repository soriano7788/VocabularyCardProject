<template>
  <div class="signin-form container">
    <div class="row justify-content-center">
      <form
        class="mt-5 pt-3 pb-3 col-md-5 col-sm-10 shadow bg-gradient text-secondary rounded bg-main text-light"
      >
        <div class="mb-3 row justify-content-center">
          <label for="loginId" class="col-md-4 col-form-label">
            {{ $t("AUTH.ACCOUNT") }}
          </label>
          <div class="col-md-6">
            <input
              type="text"
              class="form-control"
              id="loginId"
              v-model="account"
            />
          </div>
        </div>
        <div class="mb-3 row justify-content-center">
          <label for="password" class="col-md-4 col-form-label">{{
            $t("AUTH.PASSWORD")
          }}</label>
          <div class="col-md-6">
            <input
              type="password"
              class="form-control"
              id="password"
              v-model="password"
            />
          </div>
        </div>
        <button @click.prevent="login" type="submit" class="btn btn-secondary">
          {{ $t("AUTH.SIGN_IN") }}
        </button>
      </form>
    </div>
  </div>
</template>

<script>
export default {
  data: function() {
    return {
      account: "",
      password: "",
    };
  },
  methods: {
    login: function() {
      // call login action
      this.$store.dispatch("login", {
        LoginId: this.account,
        Password: this.password,
      });
    },
  },
};
</script>
