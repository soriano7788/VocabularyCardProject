import Vue from 'vue'
import VueRouter from 'vue-router'

import Register from "./components/Register.vue";
import Signin from "./components/Signin.vue";
import Home from "./components/Home.vue";
import CardSetDashboard from "./components/CardSet/CardSetDashboard.vue";
import CardSets from "./components/CardSet/CardSets.vue";
import Cards from "./components/Card/Cards.vue";

Vue.use(VueRouter);

export const routes = [
    {
        path: "/", component: Home, beforeEnter: (to, from, next) => {
            next();
        }
    },
    {
        path: "/CardSets",
        component: CardSetDashboard,
        children: [
            { path: "", component: CardSets },
            {
                path: ":cardSetId/Cards", component: Cards, name: "CardSetCards", beforeEnter: (to, from, next) => {
                    next();
                }
            },
            // { path: ":id/edit", component: CardSetEdit },
        ]
    },
    { path: "/Register", component: Register },
    {
        path: "/Signin", component: Signin, beforeEnter: (to, from, next) => {
            // 假如目前是已通過驗證的狀態，那不應該還能到登入頁，除非已登出
            next();
        }
    }
];

export default new VueRouter({
    mode: "history",
    base: "/vocabulary/",
    routes: routes
});