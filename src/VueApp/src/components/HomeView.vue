<template>
    <div class="pad-8px">
        <h2>{{locale.HomeView.heading}}</h2>
        <p>
            With Estate Agency you can: <br>
            - Search real estate objects that match your needs <br>
            - Publish your own estate objects <br>
            - Save your favorites to bookmarks <br>
            - Make orders <br>
        </p>
        <p class="margin-4-after">
            Currently, Estate Agency is in prototype state.
        </p>
        <h2>{{locale.HomeView.usefulLinks}}</h2>
        <p>
            <router-link to="/login" class="text-link">
                {{locale.LoginView.link}}
            </router-link><br>
            <router-link to="/signup" class="text-link">
                {{locale.SignupView.link}}
            </router-link><br>
        </p>
        <h2>{{locale.HomeView.technologies}}</h2>
        <p>
            <template v-for="item of usefulLinks">
                <a target="blank" :href="item.link" class="text-link">
                    {{item.name}}
                </a><br>
            </template>
        </p>
    </div>
</template>

<script>
import setTitle from "../modules/set-title.js"
import usefulLinks from "../modules/useful-links.js"

export default {
    inject: ["locale"],
    data () {
        return {
            usefulLinks: usefulLinks
        }
    },
    computed: {
        pageName () {
            return `${this.locale.HomeView.title} | ${this.locale.siteName}`
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
