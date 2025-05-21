<script setup>
import { onMounted } from 'vue';
import { ref } from 'vue';
import {useRoute} from "vue-router";
import { useRouter } from 'vue-router';
import axios from 'axios';
const query = ref();
const album = ref();
const router = useRouter();

//get the ID from the URL
onMounted(() => {
    const route = useRoute();    
    query.value = route.params.id;
    getSingularAlbumFromID()
});

//get the song from the API using the ID
function getSingularAlbumFromID(){
        axios.get("http://localhost:5164/GetterSpotify/SearchAlbumsFromId?q=" + query.value
        ,{withCredentials : true}
         ).then((response) => {
            response = JSON.parse(response.data.response)
            album.value = response;
            console.log(response)
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
    <div id="singularContainer" v-if="album">
        <div>  
            <img :src="album.images[0].url" id="singularImage"/>
        </div>
    
        <div id="singularInfo" class="Font">
            <div id="singularType"> {{album.album_type.substring(0,1).toUpperCase()+ album.album_type.substring(1) }}</div>
            <div  id="singularTitle">{{ album.name }}</div>
            
            <a id="singularSpotify" :href="`https://open.spotify.com/album/${ album.id }`" target="_blank">
                    <img src="../../../assets/img/spotifyLogo.png" alt="Spotify">
            </a>

            <div id="singularArtistHolder" >
                <button  @click="changePage('/artist/' + album.artists[i].id)" class="singularArtist" v-for="(artist,i) in album.artists">{{artist.name }} 
                   <div v-if="i != album.artists.length - 1">,</div>
                </button>
                <div id="singularReleaseDate">· {{ album.release_date }}</div>
            </div>
        </div>
    </div>

    <div id="singularTrackHolder">
        <div id="singularTrackTitle">Tracks</div>
        <div id="singularTrackList">
            <div v-for="(track,i) in album.tracks.items" class="singularTrack" @click="changePage('/track/' + track.id)">
                <div class="singularTrackName">{{ track.name }}</div>
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

#singularType{
    font-size: 2vw;
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