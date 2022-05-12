<template>
    <div class="header">
        <div class="width-container flex-strip pad-8px">
            <span class="site-title">{{locale.siteName}}</span>
            <span class="spacer"></span>
            <router-link to="/" class="button">
                {{locale.HomeView.link}}
            </router-link>
            <router-link to="/explore" class="button">
                {{locale.ExploreView.link}}
            </router-link>
            <router-link :to="userId ? '/account' : '/guest'" class="button">
                {{locale.AccountView.link}}
            </router-link>
            <span class="spacer"></span>
            <Listbox v-model="localeKey" as="div" class="pos-relative">
                <ListboxButton class="button flex-strip flex-i">
                    <span>{{locale.nativeName}}</span>
                    <ChevronDownIcon class="icon" />
                </ListboxButton>
                <ListboxOptions as="div" class="dropdown pad-8px">
                    <ListboxOption as="span" class="dropdown-item"
                        v-for="(val, key) in allLocales" 
                        :key="key" :value="key"
                        @click="localeSelected">
                        {{val.nativeName}}
                    </ListboxOption>
                </ListboxOptions>
            </Listbox>
        </div>
    </div>
    <div class="main">
        <div class="width-container">
            <router-view></router-view>
        </div>
    </div>
    <div class="footer">
        <div class="width-container">
            <span>
                &copy; 2022 Dmitriy Naumov |
                <a href="https://github.com/dmitriynaumov1024" target="blank">My github</a>
            </span>
        </div>
    </div>
</template>

<script>
import { computed } from "vue"
import axios from "axios"

import { Listbox, ListboxLabel, ListboxButton, ListboxOptions, ListboxOption } 
from "@headlessui/vue"

import { ChevronDownIcon } from "@heroicons/vue/solid"

import L from "./locales.js"
import S from "./storage.js"
import setTitle from "./modules/set-title.js"
import cdnResolve from "./modules/cdn-resolve.js"
import dateFormat from "./modules/date-format.js"

export default {
    components: {
        Listbox,
        ListboxLabel,
        ListboxButton,
        ListboxOptions,
        ListboxOption,
        ChevronDownIcon 
    },
    data () {
        return { 
            allLocales: L.all(),
            locale: L.locale(),
            localeKey: L.key(),
            userId: null,
        }
    },
    provide () {
        return {
            "locale": computed(()=> this.locale),
            "userId": computed(()=> this.userId),
            "cdnResolve": cdnResolve,
            "dateFormat": dateFormat,
            "logIn": this.logIn,
            "signUp": this.signUp
        }
    },
    created () {
        setTitle(this.locale.siteName)
        L.onChange(() => {
            this.locale = L.locale()
        })
    },
    updated () {
        setTitle(this.locale.siteName)
    },
    methods: {
        localeSelected () {
            L.setLocale(this.localeKey)
        },
        restoreSession () {
            axios.get ("/api/restore-session")
            .then (r => {
                if (r.data.userId) {
                    this.userId = r.data.userId
                }
            })
        },
        logIn (phone, password) {
            return  axios.post("/api/login", {
                phone: phone,
                password: password
            })
            .then (r => {
                if (r.data.userId) {
                    this.userId = r.data.userId
                    this.$router.push("/account")
                }
            })
        },
        signUp () {

        }
    }
}
</script>

<style scoped>

</style>