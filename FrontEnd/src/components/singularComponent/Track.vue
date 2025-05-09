<script setup>
import { onMounted } from 'vue';
import { ref } from 'vue';
import {useRoute} from "vue-router";
import axios from 'axios';
const query = ref();

onMounted(() => {
    const route = useRoute();
    query.value = route.params.id;
    console.log(query.value);
    getSingularSongFromID()
});



function getSingularSongFromID(){
        axios.get("http://localhost:5164/ConnectSpotify/SearchSongsFromId?q=" + query.value
        ,{withCredentials : true}
         ).then((response) => {
            console.log(response.data)
        }).catch((error)=>{
              console.log(error)
            })
    }
    
</script>


<template>


</template>