import * as Vue from "vue"
import * as VueRouter from "vue-router"
import axios from "axios"

import App from "./App.vue"
import HomeView from "./components/HomeView.vue"
import ExploreView from "./components/ExploreView.vue"
import ObjectView from "./components/ObjectView.vue"
import AccountView from "./components/AccountView.vue"
import GuestView from "./components/GuestView.vue"
import LoginView from "./components/LoginView.vue"
import SignupView from "./components/SignupView.vue"
import CreateObjectView from "./components/CreateObjectView.vue"
import MyObjectsView from "./components/MyObjectsView.vue"

import "./css/style.css"

const myRouter = VueRouter.createRouter({
    history: VueRouter.createWebHistory(),
    routes: [
        { path: "/", component: HomeView },
        { path: "/explore", component: ExploreView },
        { path: "/object/:index", name: "object", component: ObjectView, props: true },
        { path: "/account", component: AccountView },
        { path: "/guest", component: GuestView },
        { path: "/login", component: LoginView, 
          props: route => ({ next: route.query.next }) },
        { path: "/signup", component: SignupView, 
          props: route => ({ next: route.query.next }) },
        { path: "/createobject", component: CreateObjectView },
        { path: "/my-objects", component: MyObjectsView }
    ]
})

window.axios = axios

var app = Vue.createApp(App)
app.config.unwrapInjectedRef = true
app.use(myRouter)
app.mount('#app')
