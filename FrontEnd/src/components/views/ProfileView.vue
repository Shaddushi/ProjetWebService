
<script setup>
import { onMounted } from 'vue';
import { ref } from 'vue';
import axios from 'axios';
import { countries } from 'country-flag-icons'
const images = ref("../assets/img/defaultpic.png");
const currentPlayingSong = ref();

const props = defineProps({
        profileData: {
            Object
        },
    })


// get the profile pic if it exists else use the default one set above
onMounted(() => {
    if(props.profileData.images.length > 0){
        images.value = props.profileData.images[0].url;
    }
    axios.get("http://localhost:5164/GetterSpotify/GetCurrentlyPlayingSong",
        {withCredentials : true}
         ).then((response) => {
            if(response.data != null){  
                response = JSON.parse(response.data.response)
                currentPlayingSong.value = response;
            }
        }).catch((error)=>{
              console.log(error)
            })
    
});

</script>

<template>
    <div id="profileView">
        <div id="profileHeader">
            <img :src="images" id="profileImage" />
            <div id="profileCurrentSongInfoAndProfile">


                <div id="profileHeaderContent">
                    <div id="profileHeaderRow">     
                        <div id="profileName">   {{ props.profileData.display_name }} ·</div>
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
                    <div id="profileCurrentSong">
                        <div id="profileCurrentSongImg" class="profileContent">
                            <img :src="currentPlayingSong.item.album.images[0].url" id="profileCurrentSongImg"/>
                        </div>
                        <div id="profileCurrentSongInfo" class="profileContent">
                            <div id="profileCurrentSongTitle" class="profileSongContent">{{ currentPlayingSong.item.name }}</div>
                            <div id="profileCurrentSongAlbum" class="profileSongContent">{{ currentPlayingSong.item.album.name }}</div>
                            <div id="profileCurrentSongArtist" class="profileSongContent">Artist: {{ currentPlayingSong.item.artists[0].name }}</div>
                        </div>
                    </div>

                </div>
                
            </div>  
        </div>
    </div>


</template>


<style scoped>

#profileCurrentSongTitle{
    font-size: 2vw;
    margin-top: 1vw;
    margin-bottom: 1vw;
    color: var(--Secondary-color);
    font-family: 'Font', sans-serif;
}

#profileCurrentSongImg{
    aspect-ratio: 1 / 1;
    width: 10vw;
    height: 10vw;
    border-radius: 20px;    
    object-fit: cover;  
    margin: 2%;
}

#profileCurrentSongContainer{
    display: flex;
    flex-direction: column;
    flex-wrap: wrap;
    align-content: flex-end;
    justify-content: center;
    width: 80%;
    
}

#profileCurrentSong{
    display: flex;
    flex-direction: row;
    width: 30vw;
    height: 15vw;
    background-color: var(--Quaternary-color);
    border-radius: 20px;
    padding: 10px;
    align-self: center;
    margin-right: 8vw;
    border: 2px solid var(--Primary-color);

}

#profileCurrentSongInfoAndProfile{
    display: flex;
    flex-direction: row;
    width: 100%;
}

#profileCurrentSongInfo{
    display: flex;
    flex-direction: column;
    margin-left: 2vw;
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