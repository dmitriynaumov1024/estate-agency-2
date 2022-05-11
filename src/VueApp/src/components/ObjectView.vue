<template>
    <div v-if="data" class="card">
        <ImageGallery :sourceList="data.item.photoSources" />
        <h2>{{locale.ObjectView.item}} #{{index}}</h2>
        <p><b>{{data.item.caption}}</b></p>
        <p>{{data.item.description}}</p>
        <table class="object-table margin-after">
            <tr>
                <td>Posted on</td>
                <td>{{dateFormat(data.item.postDate)}}</td>
            </tr>
            <tr>
                <td>Region</td>
                <td>{{data.location.region}}</td>
            </tr>
            <tr>
                <td>Town</td>
                <td>{{data.location.town}}</td>
            </tr>
            <tr>
                <td>District</td>
                <td>{{data.location.district}}</td>
            </tr>
            <tr>
                <td>Address</td>
                <td>{{data.item.address}}</td>
            </tr>
            <tr>
                <td>Square meters</td>
                <td>{{data.item.squareMeters}}</td>
            </tr>
            <tr>
                <td>Tags</td>
                <td class="flex-strip flex-i">
                    <span v-for="(val, key) in data.item.tags" 
                        class="tag">
                        {{val}}
                    </span>
                </td>
            </tr>
            <tr>
                <td>Price</td>
                <td><b>{{data.item.price}}</b></td>
            </tr>
        </table>
        <table class="object-table margin-after">
            <tr>
                <td>Seller</td>
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
        <div class="flex-strip">
            <span class="spacer"></span>
            <button class="button">
                {{locale.ObjectView.actionBookmark}}
            </button>
            <button class="button">
                {{locale.ObjectView.actionOrder}}
            </button>
            <button class="button">
                {{locale.ObjectView.actionReport}}
            </button>
        </div>
    </div>
    <div v-else-if="error" class="card">
        <p class="text-center">{{error}}</p>
    </div>
    <div v-else class="card">
        <p class="text-center">{{locale.loading}}</p>
    </div>
</template>

<script>
import { Menu, MenuButton, MenuItem, MenuItems } from "@headlessui/vue"
import { ChevronDownIcon } from "@heroicons/vue/solid"
import ImageGallery from "./micro/ImageGallery.vue"
import setTitle from "../modules/set-title.js"
import axios from "axios"

export default {
    inject: ["locale", "cdnResolve", "dateFormat"],
    props: ["index"],
    components: {
        Menu, MenuButton, MenuItem, MenuItems, ChevronDownIcon,
        ImageGallery
    },
    data () {
        return {
            error: false,
            data: false
        }
    },
    computed: {
        pageName () {
            return `${this.locale.ObjectView.item} #${this.index} | ${this.locale.siteName}`
        }
    },
    created () {
        axios ({
            method: "get",
            url: "/api/object", 
            params: { id: this.index }
        })
        .then ( response => {
            if (response.status == 200) {
                var rawdata = response.data
                if (typeof rawdata == "string") {
                    rawdata = JSON.parse(rawdata)
                }
                this.data = rawdata
                console.log(rawdata)
            }
            else {
                this.error = true
            }
        })
        .catch ( error => {
            if (error.response.data) {
                this.error = error.response.data.error || this.locale.unknownError
            }
            else {
                this.error = this.locale.unknownError
            }
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
    padding: 1px 6px;
}

.object-table td::after {
    content: " ";
    display: inline-block;
}

.object-table tr>td:nth-child(1) {
    display: inline-block;
    width: 40%;
}

.object-table tr>td:nth-child(2) {
    display: inline-block;
    width: 60%;
    border-bottom: 1px solid var(--color-bg-3);
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