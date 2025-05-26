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
const tracks = ref([]);

//get the ID from the URL
onMounted(() => {
    const route = useRoute();    
    query.value = route.params.id;
    getSingularArtistFromID()
    getArtistTopTracks();
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

async function getArtistTopTracks(){
    await axios.get("http://localhost:5164/GetterSpotify/ArtistTopTracks?id=" + query.value
        ,{withCredentials : true}
         ).then((response) => {
            if (response.data.response == null || response.data.response == "") {
                return;
            }
            response = JSON.parse(response.data.response)
            tracks.value = response.tracks;
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
                    Genre: 
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


    <div id="singularTracksHolder" v-if="tracks">
        <div id="singularTracksTitle">Most Popular Tracks</div>
            <div id="singularTrackList">
                <div v-for="track in tracks" class="singularTrack" @click="changePage('/track/' + track.id)">
                    <div class="TrackItem">
                        <img v-if="track.album?.images.length > 0" :src="track.album.images[0].url" id="singularTrackImage"/>
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


@media screen and (max-width: 700px) {
    
    #singularTrackList{
        display: flex;
        flex-wrap: wrap;
        overflow-x: hidden;
        justify-content: center;
    }

    #singularTracksTitle{
        font-size: 3vw;
        align-self: center;
    }

    #singularContainer{
        flex-direction: column;
        align-items: center;
    }
    #singularImage{
        width: 60vw;
        height: 60vw;
    }
    #singularInfo{
        align-items: center;
        margin-top: 2vw;
    }

    #singularTitle{
        font-size: 5vw;
    }

    #singularFollowers{
        font-size: 2vw;
        margin-bottom: 1vw;
    }

    #singularGenre{
        font-size: 2vw;
    }

    #singularSpotify{
        display: flex;
        width: 5vw;
        height: 5vw;
        margin-left: 0;

    }

    #singularHeaderContent{
        display: flex;
        flex-direction: column;
        align-items: center;
    }
    
}


</style>