<template>
    <div v-if="data" class="card">
        <div class="margin-after">
            <ImageGallery :sourceList="data.item.photoSources" />
        </div>
        <p><b>{{data.item.caption}}</b></p>
        <p>{{data.item.description}}</p>
        <table class="object-table margin-after">
            <tr>
                <td>{{locale.ObjectView.label.postDate}}</td>
                <td>{{dateFormat(data.item.postDate)}}</td>
            </tr>
            <tr>
                <td>{{locale.ObjectView.label.region}}</td>
                <td>{{data.location.region}}</td>
            </tr>
            <tr>
                <td>{{locale.ObjectView.label.town}}</td>
                <td>{{data.location.town}}</td>
            </tr>
            <tr v-if="data.location.district">
                <td>{{locale.ObjectView.label.district}}</td>
                <td>{{data.location.district}}</td>
            </tr>
            <tr>
                <td>{{locale.ObjectView.label.address}}</td>
                <td>{{data.item.address}}</td>
            </tr>
            <tr>
                <td>{{locale.ObjectView.label.squareMeters}}</td>
                <td>{{data.item.squareMeters}}</td>
            </tr>
            <tr>
                <td>{{locale.ObjectView.label.tags}}</td>
                <td class="flex-strip flex-i">
                    <span v-for="(val, key) in data.item.tags" 
                        class="tag">
                        {{val}}
                    </span>
                </td>
            </tr>
            <tr>
                <td>{{locale.ObjectView.label.price}}</td>
                <td><b>{{data.item.price}}</b></td>
            </tr>
        </table>
        <table class="object-table margin-after">
            <tr>
                <td>{{locale.ObjectView.label.seller}}</td>
                <td>{{data.seller.name}} {{data.seller.surname}}</td>
            </tr>
            <tr>
                <td></td>
                <td>{{data.seller.phone}}</td>
            </tr>
            <tr>
                <td></td>
                <td>{{data.seller.email}}</td>
            </tr>
        </table>
        <div class="flex-strip" v-if="userId">
            <span class="spacer"></span>
            <button class="button">
                {{locale.ObjectView.action.bookmark}}
            </button>
            <button class="button">
                {{locale.ObjectView.action.order}}
            </button>
            <button class="button">
                {{locale.ObjectView.action.report}}
            </button>
        </div>
    </div>
    <div v-else-if="messageKey" class="card">
        <p class="text-center">{{error}}</p>
    </div>
    <div v-else class="card">
        <p class="text-center">{{locale.loading}}</p>
    </div>
</template>

<script>
import ImageGallery from "./micro/ImageGallery.vue"
import setTitle from "../modules/set-title.js"
import axios from "axios"

export default {
    inject: ["locale", "dateFormat", "userId"],
    props: ["index"],
    components: {
        ImageGallery
    },
    data () {
        return {
            data: undefined,
            messageKey: undefined
        }
    },
    computed: {
        pageName () {
            return `${this.locale.ObjectView.item} #${this.index} | ${this.locale.siteName}`
        },
        error () {
            return this.locale[this.messageKey] || 
                   `${this.locale.unknownError} [${this.messageKey}]`
        }
    },
    created () {
        axios ({
            method: "get",
            url: "/api/object", 
            params: { id: this.index }
        })
        .then (r => {
            if (r.data.message) {
                this.messageKey = r.data.message
            }
            else {
                this.data = r.data
            }
        })
        .catch ( error => {
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
.object-table {
    display: block;
}

.object-table tbody {
    display: block;
}

.object-table tr {
    display: flex;
    flex-direction: row;
    align-items: flex-start;
}

.object-table td {
    display: none;
    margin-bottom: 6px;
}

.object-table td::after {
    content: " ";
    display: inline-block;
}

.object-table tr>td:nth-child(1) {
    display: inline-block;
    width: 40%;
    padding: 1px 0px;
}

.object-table tr>td:nth-child(2) {
    display: inline-block;
    width: 60%;
    border-bottom: 1px solid var(--color-bg-3);
    padding: 1px 4px;
}

.tag {
    display: inline-block;
    padding: 0px 8px 1px;
    border-radius: 0.6rem;
    background-color: var(--color-accent-1);
    color: var(--color-bg-0);
    font-size: 0.9rem;
}

.image-gallery img {
    display: inline-block;
    height: 320px;
    width: 480px;
    object-fit: cover;
}
</style>