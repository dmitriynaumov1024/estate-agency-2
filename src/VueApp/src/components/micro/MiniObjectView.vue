<template>
    <div class="card margin-after">
        <div class="mini-object-card">
            <img :src="cdnResolve(photoSource)" class="card-img">
            <div class="flex-grow">
                <p>
                    {{address}}<br>
                    {{dateFormat(postDate)}}<br>
                    {{description}}<br>
                    {{locale.ObjectView.label.price}}: <b>{{price}}</b>
                </p>
                <p><router-link :to="`/object/${index}`" class="text-link">
                    {{locale.seeMore}} &gt;
                </router-link></p>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    inject: ["locale", "dateFormat", "cdnResolve"],
    props: [
        "index",
        "photoSource",
        "address",
        "postDate",
        "description",
        "price"
    ]
}
</script>

<style scoped>
.mini-object-card {
    display: flex;
    align-items: flex-start;
}

.card-img {
    height: 10rem;
    width: 13rem;
    object-fit: cover;
    margin-right: 1rem;
    border-radius: 4px;
}
</style>