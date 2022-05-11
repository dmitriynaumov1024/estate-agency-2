<template>
    <div class="not-wide">
        <div class="card">
            <h2 class="text-center margin-4-after">
                {{locale.SignupView.heading}}
            </h2>
            <div>
                <input v-model="phone" type="text" 
                    :placeholder="locale.LoginView.placeholder.phone" 
                    required class="margin-after">
                <input v-model="password" type="password" 
                    :placeholder="locale.LoginView.placeholder.password"
                    required class="margin-after">
            </div>
            <div class="margin-4-after">
                <span class="error-msg">{{error}}&ensp;</span>
            </div>
            <div class="flex-strip flex-center">
                <button @click="submit" class="button button-primary button-wider">
                    {{locale.SignupView.signupButton}}
                </button>
            </div>
        </div>
    </div>
</template>

<script>
import setTitle from "../modules/set-title.js"

export default {
    inject: ["locale"],
    data () {
        return {
            phone: "",
            password: "",
            error: undefined
        }
    },
    methods: {
        submit () {
            console.log (this.phone)
            console.log (this.password)
        }
    },
    computed: {
        pageName () {
            return `${this.locale.SignupView.title} | ${this.locale.siteName}`
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