<script setup>
import { onMounted, ref, watch } from 'vue';
import { useRouter } from 'vue-router';
import { useWindowSize } from 'vue-window-size';



const router = useRouter();
const { width} = useWindowSize();
const maxchars = ref(25);


//Get the track data from the props
const props = defineProps({
        track: {
            type: Object,
            required: true
        }
    })



// Function to change the page
const changePage = (path) => {
    router.push(path);
};

// Set the maxchars value based on the window size
onMounted(() => {
    if (width.value > 1250) {
        maxchars.value = 25;
    }
    else if (width.value > 650) {
        maxchars.value = 20;
    } else {
        maxchars.value = 10;
    }
});

watch(width, (newWidth) => {
    if (newWidth > 1250) {
        maxchars.value = 25;
    }
    else if (newWidth > 650) {
        maxchars.value = 20;
    } 
    else {
        maxchars.value = 10;
    }
});

</script>


<template>
    <div class="trackItem">
        <img v-if="props.track.album.images.length > 0" @click="changePage('track/' + props.track.id)" :src="props.track.album.images[0].url" class="trackImage"/>
        <img v-else @click="changePage('track/' + props.track.id)" src="../../../assets/img/defaultpic.png" class="trackImage"/>
        <div class="trackInfo">
            <div @click="changePage('track/' + props.track.id)" class="trackName">{{ props.track.name.substring(0, maxchars)}}
                <span v-if="props.track.name.length > maxchars">...</span>
            </div>

            <div @click="changePage('artist/' + props.track.artists[0].id)" v-if="props.track.artists.length > 1" class="trackArtist">        
                {{ props.track.artists[0].name }} + {{ props.track.artists.length - 1 }} more
            </div>
            <div @click="changePage('artist/' + props.track.artists[0].id)" v-else class="trackArtist">
                {{ props.track.artists[0].name }}

            </div>
           
        </div>
    
        <div @click="changePage('album/' + props.track.album.id)" class="trackAlbum">
                {{ props.track.album.name.substring(0, maxchars)}}
                <span v-if="props.track.album.name.length > maxchars">...</span>
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
    border: 1px solid transparent;
    transition: border-color 0.2s ease-in-out; 
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
    border-color: var(--Secondary-color);
    box-shadow: 0 0 5px var(--Secondary-color);
}



</style>
