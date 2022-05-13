<template>
    <div class="card">
        <h2 class="text-center margin-4-after">
            {{locale.SignupView.heading}}
        </h2>
        <table class="profile-table">
            <tr>
                <td>{{locale.SignupView.label.name}}</td>
                <td><input v-model="name" type="text" 
                    :placeholder="locale.SignupView.placeholder.name" 
                    required></td>
            </tr>
            <tr>
                <td>{{locale.SignupView.label.surname}}</td>
                <td><input v-model="surname" type="text" 
                    :placeholder="locale.SignupView.placeholder.surname" 
                    required></td>
            </tr>
            <tr>
                <td>{{locale.SignupView.label.phone}}</td>
                <td><input v-model="phone" type="text" 
                    :placeholder="locale.SignupView.placeholder.phone" 
                    required></td>
            </tr>
            <tr>
                <td>{{locale.SignupView.label.email}}</td>
                <td><input v-model="email" type="text" 
                    :placeholder="locale.SignupView.placeholder.email" 
                    required></td>
            </tr>
            <tr>
                <td>{{locale.SignupView.label.password}}</td>
                <td><input v-model="password" type="password" 
                    :placeholder="locale.SignupView.placeholder.password"
                    required></td>
            </tr>
            <tr>
                <td>{{locale.SignupView.label.passwordRepeat}}</td>
                <td><input v-model="password2" type="password" 
                    :placeholder="locale.SignupView.placeholder.password"
                    required></td>
            </tr>
        </table>
        <div class="margin-4-after">
            <span v-if="messageKey" class="error-msg">{{error}}</span>&ensp;
        </div>
        <div class="flex-strip flex-center">
            <button @click="submit" :disabled="!valid()" 
                class="button button-primary button-wider">
                {{locale.SignupView.signupButton}}
            </button>
        </div>
    </div>
</template>

<script>
import setTitle from "../modules/set-title.js"

export default {
    inject: ["locale", "signUp"],
    props: ["next"],
    data () {
        return {
            phone: "",
            password: "",
            password2: "",
            email: "",
            name: "",
            surname: "",
            messageKey: undefined
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
        },
        error () {
            return this.locale[this.messageKey] || 
                   `${this.locale.unknownError} [${this.messageKey}]`
        }
    },
    methods: {
        passwordValid () {
            return this.password.length > 8
        },
        passwordMatch () {
            return this.password == this.password2
        },
        phoneValid () {
            return /^[0-9]{10}$/.test(this.phone) 
        },
        nameValid () {
            return this.name.length > 1
        },
        surnameValid () {
            return this.surname.length > 2
        },
        emailValid () {
            return /^[a-zA-Z0-9._-]+@[a-zA-Z0-9][a-zA-Z0-9.-]{0,61}[a-zA-Z0-9]\.[a-zA-Z.]{2,6}$/.test(this.email)
        },
        valid () {
            return this.passwordValid() && 
                   this.passwordMatch() && 
                   this.phoneValid() &&
                   this.nameValid() &&
                   this.surnameValid() &&
                   this.emailValid()
        },
        submit () {
            this.signUp({
                name: this.name,
                surname: this.surname,
                locationId: 1,
                phone: this.phone,
                password: this.password,
                email: this.email
            }, this.next)
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

<style scoped>
.profile-table {
    display: block;
    margin: auto;
}

.profile-table tbody {
    display: block;
}

.profile-table tr {
    display: flex;
    flex-direction: row;
    align-items: flex-end;
}

.profile-table td {
    display: none;
    margin-bottom: 6px;
}

.profile-table tr>td:nth-child(1) {
    display: inline-block;
    width: 50%;
}

.profile-table tr>td:nth-child(2) {
    display: inline-block;
    width: 50%;
}
</style>