<template>
    <div class="image-viewer">
        <img :src="cdnResolve(sourceList[selection])">
        <button @click="slide(-1)" class="viewer-button left-button"></button>
        <button @click="slide(+1)" class="viewer-button right-button"></button>
        <span class="info">{{selection + 1}} / {{sourceList.length}}</span>
    </div>
</template>

<script>
export default {
    inject: ["cdnResolve"],
    props: ["sourceList"],
    data () {
        return {
            selection: 0
        }
    },
    methods: {
        slide (dx) {
            this.selection = (this.selection + dx) % this.sourceList.length
        }
    }
}
</script>

<style scoped>
.image-viewer {
    position: relative;
    display: block;
}

.image-viewer img {
    display: block;
    margin: auto;
    height: 320px;
    max-width: 100%;
    object-fit: cover;
    border-radius: 4px;
}

.viewer-button {
    appearance: none;
    border: 2px solid var(--color-bg-2);
    border-radius: 50%;
    display: inline-block;
    z-index: 2;
    position: absolute;
    width: 2.5rem;
    height: 2.5rem;
    transform: translateY(-50%);
    background-color: var(--color-bg-0);
    color: var(--color-1);
}

.viewer-button:hover {
    box-shadow: var(--shadow-0);
}

.left-button {
    left: 5px;
    top: 50%;
}

.left-button::before, 
.right-button::before {
    font-size: 1.4rem;
    font-weight: bold;
    position: absolute;
    left: 50%;
    top: 50%;
    transform: translate(-50%,-50%);
}

.right-button {
    right: 5px;
    top: 50%;
}

.left-button::before {
    content: "<";
}

.right-button::before {
    content: ">";
}

.info {
    position: absolute;
    z-index: 2;
    bottom: 8px;
    left: 50%;
    padding: 4px 16px;
    transform: translateX(-50%);
    background-color: var(--color-bg-0-t);
    color: var(--color-1);
}
</style>