<script setup>
import { onMounted } from 'vue';

import { useRouter } from 'vue-router';

const router = useRouter();

const props = defineProps({
    currentPlayingSong: {
        type: Object,
        required: true
    }
});


const changePage = (path) => {
    router.push(path);
};

</script>


<template>
    <div id="profileCurrentSong">

    
        <div id="profileCurrentSongImgInfo">
           <div id="profileCurrentSongImg" class="profileContent">
               <img :src="currentPlayingSong.item.album.images[0].url" id="profileCurrentSongImg"/>
           </div>
           <div id="profileCurrentSongInfo" class="profileContent">
               <div id="profileCurrentSongTitle" class="profileSongContent" @click="changePage('track/' + currentPlayingSong.item.id)">{{ currentPlayingSong.item.name }}</div>
               <div id="profileCurrentSongAlbum" class="profileSongContent" @click="changePage('album/' + currentPlayingSong.item.album.id)">{{ currentPlayingSong.item.album.name }}</div>
              <div v-if="currentPlayingSong.item.artists.length > 1" @click="changePage('artist/' + currentPlayingSong.item.artists[0].id)" id="profileCurrentSongArtist" class="profileSongContent"> {{ currentPlayingSong.item.artists[0].name + " and " + (currentPlayingSong.item.artists.length - 1) + " more(s)" }}</div>
              <div v-else="currentPlayingSong.item.artists.length <= 1" @click="changePage('artist/' + currentPlayingSong.item.artists[0].id)" id="profileCurrentSongArtist" class="profileSongContent">{{ currentPlayingSong.item.artists[0].name }}</div>
        
          </div>
           
        </div>
        <div id="progressBarContent">
               <div class="progressBar"></div>
               <div class="songPoint" :style="{ width: (currentPlayingSong.progress_ms / currentPlayingSong.item.duration_ms) * 100 + '%' }"></div>
               <div class="songTime"> 
                {{ Math.floor(currentPlayingSong.progress_ms / 60000) }}:{{ Math.floor((currentPlayingSong.progress_ms % 60000) / 1000).toString().padStart(2, '0') + " / " }}
                {{ Math.floor(currentPlayingSong.item.duration_ms / 60000) }}:{{ Math.floor((currentPlayingSong.item.duration_ms % 60000) / 1000).toString().padStart(2, '0')  }}
            </div>
            </div>
    </div>
    
    
</template>

<style scoped>



#profileCurrentSongTitle{

    font-size: 1.7vw;
    margin-top: 0.5vw;
    margin-bottom: 1vw;
    color: var(--Secondary-color);
    font-family: 'Font', sans-serif;
    cursor: pointer;
    width: 10vw;
    overflow: hidden;
    text-overflow: ellipsis;
    white-space: nowrap;
    

}

#profileCurrentSongImg{
    aspect-ratio: 1 / 1;
    width: 10vw;
    height: 10vw;
    border-radius: 20px;    
    object-fit: cover;
    align-self: center;
}

#profileCurrentSongImgInfo{
    display: flex;
    flex-direction: row;
    align-self: center;
}


#profileCurrentSong{
    display: flex;
    flex-direction: column;
    width: 27vw;
    height: 14vw;
    background-color: #3f3f3e ;
    border-radius: 20px;
    padding: 10px;
    padding-top: 2vw;
    margin-right: 8vw;
    border: 2px solid var(--Primary-color);
    
    justify-content: center;

}


#profileCurrentSongInfo{
    display: flex;
    flex-direction: column;
    margin-left: 2vw;
}

#profileCurrentSongArtist{
    margin-top: 3vw;
    font-size: 0.8vw;
    color: var(--Quinary-color);
}

#profileCurrentSongAlbum{
    font-size: 1.1vw;
    color: var(--Quinary-color);
    width: 10vw;
    overflow: hidden;
    text-overflow: ellipsis;
    white-space: nowrap;
}


#profileCurrentSongAlbum:hover{
    color: var(--Secondary-color);
    transition: 0.2s ease-in-out;
    cursor: pointer;
}

#profileCurrentSongArtist:hover{
    color: var(--Secondary-color);
    transition: 0.2s ease-in-out;
    cursor: pointer;
}




#progressBarContent{
    width: 90%;
    margin-top: 0.5vw;
}

.songTime{
    display: flex;
    justify-content: flex-end;
    margin-top: 0.5vw;
    font-size: 0.8vw;
    color: var(--Quinary-color);
    font-family: 'Font', sans-serif;
    
}

.songPoint{
    position: relative;
    height: 0.2vw;
    background-color: var(--Secondary-color);
    border-radius: 50px;
    transition: width 0.1s ease-in-out;
    margin-left: 1vw;
}

.progressBar{
    width: 100%;
    height: 0.2vw;
    background-color: var(--Quinary-color);
    border-radius: 50px;
    min-width: 100%;
    width: 90%;
    margin-top: 1vw;
    margin-left: 1vw;
}


</style>