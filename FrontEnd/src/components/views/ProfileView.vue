
<script setup>
import { onMounted } from 'vue';
import { ref } from 'vue';
import axios from 'axios';
import { countries } from 'country-flag-icons'
import CommentItem from '../ui/CommentItem.vue';
import CommentSongItem from '../ui/CommentSongItem.vue';
import CurrentPlayingSongItem from '../ui/CurrentPlayingSongItem.vue';

const images = ref("../assets/img/defaultpic.png");
const currentPlayingSong = ref();
const comments = ref([]);
const props = defineProps({
        profileData: {
            Object
        },
    })


// get the profile pic if it exists else use the default one set above
onMounted(async () => {
    if(props.profileData.images.length > 0){
        images.value = props.profileData.images[0].url;
    }
    await axios.get("http://localhost:5164/GetterSpotify/GetCurrentlyPlayingSong",
        {withCredentials : true}
         ).then((response) => {
            if(response.data.response != null && response.data.response != ""){  
                response = JSON.parse(response.data.response)
                currentPlayingSong.value = response;
            }
        }).catch((error)=>{
              console.log(error)
            })
    
    getCurrentUserComments();
});


async function getCurrentUserComments(){
    await axios.get("http://localhost:5164/Commentary/GetCommentariesFromAuthorId?authorId=" + props.profileData.id,
        {withCredentials : true}
         ).then((response) => {
            if(response.data.result != null){
                response = response.data.result;
                comments.value = response;
            }
            
        }).catch((error)=>{
              console.log(error)
            })
}


// FUnction to update or delete a comment



function deleteComment(comment) {
    axios.delete("http://localhost:5164/Commentary/DeleteCommentaries", 
        {data: comment}
    ).then(() => {
        getCurrentUserComments();
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
            getCurrentUserComments();
        }).catch((error) => {
            console.log(error);
        });
    }

}



</script>

<template>
    <div id="profileView">
        <div id="profileHeader">
            <img :src="images" id="profileImage" />
            <div id="profileCurrentSongInfoAndProfile">


                <div id="profileHeaderContent">
                    <div id="profileHeaderRow">     
                        <div id="profileName">   {{ props.profileData.display_name}} </div>
                        <div id="profileCountry">
                            <img :src="`../../../node_modules/country-flag-icons/3x2/${props.profileData.country}.svg`" :alt="`flag-${props.profileData.country}`">
                        </div> 
                    </div>
                    <div id="profileFollowers" class="profileContent">Followers: {{ props.profileData.followers_total }}</div>
                    <div id="profileEmail" class="profileContent">E-mail : {{ props.profileData.email }}</div>
                    <a id="profileSpotify" class="profileContent" :href="`https://open.spotify.com/user/${ props.profileData.id }`" target="_blank">
                        <img src="../../../assets/img/spotifyLogo.png" alt="Spotify">
                    </a>
                </div>
                
                
                <div id="profileCurrentSongContainer" v-if="currentPlayingSong && currentPlayingSong.currently_playing_type == 'track'">
                    <CurrentPlayingSongItem :currentPlayingSong="currentPlayingSong" />
                </div>
                
            </div>  
        </div>

        
    </div>
    <div id="profileCommentary">
            <div id="profileCommentaryTitle">Your Comments</div>
            <div class="profileCommentaryContent">
                <div v-for="(comment, index) in comments" :key="index" class="profileCommentaryItem">
                    <CommentItem :comment="comment" :current_id="props.profileData.id" :is_profile=true @delete-comment="deleteComment" @update-comment="updateComment"  />
                    <CommentSongItem :song_id="comment.songId"/>
                </div>
            </div>

        </div>

</template>


<style scoped>




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
        aspect-ratio: 1 / 1;
        width: 15vw;
        height: 15vw;
        border-radius: 50%;    
        object-fit: cover;  
        margin: 2%;
        border: 2px solid var(--Secondary-color);
    }



#profileCountry{
    width: auto;
    height: auto;
    margin-right: 2%;
    margin-top: 2%;
}

#profileName{
    display: flex;
    font-size: 3vw;
    margin-top: 2%;
    
    color: var(--Secondary-color);
    font-family: 'Font', sans-serif;

    margin-right: 1%;
}


#profileCountry img{
    width: 5vw;
    margin-left: 5px;
    margin-top: 3%;
}


#profileFollowers{
    margin-top: 2%;
}

.profileContent{
    font-size: 1vw;
    margin-top: 10px;
    color: var(--Quinary-color);
    font-family: 'Font', sans-serif;
}

#profileHeaderRow{
    display: flex;
    flex-direction: row;
    margin-top: 1%;
}

#profileHeaderContent{

    display: flex;
    flex-direction: column;
    width: 40vw;
    margin-top: 2vw;
}

#profileSpotify{
    display: flex;
    flex-direction: column;
    margin-top: 1.5vw;
    width: 3vw;
    height: 3vw;
}

#profileCurrentSongInfoAndProfile{
    display: flex;
    flex-direction: row;
    width: 100%;
}


#profileCurrentSongContainer{
    display: flex;
    flex-direction: column;
    flex-wrap: wrap;
    align-content: flex-end;
    justify-content: center;
    width: 80%;
    
}

#profileCommentary{
    display: flex;
    flex-direction: column;
    margin-top: 2vw;
    margin-bottom: 2vw;
    
}

#profileCommentaryTitle{
    font-size: 2vw;
    margin-top: 1vw;
    margin-left: 3vw;
    color: var(--Quinary-color);
    font-family: 'Font', sans-serif;
}

.profileCommentaryItem{
    display: flex;
    flex-direction: row;
    width: 100%;
}

.profileCommentaryContent{
    display: flex;
    flex-direction: column;
    width: 100%;
    margin-left: 3vw;
}

@media screen and (max-width: 768px) {
    #profileImage{
        width: 20vw;
        height: 20vw;
    }
    #profileName{
        font-size: 5vw;
    }
    #profileCountry img{
        width: 7vw;
        height: 7vw;
    }
    .profileContent{
        font-size: 2vw;
        margin-top: 5px;
    }

    #profilefollowers{
        margin-top: 2px;
    }

    #profileSpotify{
        width: 5vw;
        height: 5vw;
    }


}



</style>