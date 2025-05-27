<script setup>
import axios from 'axios';
import { onMounted, ref } from 'vue';
import { useRouter } from 'vue-router';


const router = useRouter();
const song = ref();
const img = ref("../../../assets/img/defaultAlbum.png");

const props = defineProps({
    song_id: {
        type: String,
        required: true
    }
});

onMounted(async () => {
    await axios.get("http://localhost:5164/GetterSpotify/SearchSongsFromId?q=" + props.song_id, { withCredentials: true })
        .then((response) => {
            if (response.data.response != null && response.data.response != "") {
                response = JSON.parse(response.data.response);
                if (response.album.images.length > 0) {
                    img.value = response.album.images[0].url;
                }
                song.value = response;
            }
        }).catch((error) => {
            console.log(error);
        });
});

// Function to change the page
const changePage = (path) => {
    router.push(path);
};



</script>


<template>

    <div id="commentSongItem" v-if="song" >
        <img :src="img" id="commentSongImage" @click="changePage('track/' + song.id)" />
        <div id="commentSongInfo">
            <div id="SongName" @click="changePage('track/' + song.id)"> {{ song.name }}</div>
            
            <div id="SongArtists" v-if="song.artists.length > 1" @click="changePage('artist/' + song.artists[0].id)" >{{ song.artists[0].name  + " and " + (song.artists.length - 1) + " more(s)"}}</div>
            <div id="SongArtists" v-else="song.artists.length <= 1" @click="changePage('artist/' + song.artists[0].id)" >{{ song.artists[0].name }}</div>
        </div>
    </div>


</template>

<style scoped>

#commentSongItem {
    display: flex;
    align-items: center;
    margin-top: 3vw;
}

#commentSongImage {
    width: 6vw;
    height: 6vw;
    border-radius: 5px;
    object-fit: cover;
    cursor: pointer;
    border: 1px solid transparent;
}

#SongName {
    font-weight: bold;
    color: #F5F5DC;
    font-size: 1vw;
    text-overflow: ellipsis;
    width: 100%;
    }

#commentSongInfo {
    margin-left: 10px;
    display: flex;
    flex-direction: column;
    color: #F5F5DC;
    max-width: 50%;
}

#SongArtists {
    display: flex;
    flex-direction: row;
    font-size: 0.9vw;
    color: var(--Quinary-color);
    margin-top: 1.5vw;
}

#SongName:hover {
    color: var(--Secondary-color);
    transition: 0.2s ease-in-out;
    cursor: pointer;

}

#SongArtists:hover {
    color: var(--Secondary-color);
    transition: 0.2s ease-in-out;
    cursor: pointer;
}

#commentSongImage:hover {
    transition: 0.2s ease-in-out;
    border: 1px solid var(--Secondary-color);
}


@media screen and (max-width: 600px) {

    #SongName {
        display: none;
    }
    #SongArtists {
        display: none;
    }
    #commentSongImage {
        width: 10vw;
        height: 10vw;
        margin-left: 3vw;
    }

    
}


</style>