<script setup>
import { onMounted } from 'vue';
import { ref } from 'vue';
import {useRoute} from "vue-router";
import { useRouter } from 'vue-router';
import axios from 'axios';
import numbro from 'numbro';

const query = ref();
const artist = ref();
const router = useRouter();

//get the ID from the URL
onMounted(() => {
    const route = useRoute();    
    query.value = route.params.id;
    getSingularArtistFromID()
});

//get the song from the API using the ID
function getSingularArtistFromID(){
        axios.get("http://localhost:5164/GetterSpotify/SearchArtistsFromId?q=" + query.value
        ,{withCredentials : true}
         ).then((response) => {
            response = JSON.parse(response.data.response)
            artist.value = response
        }).catch((error)=>{
              console.log(error)
            })
    }

// Function to change the page
const changePage = (path) => {
    router.push(path);
};
    
</script>

<template>

    <div id="singularContainer" v-if="artist">
        <img v-if="artist.images.length > 0" :src="artist.images[0].url" id="singularImage"/>
        <img v-else src="../../../assets/img/defaultpic.png" id="singularImage"/>
        <div id="singularInfo">
            
            <div id="singularHeaderContent">
                <div id="singularTitle">   {{ artist.name }} </div>
                <div id="singularFollowers">Followers: {{ numbro(artist.followers.total).format({ average: true,mantissa: 1 }) }}</div>
                <div id="singularGenre" v-if="artist.genres.length > 0" >
                    genre: 
                    <div v-for="(genre,i) in artist.genres" class="singularGenrealone">
                        {{ genre }}<span v-if="i != artist.genres.length - 1">,</span>
                    </div>
                </div>
                
                <a id="singularSpotify" class="singularArtistContent" :href="`https://open.spotify.com/artist/${ artist.id }`" target="_blank">
                    <img src="../../../assets/img/spotifyLogo.png" alt="Spotify">
                </a>
            </div>
        </div>
    </div>



</template>



<style scoped>

#singularContainer{
    display: flex;
    flex-direction: row;
    margin-left: 3vw;
    margin-top: 3vw;
}

#singularImage{
    width: 25vw;
    height: 25vw;
    border-radius: 10px;

}

#singularInfo{
    display: flex;
    flex-direction: column;
 }

#singularFollowers{
    font-size: 1.5vw;
    font-weight: bold;
    margin-left: 2vw;
    margin-top: 1vw;
    color: var(--Quinary-color);
}


#singularTitle{
    background: none;
    border: none;
    font-size: 4vw;
    font-weight: bold;
    margin-left: 2vw;
    margin-top: 2.5vw;
    color: var(--Secondary-color);
}


#singularSpotify{
    display: flex;
    flex-direction: column;
    margin-top: 1vw;
    width: 3vw;
    height: 3vw;
    margin-left: 2.5vw;
}


#singularGenre{
    display: flex;
    flex-direction: row;
    margin-left: 2vw;
    margin-top: 1vw;
    color: var(--Quinary-color);
    font-size: 1.5vw;
    font-weight: bold;
}

.singularGenrealone{
    display: flex;
    align-items: center;
    font-size: 1.5vw;
    margin-left: 0.5vw;
    font-weight: bold;
    color: var(--Quinary-color);
}


</style>