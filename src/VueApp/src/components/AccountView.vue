<template>
    <div v-if="userId" class="card">
        <h3>{{locale.AccountView.accountInfo}}</h3>
        <table class="profile-table margin-after">
            <tr>
                <td>{{locale.AccountView.surnameLabel}}</td>
                <td>{{account.surname}}</td>
            </tr>
            <tr>
                <td>{{locale.AccountView.nameLabel}}</td>
                <td>{{account.name}}</td>
            </tr>
            <tr>
                <td>{{locale.AccountView.phoneLabel}}</td>
                <td>{{account.phone}}</td>
            </tr>
            <tr>
                <td>{{locale.AccountView.emailLabel}}</td>
                <td>{{account.email}}</td>
            </tr>
            <tr>
                <td>{{locale.AccountView.regDateLabel}}</td>
                <td>{{account.regDate}}</td>
            </tr>
        </table>
        <h3>{{locale.AccountView.accountAdvanced}}</h3>
        <p>{{locale.comingSoon}}</p>
    </div>
    <div v-else>
        <p class="text-center">{{locale.unknownError}}</p>
    </div>
</template>

<script>
import setTitle from "../modules/set-title.js"

export default {
    inject: ["locale", "userId"],
    computed: {
        pageName () {
            return `${this.locale.AccountView.title} | ${this.locale.siteName}`
        },
        account () {
            return {
                surname: "Brown",
                name: "Lavanda",
                regDate: "2022-05-01",
                phone: "329-69-2499",
                email: "lavbn19@kmns.co"
            }
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
    padding: 1px 6px;
}

.profile-table tr>td:nth-child(1) {
    display: inline-block;
    width: 50%;
}

.profile-table tr>td:nth-child(2) {
    display: inline-block;
    width: 50%;
    border-bottom: 1px solid var(--color-bg-3);
}
</style>