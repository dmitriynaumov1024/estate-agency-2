<template>
    <div class="pad-8px">
        <h2>{{locale.HomeView.heading}}</h2>
        <p>// This should be explore page, but not done yet...</p>
    </div>
</template>

<script>
import setTitle from "../modules/set-title.js"

export default {
    inject: ["locale"],
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
