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
            <div class="trackName">{{ props.track.name }}</div>
            
            
            
            <div v-for="artist in props.track.artists" class="trackArtist">
                    
                {{ artist.name }}
            
            </div>
        </div>
    </div>
</template>

<style scoped>
.trackItem {
    display: flex;
    align-items: center;
    padding: 10px;
    border-bottom: 1px solid #ccc;
}
.trackImage {
    width: 50px;
    height: 50px;
    border-radius: 5px;
    margin-right: 10px;
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
    color: #666;
}

.trackItem:hover .trackName {
    color: #000;
}


</style>
