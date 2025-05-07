
<script setup>
import { onMounted } from 'vue';


import { ref } from 'vue';
import axios from 'axios';

const images = ref("../assets/img/defaultpic.png");

const props = defineProps({
        profileData: {
            Object
        },
    })

onMounted(() => {
    if(props.profileData.images.length > 0){
        images.value = props.profileData.images[0].url;
    }
});

</script>

<template>
    <div id="profileView">
        <div id="profileHeader">
            <img :src="images" id="profileImage"/>
            <div id="profileName">Profil </div>
        </div>
    </div>


</template>


<style scoped>

:root {
    --Profile-color: #535353;
}

#profileView{
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    background: var(--Profile-color);

}

#profileHeader{
    display: flex;
    padding: 20px; 
    font-family: 'Font', sans-serif;
    width: 100%;
}

#profileImage{
            width: 15%;
            height: 15%;
            border-radius: 500px;
            margin: 2%;
            border: 2px solid var(--Secondary-color);
        }


#profileName{
    font-size: 3em;
    margin-top: 2%;
    color: var(--Primary-color);
    font-family: 'Font', sans-serif;
}






</style>