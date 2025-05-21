<script setup>

import axios from 'axios';
import { onMounted, ref } from 'vue';

const Author = ref({});

const props = defineProps({
    comment: {
        type: Object,
        required: true
    }
})

onMounted(() => {
    axios.get("http://localhost:5164/GetterSpotify/GetUserProfileById?q=" + comment.AuthorId
    ,{withCredentials : true}
     ).then((response) => {
        response = JSON.parse(response.data.response)
        console.log(response)
        Author = response
    }).catch((error)=>{
          console.log(error)
        })
});


</script>



<template>

    <div class="singularCommentary">
        <div class="singularCommentaryUser">{{ Author.name }}</div>
        <div class="singularCommentaryText">{{ comment.Text }}</div>
    </div>

</template>