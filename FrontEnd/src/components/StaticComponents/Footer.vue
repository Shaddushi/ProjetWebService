<script setup>
    import { ref } from 'vue';
    const props = defineProps({
        display_name: {
            String
        },
        images: {
            String
        },
        
    })


</script>


<template>
    <img :src="props.images">
    <div> {{ display_name }} </div>
</template>