import Vue from 'vue'
import VueRouter from 'vue-router'

import Register from "./components/Register.vue";
import Signin from "./components/Signin.vue";
import Home from "./components/Home.vue";
import CardSets from "./components/CardSet/CardSets.vue";
import Cards from "./components/Card/Cards.vue";

Vue.use(VueRouter);

export const routes = [
    { path: "/", component: Home },
    {
        path: "/CardSets",
        component: CardSets,
        children: [
            { path: ":id/Cards", component: Cards },
            // { path: ":id/edit", component: CardSetEdit },
        ]
    },
    { path: "/Register", component: Register },
    { path: "/Signin", component: Signin }
];

export default new VueRouter({
    mode: "history",
    base: "/vocabulary/",
    routes: routes
});