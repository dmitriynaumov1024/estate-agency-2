<template>
    <div class="card">
        <div class="margin-after">
            <form>
                <label for="files-input" class="button button-primary">
                    Select files
                </label>
                <input type="file" name="files-input" 
                    id="files-input" accept="image/*" multiple 
                    ref="files-input" class="hidden"
                    @change="fileInputChanged"/>
            </form>
        </div>
        <div class="gallery margin-after">
            <div v-for="(source, index) in imageSources" class="img-wrap">
                <img :src="source" :key="source" @load="revoke(source)">
                <button class="delete" @click="removeImage(index)">
                    <XIcon class="icon"/>
                </button>
            </div>
        </div>
        <button class="button button-primary" @click="inspect">inspect</button>
    </div>
</template>

<script>
import { XIcon } from "@heroicons/vue/solid" 

export default {
    components: { XIcon },
    data () {
        return {
            files: []
        }
    },
    computed: {
        imageSources () {
            return this.files.map (f => URL.createObjectURL(f))
        },
        pageName () {
            return `${this.locale.CreateObjectView.title} | ${this.locale.siteName}`
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
    background-color: var(--color-bg-1);
    padding: 5px;
    border-radius: 5px;
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
</style>