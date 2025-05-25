<script setup>
import { onMounted } from 'vue';
import { ref } from 'vue';
import {useRoute} from "vue-router";
import { useRouter } from 'vue-router';
import axios from 'axios';
import CommentItem from '../ui/CommentItem.vue';
import { defineEmits } from 'vue';
const query = ref();
const track = ref();
const router = useRouter();
const comments = ref([]);
const current_id = ref("");
const commentary = ref("");
const img = ref();

//get the ID from the URL
onMounted(() => {
    const route = useRoute();
    query.value = route.params.id;
    img.value = localStorage.getItem("user_img");
    getSingularSongFromID()
    getAllCommentaryFromCurrentSong()
    getCurrentProfileId()
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
        if(response.data.response != ""){
            comments.value = response.data.result;
        }

    }).catch((error)=>{
          console.log(error)
        })
}

function addCommentary(){
    console.log(commentary.value)
    if (commentary.value == ""){
        alert("Please enter a commentary.");
    }
    else{
        axios.post("http://localhost:5164/Commentary/PostCommentaries", {
            "comment": commentary.value,
            "songId": query.value
        }
        ,{withCredentials : true}
         ).then(() => {
            commentary.value = "";
            getAllCommentaryFromCurrentSong()
        }).catch((error)=>{
              console.log(error)
            })
    }
}

function getCurrentProfileId(){
    axios.get("http://localhost:5164/ConnectSpotify/IsConnected",{withCredentials : true}).then((response) => {
        if(response.data != ""){
            current_id.value = response.data.id;
        }
        else{
            return "";
        }
    }).catch((error)=>{
        console.log(error)
    })
}


// Function to change the page
const changePage = (path) => {
    router.push(path);
};


// Functions to delete a comment or update it

function deleteComment(comment) {
    axios.delete("http://localhost:5164/Commentary/DeleteCommentaries", 
        {data: comment}
    ).then(() => {
        getAllCommentaryFromCurrentSong();
    }).catch((error) => {
        console.log(error);
    });
}

function updateComment(comment) {
    if (comment.text == ""){
        deleteComment(comment);
    }
    else{
        axios.put("http://localhost:5164/Commentary/UpdateCommentaries", comment).then(() => {
            getAllCommentaryFromCurrentSong();
        }).catch((error) => {
            console.log(error);
        });
    }

}


    
</script>


<template>
    <div id="singularContainer" v-if="track">
        <div id="singularImageHolder">  
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
        <div id="singularCommentariesTitle" class="Font">Comment Section</div>
        <div id="singularCommentariesUserImage" v-if="img">
            <img :src="img" id="singularCommentariesUserImage"/>
            <div>
                <textarea type="text" placeholder="Write a commentary..." id="singularCommentariesInputField" v-model="commentary"></textarea>
                <button id="singularCommentariesButton" @click="addCommentary()">Send</button>
            </div>
        
        </div>
    
        <div id="singularCommentariesList" >
            <div v-for="comment in comments" :key="comment.id">
                <CommentItem :comment="comment" :current_id="current_id" @delete-comment="deleteComment" @update-comment="updateComment"/>
            </div>
        </div>
    </div>

</template>



<style scoped>


/* Track style */

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
    width: 60vw;
    overflow: hidden; 
    text-overflow: ellipsis;
}

#singularAlbum{
    background: none;
    border: none;
    font-size: 1.5vw;
    margin-left: 2vw;
    margin-top: 1vw;
    color: var(--Quinary-color);
    width: 60vw;
    overflow: hidden; 
    text-overflow: ellipsis;
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


#singularCommentariesContainer{
    display: flex;
    flex-direction: column;
    margin-left: 5vw;
    margin-top: 4vw;
    margin-bottom: 5vw;

}

 #singularCommentariesTitle{
    font-weight: bold;
    color: var(--Quinary-color);
    font-size: 2vw;

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


/* Commentaries Section */

textarea {
  outline: none; 
  border: none; 
}

#singularCommentariesUserImage{
    display: flex;
    flex-direction: row;
    align-items: center;
}

#singularCommentariesUserImage img{
    width: 5vw;
    height: 5.5vw;
    border-radius: 50%;
    margin-left: 1vw;
    aspect-ratio: 1 / 1;
}

#singularCommentariesUserImage div{
    display: flex;
    flex-direction: column;
    align-items: flex-end;
}

#singularCommentariesInputField{
    display: flex;
    flex-direction: row;
    margin-left: 1.5vw;
    margin-top: 2vw;
    width: 65vw;
    height: 5vw;
    background-color: var(--Quaternary-color);
    resize: none;
    border-radius: 10px;
    padding: 10px;
    color: var(--Quinary-color);
    font-size: 1vw;
    font-family: 'Font', sans-serif;
    scrollbar-width: thin;
    scrollbar-color: var(--Quinary-color) var(--Primary-color);
}

#singularCommentariesInputField::placeholder{
    font-size: 1vw;
    font-family: 'Font', sans-serif;
}

#singularCommentariesButton{
    background-color: var(--Secondary-color);
    color: var(--Primary-color);
    border: none;
    width: 100%;
    font-size: 1vw;
    font-family: 'Font', sans-serif;
    margin-left: 1vw;
    width: 5vw;
    height: 1.4vw;
    border-radius: 10px;
    cursor: pointer;
    margin-top: 0.5vw;
    
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

    #singularTitle{
        font-size: 5vw;
        margin-left: 0;
        margin-top: 2vw;
        width: 80vw;
        text-align: center;
    }

    #singularAlbum{
        font-size: 2.5vw;
        margin-left: 0;
        margin-top: 1vw;
        width: 80vw;
        text-align: center;
    }

    #singularSpotify{
        display: flex;
        flex-direction: column;
        margin-top: 1.5%;
        width: 5vw;
        height: 5vw;
        margin-left: 0;
    }
    
    #singularArtistHolder{
        display: flex;
        align-items: center;
        margin-left: 0;
        margin-top: 1vw;
    }

    #singularCommentariesUserImage{
        width: 100vw;

    }

    #singularCommentariesInputField{
        width: 80vw;
    }

    #singularCommentariesButton{
        height: 3vw;
        width: 7vw;
        margin-top: 1vw;
        font-size: 1.3vw;
    }
    
}




</style>