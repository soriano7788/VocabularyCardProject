<template>
    <div class="signin-form">
        <form>
            <div class="mb-3 row">
                <label for="loginId" class="col-sm-2 col-form-label"
                    >Account</label
                >
                <div class="col-sm-5">
                    <input
                        type="text"
                        class="form-control"
                        id="loginId"
                        v-model="account"
                    />
                </div>
            </div>
            <div class="mb-3 row">
                <label for="password" class="col-sm-2 col-form-label"
                    >Password</label
                >
                <div class="col-sm-5">
                    <input
                        type="password"
                        class="form-control"
                        id="password"
                        v-model="password"
                    />
                </div>
            </div>
            <button
                @click.prevent="login"
                type="submit"
                class="btn btn-primary"
            >
                Sign in
            </button>
        </form>
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
