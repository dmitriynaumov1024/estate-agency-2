<template>
    <div class="not-wide">
        <div class="card">
            <div class="margin-after">
                <img src="/img/almost-there.jpg" class="img-main-round">
            </div>
            <h2 class="text-center">{{locale.GuestView.heading}}</h2>
            <p class="margin-4-after">{{locale.GuestView.text}}</p>
            <div class="flex-strip flex-center">
                <router-link to="/login" class="button button-primary button-wider">
                    {{locale.LoginView.link}}
                </router-link>
                <router-link to="/signup" class="button button-secondary button-wider">
                    {{locale.SignupView.link}}
                </router-link>
            </div>
        </div>
    </div>
</template>

<script>
import setTitle from "../modules/set-title.js"

export default {
    inject: ["locale"],
    computed: {
        pageName () {
            return `${this.locale.GuestView.title} | ${this.locale.siteName}`
        }
    },
    mounted () {
        setTitle(this.pageName)
    },
    updated () {
        setTitle(this.pageName)
    }
}
</script>