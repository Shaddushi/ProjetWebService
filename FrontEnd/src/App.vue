<script setup>
import { RouterLink, RouterView } from 'vue-router'
import Header from './components/StaticComponents/Header.vue';
import { onMounted } from 'vue';
import { ref } from 'vue';
import axios from 'axios';

const images = ref();
const display_name = ref();


onMounted(() => {
  axios.get("http://localhost:5164/ConnectSpotify/IsConnected",{withCredentials : true}).then((response) => {
            console.log(response.data)

            if(response.data == ""){
              
              ConnectUserToSpotify()

            }
            else{
              display_name.value = response.data.display_name;
              images.value = response.data.images[1].url;
            }
            }).catch((error)=>{ console.log(error)})

});


function ConnectUserToSpotify(){
        axios.get("http://localhost:5164/ConnectSpotify/GetSpotifyAuth",{withCredentials : true}).then((response) => {
            window.location.href = response.data;
            }).catch((error)=>{
              console.log(error)
            })
    }

</script>

<template>
    <Header :images="images" :display_name="display_name"></Header>
    <div class="wrapper">
      <nav>
        <RouterLink to="/">Home</RouterLink>
      </nav>
    </div>

</template>
