<script setup>
import { onMounted } from 'vue';
import { ref } from 'vue';
import {useRoute} from "vue-router";
import { useRouter } from 'vue-router';
import axios from 'axios';
import CommentItem from '../ui/CommentItem.vue';
const query = ref();
const track = ref();
const router = useRouter();
var comments = [];
var recommendedTracks = [];
const commentary = ref("");


//get the ID from the URL
onMounted(() => {
    const route = useRoute();
    query.value = route.params.id;
    getSingularSongFromID()
    getAllCommentaryFromCurrentSong()
    GetRecommendedSongs()
});

//get the song from the API using the ID
async function getSingularSongFromID(){
        await axios.get("http://localhost:5164/GetterSpotify/SearchSongsFromId?q=" + query.value
        ,{withCredentials : true}
         ).then((response) => {
            response = JSON.parse(response.data.response)
            track.value = response;
        }).catch((error)=>{
              console.log(error)
            })
    }


function getAllCommentaryFromCurrentSong(){
    axios.get("http://localhost:5164/Commentary/GetCommentaries?q=" + query.value
    ,{withCredentials : true}
     ).then((response) => {
        comments = response.data
    }).catch((error)=>{
          console.log(error)
        })
}

function addCommentary(){
    if (commentary.value == ""){
        alert("Please enter a commentary.");
    }
    else{
        axios.post("http://localhost:5164/Commentary/PostCommentaries",{
            comment: commentary.value,
            songId: track.id,
            CommenterId: localStorage.getItem("user_id")
        },{withCredentials : true}
         ).then(() => {
            getAllCommentaryFromCurrentSong()
        }).catch((error)=>{
              console.log(error)
            })
    }
}

function GetRecommendedSongs(){
    axios.get("http://localhost:5164/GetterSpotify/SimilarSongs?seedId=" + query.value
    ,{withCredentials : true}
     ).then((response) => {
        console.log(response)
        recommendedTracks = response;
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
    <!-- De base il était sensé avoir les recommandations mais spotify a deprecated ce endpoint j'ai le seum
    t'facon personne va voir ce comment donc je vais me plaindre ici -->

    <div id="singularCommentariesContainer" v-if="track">
        <div id="singularCommentariesTitle" class="Font">Commentaries</div>
        <div id="singularCommentariesInput">
            <input type="text" placeholder="Write a commentary..." id="singularCommentariesInputField" v-model="commentary" @keyup.enter="addCommentary()">
            <button id="singularCommentariesButton" @click="addCommentary()">Send</button>
        </div>
        
        <div id="singularCommentariesList">
            <div v-for="comment in comments">
                <CommentItem :comment="comment"/>
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


#singularRecommandedContainer, #singularCommentariesContainer{
    display: flex;
    flex-direction: column;
    margin-left: 5vw;
    margin-top: 4vw;

}

#singularRecommandedTitle, #singularCommentariesTitle{
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