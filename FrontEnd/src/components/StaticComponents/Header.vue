<script setup>
import { useRouter } from 'vue-router';
import { ref } from 'vue';

import { createRouter, createWebHistory } from 'vue-router';




const tracksActivated = ref(false);
const albumsActivated = ref(false);
const artistsActivated = ref(false);


const router = useRouter();


// J'ai fait mieux en code on va pas se mentir 
// Mais bon j'ai pas trouver de solution pour 
// l'instant... 
function changeCssButtons(path){
    if(path == "/tracks"){
        tracksActivated.value = true;
        albumsActivated.value = false;
        artistsActivated.value = false;
    }
    else if(path == "/albums"){
        tracksActivated.value = false;
        albumsActivated.value = true;
        artistsActivated.value = false;
    }
    else if(path == "/artists"){
        tracksActivated.value = false;
        albumsActivated.value = false;
        artistsActivated.value = true;
    }
    else if(path == "/profile"){
        tracksActivated.value = false;
        albumsActivated.value = false;
        artistsActivated.value = false;      
    }
}


const changePage = (path) => {
    changeCssButtons(path);
    router.push(path);
};


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
    
    <div id="Header">  
        <nav id="nav">
        <button @click="changePage('/tracks')" :class="{OnPage: tracksActivated}">Tracks</button>
        <button @click="changePage('/albums')" :class="{OnPage: albumsActivated}">Albums</button>
        <button @click="changePage('/artists')" :class="{OnPage: artistsActivated}">Artists</button>
        </nav>
        <button @click="changePage('/profile')" id="Profile">
                <div id="Header_Name" class="Font">  {{ display_name }} </div >
                <img :src="props.images" id="Header_Img">
        </button>
    </div>
    
</template>


<style>

#Header{
    display: flex; 
    background-color: var(--Primary-color);
    padding: 20px; 
    font-family: 'Font', sans-serif;
    width: 100%;
}

    #nav{
        width: 50%;
        display: flex; 
        flex-direction: row;
    }

        #nav button{
            background: none;
            color: var(--Secondary-color);
            border: none;
            font-size: large;
            margin-left: 40px;
            cursor: pointer;
            align-self: center;
            font-family: 'Font', sans-serif;
            position: relative;
            transition: color 0.3s;
        }

        #nav button::before{
            content: "";
            position: absolute;
            width: 0;
            bottom: 0;
            left: 0;
            height: 2px;
            background-color: var(--Secondary-color); 
            transition: width 0.3s;
        }

        

        #nav button:hover::before{
            width: 100%;
        }

        .OnPage::after{
            content: "";
            position: absolute;
            width: 100%;
            bottom: 0;
            left: 0;
            height: 2px;
            background-color: var(--Secondary-color); 
            transition: width 0.3s;
        }



    #Profile{
        display: flex; 
        flex-direction: row; 
        margin-left: auto;
        margin-right: 3%;
        background: none;
        border: none;
        font-family: 'Font', sans-serif;
        font-size:medium;
    }

        #Header_Name{
            color: var(--Secondary-color); 
            font-size:150%; 
            display: flex; 
            align-self: center;
            margin-bottom: 5px;
        }

        #Header_Img{
            border-radius: 500px;
            margin-left: 20px;
            margin-right: 20px;
        }




</style>