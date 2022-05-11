<template>
    <div class="not-wide">
        <div class="card">
            <h2 class="text-center margin-4-after">
                {{locale.LoginView.heading}}
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
                    {{locale.LoginView.loginButton}}
                </button>
            </div>
        </div>
    </div>
</template>

<script>
import setTitle from "../modules/set-title.js"

export default {
    inject: ["locale", "logIn"],
    data () {
        return {
            phone: "",
            password: "",
            error: undefined,
            errorTarget: undefined
        }
    },
    computed: {
        pageName () {
            return `${this.locale.LoginView.title} | ${this.locale.siteName}`
        }
    },
    methods: {
        submit () {
            this.logIn (this.phone, this.password)
            .then (response => {
                if (response.data.error) {
                    this.error = response.data.error
                    this.errorTarget = response.data.errorTarget
                }
                else {
                    this.error = undefined
                }
            })
            .catch (error => {
                if (error.response.data) {
                    this.error = error.response.data.error || this.locale.unknownError
                }
                else {
                    this.error = this.locale.unknownError
                }
            })
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