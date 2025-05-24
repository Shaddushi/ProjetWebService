<script setup>
import { RouterLink, RouterView } from 'vue-router'
import Header from './components/StaticComponents/Header.vue';
import { onMounted } from 'vue';
import { ref } from 'vue';
import axios from 'axios';

const images = ref("../assets/img/defaultpic.png");
const display_name = ref();
const profileData = ref({});

onMounted(() => {
  axios.get("http://localhost:5164/ConnectSpotify/IsConnected",{withCredentials : true}).then((response) => {
            console.log(response.data)

            if(response.data == ""){
              ConnectUserToSpotify()
            }
            else{
              profileData.value = response.data;
              display_name.value = response.data.display_name;
              localStorage.setItem("user_img", "../assets/img/defaultpic.png");
              if(response.data.images.length > 0){
                images.value = response.data.images[0].url;
                localStorage.setItem("user_img", profileData.value.images[0].url);
              }
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
      <router-view v-slot="{ Component, route }">

        <!-- Pass props to the component if it needs it -->
        <component 
          :is="Component" 
          v-if="route.path === '/profile'" 
          :profileData="profileData" 
        />
        <component 
          :is="Component" 
          v-else 
        />
      </router-view>
    </div>

</template>
