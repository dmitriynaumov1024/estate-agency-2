<template>
    <div v-if="userId" class="card">
        <p>// Here will be my objects.</p>
    </div>
    <div v-else class="card">
        <p class="text-center">{{error}}</p>
    </div>
</template>

<script>
import setTitle from "../modules/set-title.js"
import axios from "axios"

export default {
    inject: ["locale", "userId", "dateFormat"],
    data () {
        return {
            messageKey: undefined
        }
    },
    computed: {
        pageName () {
            return `${this.locale.MyObjectsView.title} | ${this.locale.siteName}`
        },
        error () {
            return this.locale[this.messageKey] || 
                `${this.locale.unknownError} [${this.messageKey}]`
        }
    },
    methods: {
        load () { }
    },
    mounted () {
        // this.load()
    }
}
</script>