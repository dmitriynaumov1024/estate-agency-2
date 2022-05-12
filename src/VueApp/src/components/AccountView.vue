<template>
    <div v-if="userId && account" class="card">
        <h3>{{locale.AccountView.accountInfo}}</h3>
        <table class="profile-table margin-after">
            <tr>
                <td>{{locale.AccountView.label.surname}}</td>
                <td>{{account.surname}}</td>
            </tr>
            <tr>
                <td>{{locale.AccountView.label.name}}</td>
                <td>{{account.name}}</td>
            </tr>
            <tr>
                <td>{{locale.AccountView.label.phone}}</td>
                <td>{{account.phone}}</td>
            </tr>
            <tr>
                <td>{{locale.AccountView.label.email}}</td>
                <td>{{account.email}}</td>
            </tr>
            <tr>
                <td>{{locale.AccountView.label.region}}</td>
                <td>{{location.region}}</td>
            </tr>
            <tr>
                <td>{{locale.AccountView.label.town}}</td>
                <td>{{location.town}}</td>
            </tr>
            <tr v-if="location.district">
                <td>{{locale.AccountView.label.district}}</td>
                <td>{{location.district}}</td>
            </tr>
            <tr>
                <td>{{locale.AccountView.label.regDate}}</td>
                <td>{{dateFormat(regDate)}}</td>
            </tr>
        </table>
        <h3>{{locale.AccountView.accountAdvanced}}</h3>
        <p><router-link to="/my-objects" class="text-link">
            {{locale.AccountView.action.objects}}
        </router-link></p>
        <p><router-link to="/my-bookmarks" class="text-link">
            {{locale.AccountView.action.bookmarks}}
        </router-link></p>
        <p><router-link to="/my-orders" class="text-link">
            {{locale.AccountView.action.orders}}
        </router-link></p>
        <p><router-link to="/my-wishes" class="text-link">
            {{locale.AccountView.action.wishes}}
        </router-link></p>
        <p><router-link to="/my-suggestions" class="text-link">
            {{locale.AccountView.action.suggestions}}
        </router-link></p>
    </div>
    <div v-else class="not-wide">
        <div class="card">
            <p>{{error}}</p>
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
import axios from "axios"
import setTitle from "../modules/set-title.js"

export default {
    inject: ["locale", "userId", "dateFormat"],
    data () {
        return {
            account: undefined,
            location: undefined,
            regDate: undefined,
            messageKey: undefined
        }
    },
    computed: {
        pageName () {
            return `${this.locale.AccountView.title} | ${this.locale.siteName}`
        },
        error () {
            return this.locale[this.messageKey] || 
                   `${this.locale.unknownError} [${this.messageKey}]`
        }
    },
    created () {
        axios.get("/api/account")
        .then(r => {
            if (r.data.person) {
                this.account = r.data.person
                this.location = r.data.location
                this.regDate = r.data.regDate
            }
            else {
                this.messageKey = r.data.message
            }
        })
        .catch(error => {
            this.messageKey = "unknownError"
        })
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
    max-width: 38rem;
}

.profile-table tbody {
    display: block;
}

.profile-table tr {
    display: flex;
    flex-direction: row;
    align-items: flex-start;
}

.profile-table td {
    display: none;
    margin-bottom: 6px;
}

.profile-table tr>td:nth-child(1) {
    display: inline-block;
    width: 50%;
    padding: 1px 0px;
}

.profile-table tr>td:nth-child(2) {
    display: inline-block;
    width: 50%;
    border-bottom: 1px solid var(--color-bg-3);
    padding: 1px 6px;
}
</style>