<script setup>
import { onMounted } from 'vue';

const props = defineProps({
        track: {
            type: Object,
            required: true
        }
    })



</script>


<template>
    <div class="trackItem">
        <img :src="props.track.album.images[0].url" class="trackImage"/>
        <div class="trackInfo">
            <div class="trackName">{{ props.track.name.substring(0, 20)}}
                <span v-if="props.track.name.length > 20">...</span>
            </div>

            <div v-if="props.track.artists.length > 1" class="trackArtist">        
                {{ props.track.artists[0].name }} + {{ props.track.artists.length - 1 }} more
            </div>
            <div v-else class="trackArtist">
                {{ props.track.artists[0].name }}

            </div>
           
        </div>
        
        <div class="trackAlbum">
                {{ props.track.album.name.substring(0, 20)}}
                <span v-if="props.track.album.name.length > 20">...</span>
        </div>
    
    </div>
</template>

<style scoped>

div{
    color: #F5F5DC;
}

.trackItem {
    display: flex;
    align-items: center;
    padding: 10px;
    margin: 0; 
    border: var(--Primary-color) 3px solid;
    border-radius: 10px;
    width: 100%;
    box-sizing: border-box; 
    background: var(--Quaternary-color);
}
.trackImage {
    width: 50px;
    height: 50px;
    border-radius: 5px;
    margin-right: 10px;
    border: var(--Primary-color) 1px solid;
}


.trackInfo {
    display: flex;
    flex-direction: column;
}
.trackName {
    font-size: 16px;
    font-weight: bold;
}



.trackArtist {
    font-size: 14px;
}

.trackAlbum{
    display: flex;
    flex-direction: row;
    align-items: end;
    margin-left: auto;
    margin-right: 5px;
    color: #2c2828;
}


.trackName:hover {
    transition: 0.2s ease-in-out;
    color: var(--Secondary-color);
    cursor: pointer;
}

.trackImage:hover + .trackInfo .trackName {
    transition: 0.2s ease-in-out;
    color: var(--Secondary-color);
    cursor: pointer;
}

.trackImage:hover{
    cursor: pointer;
    transition: 0.2s ease-in-out;
    border: var(--Secondary-color) 1px solid;
}

.trackArtist:hover {
    transition: 0.2s ease-in-out;
    color: var(--Secondary-color);
    cursor: pointer;
}

.trackAlbum:hover {
    transition: 0.2s ease-in-out;
    color: var(--Secondary-color);
    cursor: pointer;
}

.trackItem:hover {
    border: var(--Secondary-color) 1px solid;
    transition: 0.2s ease-in-out;

}



</style>
