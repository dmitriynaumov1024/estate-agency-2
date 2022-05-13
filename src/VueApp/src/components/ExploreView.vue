<template>
    <div v-if="objects">
        <h2 class="text-center">{{locale.ExploreView.title}}</h2>
        <MiniObjectView v-for="obj of objects" v-bind="obj" />
    </div>
    <div v-else-if="messageKey" class="card">
        <p class="text-center error-msg">{{error}}</p>
    </div>
    <div v-else class="card">
        <p class="text-center">{{locale.loading}}</p>
    </div>
</template>

<script>
import axios from "axios"
import setTitle from "../modules/set-title.js"

import MiniObjectView from "./micro/MiniObjectView.vue"

export default {
    inject: ["locale"],
    components: {
        MiniObjectView
    },
    data () {
        return {
            objects: undefined,
            messageKey: undefined
        }
    },
    computed: {
        pageName () {
            return `${this.locale.HomeView.title} | ${this.locale.siteName}`
        },
        error () {
            return this.locale[this.messageKey] || 
                   `${this.locale.unknownError} [${this.messageKey}]`
        }
    },
    mounted () {
        setTitle(this.pageName)
        this.loadObjects()
    },
    updated () {
        setTitle(this.pageName)
    },
    methods: {
        loadObjects () {
            axios.get ("/api/objects/top")
            .then (r => {
                if (r.data.message) {
                    this.messageKey = r.data.message
                } 
                else {
                    this.objects = r.data
                    this.messageKey = undefined
                }
            })
            .catch(r => {
                this.messageKey = "unknownError"
            })
        }
    }
}
</script>
