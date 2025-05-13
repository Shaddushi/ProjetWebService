
<script setup>
import { onMounted } from 'vue';
import { ref } from 'vue';
import axios from 'axios';
import { countries } from 'country-flag-icons'
const images = ref("../assets/img/defaultpic.png");

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
});

</script>

<template>
    <div id="profileView">
        <div id="profileHeader">
            <img :src="images" id="profileImage" />
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
    width: 100%;
}

#profileSpotify{
    display: flex;
    flex-direction: column;
    margin-top: 1%;
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