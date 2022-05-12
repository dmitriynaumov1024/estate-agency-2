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
                <span v-if="messageKey" class="error-msg">{{error}}&ensp;</span>
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
            messageKey: undefined
        }
    },
    computed: {
        pageName () {
            return `${this.locale.LoginView.title} | ${this.locale.siteName}`
        },
        error () {
            return this.locale[this.messageKey] || 
                   `${this.locale.unknownError} [${this.messageKey}]`
        }
    },
    methods: {
        submit () {
            this.logIn (this.phone, this.password)
            .then (r => {
                console.log(r)
                this.messageKey = r.data.message
            })
            .catch (error => {
                console.log(error)
                this.messageKey = "unknownError"
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