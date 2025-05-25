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
        <div id="singularImageHolder">  
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

    <div id="singularTracksHolder" v-if="album">
        <div id="singularTracksTitle">Tracks</div>
            <div id="singularTrackList">
                <div v-for="track in album.tracks.items" class="singularTrack" @click="changePage('/track/' + track.id)">
                    <div class="TrackItem">
                        <img v-if="album.images.length > 0" :src="album.images[0].url" id="singularTrackImage"/>
                        <img v-else src="../../../assets/img/defaultAlbum.png" id="singularTrackImage"/>
                </div>
                <div class="singularTrackTitle">{{ track.name }}</div>
                <div class="singularTrackDuration">{{ Math.floor(track.duration_ms / 60000) }}:{{ Math.floor((track.duration_ms % 60000) / 1000).toString().padStart(2, '0') }}</div>
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
    width: 60vw;
    font-size: 4vw;
    font-weight: bold;
    margin-left: 2vw;
    margin-top: 2.5vw;
    color: var(--Secondary-color);
    overflow: hidden; 
    text-overflow: ellipsis;
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

#singularTracksHolder{
    display: flex;
    flex-direction: column;
    margin-left: 5vw;
    margin-top: 4vw;

}

 #singularTracksTitle{
    font-weight: bold;
    color: var(--Quinary-color);
    font-size: 2vw;
    text-decoration: underline;
}

#singularTrackList{
    display: flex;
    flex-direction: row;
    margin-top: 2vw;
    overflow: auto;
    scrollbar-width: thin;
    scrollbar-color: var(--Quinary-color) var(--Tertiary-color);
    margin-bottom: 5vw;
    box-sizing: border-box;
    width: 93%;
}

.singularTrack{
    display: flex;
    flex-direction: column;
    background-color: var(--Quaternary-color);
    border-radius: 10px;
    padding: 10px;
    margin-left: 1vw;
    margin-bottom: 2vw;
    cursor: pointer;
    width: 10vw;
    height: 13vw;
    
}


#singularTrackImage{
    width: 10vw;
    height: 10vw;
    border-radius: 10px;
}

.singularTrackTitle{
    font-size: 0.8vw;
    text-align: center;
    overflow: hidden; 
    text-overflow: ellipsis;
    color: var(--Quinary-color);
}

.singularTrackDuration{
    font-size: 0.7vw;
    text-align: center;
    color: var(--Quinary-color);
}

.singularTrackTitle:hover{
    transition: 0.2s ease-in-out;
    color: var(--Secondary-color);
    cursor: pointer;
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


@media screen and (max-width: 1000px) {
    #singularContainer{
        display: flex;
        flex-direction: column;
    }

    #singularImageHolder{
        display: flex;
        width: 100%;
        justify-content: center;
        border-radius: 10px;
    }

    #singularImage{
        width: 50vw;
        height: 50vw;
        border-radius: 10px;
    }

    #singularInfo{
        display: flex;
        flex-direction: column;
        align-items: center;
        margin-left: 0;
    }
}


@media screen and (max-width: 700px) {
    
    #singularTrackList{
        display: flex;
        flex-wrap: wrap;
        overflow-x: hidden;
    }

    #singularTracksTitle{
        font-size: 3vw;
        align-self: center;
    }
    
}


</style>