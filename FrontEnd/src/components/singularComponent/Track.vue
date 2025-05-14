<script setup>
import { onMounted } from 'vue';
import { ref } from 'vue';
import {useRoute} from "vue-router";
import { useRouter } from 'vue-router';
import axios from 'axios';
const query = ref();
const track = ref();
const router = useRouter();

//get the ID from the URL
onMounted(() => {
    const route = useRoute();
    query.value = route.params.id;
    getSingularSongFromID()
});

//get the song from the API using the ID
function getSingularSongFromID(){
        axios.get("http://localhost:5164/GetterSpotify/SearchSongsFromId?q=" + query.value
        ,{withCredentials : true}
         ).then((response) => {
            console.log(response.data)
            track.value = response.data.result;
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
    <div id="singularContainer" v-if="track">
        <div>  
            <img :src="track.album.images[0].url" id="singularImage"/>
        </div>
    
        <div id="singularInfo" class="Font">
            <div id="singularTitle">{{ track.name }}</div>
            <button @click="changePage('/album/' + track.album.id)" id="singularAlbum">{{ track.album.name }}</button>
            
            <a id="singularSpotify" :href="`https://open.spotify.com/track/${ track.id }`" target="_blank">
                <img src="../../../assets/img/spotifyLogo.png" alt="Spotify">
            </a>
            
            <div id="singularArtistHolder" >
                <button  @click="changePage('/artist/' + track.artists[i].id)" class="singularArtist" v-for="(artist,i) in track.artists">{{artist.name }} 
                   <div v-if="i != track.artists.length - 1">,</div>
                </button>
                <div id="singularReleaseDate">· {{ track.album.release_date }}</div>
            </div>
        </div>
    </div>
    <div id="singularRecommandedContainer" v-if="track">
        <div id="singularRecommandedTitle" class="Font">Similar Songs</div>
        <div id="singularRecommandedList">
        <!-- Attendre que robin fait le back pour afficher les titres similaires -->
        </div>
    </div>

    <div id="singularCommentariesContainer" v-if="track">
        <!-- Attendre que robin fait le back pour afficher les commentaires -->
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


#singularTitle{
    font-size: 4vw;
    font-weight: bold;
    margin-left: 2vw;
    margin-top: 4.5vw;
    color: var(--Secondary-color);
}

#singularAlbum{
    background: none;
    border: none;
    font-size: 1.5vw;
    margin-left: 2vw;
    margin-top: 1vw;
    color: var(--Quinary-color);
    width: max-content;
    text-align: left;
}

#singularSpotify{
    display: flex;
    flex-direction: column;
    margin-top: 1.5%;
    width: 3vw;
    height: 3vw;
    margin-left: 2.5vw;
}
#singularArtistHolder{
    display: flex;
    flex-direction: row;
    margin-left: 2vw;
    margin-top: auto;
    margin-bottom: 1vw;
}

.singularArtist{
    background: none;
    border: none;
    display: flex;
    flex-direction: row;
    font-size: 1.5vw;
    margin-right: 5px;
    margin-top: 1vw;
    color: var(--Quinary-color);
}

#singularReleaseDate{
    font-size: 1.5vw;
    margin-right: 5px;
    margin-top: 1vw;
    color: var(--Quinary-color);
}


#singularRecommandedContainer{
    display: flex;
    flex-direction: column;
    margin-left: 5vw;
    margin-top: 4vw;

}

#singularRecommandedTitle{
    font-weight: bold;
    color: var(--Quinary-color);
    font-size: 2vw;
    text-decoration: underline;
}


.singularArtist:hover{
    transition: 0.2s ease-in-out;
    color: var(--Secondary-color);
    cursor: pointer;
}

#singularAlbum:hover{
    transition: 0.2s ease-in-out;
    color: var(--Secondary-color);
    cursor: pointer;
}

</style>