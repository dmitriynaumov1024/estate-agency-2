<template>
    <div v-if="userId" class="card">
        <h2 class="text-center">
            {{locale.CreateObjectView.title}}
        </h2>
        <table class="profile-table margin-after">
            <tr>
                <td>{{locale.CreateObjectView.label.region}}</td>
                <td><input v-model="region" type="text" 
                    placeholder="" 
                    required></td>
            </tr>
            <tr>
                <td>{{locale.CreateObjectView.label.town}}</td>
                <td><input v-model="town" type="text" 
                    placeholder="" 
                    required></td>
            </tr>
            <tr>
                <td>{{locale.CreateObjectView.label.district}}</td>
                <td><input v-model="district" type="text" 
                    placeholder="" 
                    required></td>
            </tr>
            <tr>
                <td>{{locale.CreateObjectView.label.address}}</td>
                <td><input v-model="address" type="text" 
                    placeholder="" 
                    required></td>
            </tr>
            <tr>
                <td>{{locale.CreateObjectView.label.objectType}}</td>
                <td><input v-model="objectType" type="text" 
                    placeholder="" 
                    required></td>
            </tr>
            <tr>
                <td>{{locale.CreateObjectView.label.caption}}</td>
                <td><input v-model="caption" type="text" 
                    placeholder="" 
                    required></td>
            </tr>
            <tr>
                <td>{{locale.CreateObjectView.label.description}}</td>
                <td><textarea v-model="description"
                    placeholder="" 
                    required></textarea></td>
            </tr>
            <tr>
                <td>{{locale.CreateObjectView.label.squareMeters}}</td>
                <td><input v-model="squareMeters" type="number" 
                    placeholder="" 
                    required></td>
            </tr>
            <tr>
                <td>{{locale.CreateObjectView.label.price}}</td>
                <td><input v-model="price" type="number" 
                    placeholder="" 
                    required></td>
            </tr>
        </table>
        <div class="margin-after">
            <label for="files-input" class="button button-primary">
                {{locale.CreateObjectView.action.selectPhotos}}
            </label>
            <input type="file" name="files-input" 
                id="files-input" accept="image/*" multiple 
                ref="files-input" class="hidden"
                @change="fileInputChanged"/>
        </div>
        <div class="gallery margin-after">
            <div v-for="(source, index) in imageSources" class="img-wrap">
                <img :src="source" :key="source" @load="revoke(source)">
                <button class="delete" @click="removeImage(index)">
                    <XIcon class="icon"/>
                </button>
            </div>
        </div>
        <p v-if="messageKey" class="error-msg"></p>
        <button class="button button-primary button-big" @click="submitObject">
            {{locale.CreateObjectView.action.submit}}
        </button>
    </div>
    <div v-else class="not-wide">
        <div class="card">
            <p>{{locale.notLoggedIn}}</p>
            <div class="flex-strip flex-center">
                <router-link to="/login?next=/createobject" 
                    class="button button-primary button-wider">
                    {{locale.LoginView.link}}
                </router-link>
                <router-link to="/signup?next=/createobject" 
                    class="button button-secondary button-wider">
                    {{locale.SignupView.link}}
                </router-link>
            </div>
        </div>
    </div>
</template>

<script>
import setTitle from "../modules/set-title.js"
import axios from "axios"

import { XIcon } from "@heroicons/vue/solid" 

export default {
    inject: ["locale", "userId"],
    components: { XIcon },
    data () {
        return {
            objectType: "",
            region: "",
            town: "",
            district: "",
            address: "",
            locationId: 1,
            caption: "",
            description: "",
            tags: "",
            squareMeters: "",
            price: "",
            files: [],
            messageKey: undefined
        }
    },
    computed: {
        imageSources () {
            return this.files.map (f => URL.createObjectURL(f))
        },
        pageName () {
            return `${this.locale.CreateObjectView.title} | ${this.locale.siteName}`
        },
        error () {
            return this.locale[this.messageKey] || 
                   `${this.locale.unknownError} [${this.messageKey}]`
        }
    },
    methods: {
        fileInputChanged () {
            this.files = [...this.$refs["files-input"].files]
        },
        removeImage (index) {
            this.files.splice(index, 1)
        },
        revoke (url) {
            URL.revokeObjectURL(url)
        },
        submitObject () {
            console.log("begin submitting..")
            axios.post ("/api/objects/create", {
                locationId: 1,
                caption: this.caption,
                description: this.description,
                tags: this.tags.split(" "),
                squareMeters: this.squareMeters,
                price: this.price,
                address: this.address,
                objectType: 1,
            })
            .then (r=> {
                console.log(r.data)
                if (typeof r.data != "number") {
                    this.messageKey = "unknownError"
                    return
                }
                var f = new FormData()
                f.append("objectId", r.data)
                this.files.forEach (file => {
                    f.append("photos", file)
                })
                console.log(f)
                axios({
                    url: "/api/objects/attach-photos",
                    method: "POST",
                    data: f,
                    headers: { "Content-Type": "multipart/form-data" }
                })
                .then (r2=> {
                    console.log(r2.data)
                    if (r2.data == true) {
                        this.$router.push("/object/" + r.data)
                    }
                })
            })
            .catch (error=> {
                console.log(error)
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

<style scoped>
.gallery {
    display: flex;
    flex-direction: row;
    flex-wrap: wrap;
}

.gallery img {
    display: inline-block;
    height: 120px;
    border-radius: 4px;
    object-fit: cover;
}

.gallery .img-wrap {
    margin: 0 7px 5px 0;
    display: inline-block;
    position: relative;
}

.gallery button.delete {
    appearance: none;
    border: none;
    outline: none;
    padding: 4px 4px 1px 4px;
    display: inline-block;
    border-radius: 50%;
    position: absolute;
    top: 4px;
    right: 4px;
    background-color: var(--color-bg-0-t);
}

.gallery button.delete:hover {
    background-color: var(--color-bad);
}

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
    align-items: flex-start;
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