<script setup>
import { onMounted, ref, watch } from 'vue';
import { useRouter } from 'vue-router';
import { useWindowSize } from 'vue-window-size';


const router = useRouter();
const { width} = useWindowSize();
const maxchars = ref(15);


//Get the album data from the props.album
const props = defineProps({
        album: {
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
    if (width > 1250) {
        maxchars.value = 25;
    }
    else if (width > 650) {
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
    } else {
        maxchars.value = 10;
    }
}, { immediate: true });

</script>


<template>
    <div class="albumItem">
        <img @click="changePage('album/' + props.album.id)" :src="props.album.images[0].url" class="albumImage"/>
        <div class="albumInfo">
            <div @click="changePage('album/' + props.album.id)" class="albumName">{{ props.album.name.substring(0, maxchars)}}
                <span v-if="props.album.name.length > maxchars">...</span>
            </div>

            <div @click="changePage('artist/' + props.album.artists[0].id)" v-if="props.album.artists.length > 1" class="albumArtist">        
                {{ props.album.artists[0].name }} + {{ props.album.artists.length - 1 }} more
            </div>
            <div @click="changePage('artist/' + props.album.artists[0].id)" v-else class="albumArtist">
                {{ props.album.artists[0].name }}

            </div>
           
        </div>

            <div @click="changePage('album/' + props.album.album_type)" class="albumType">
                {{ props.album.album_type.substring(0, maxchars)}}
                <span v-if="props.album.album_type.length > maxchars">...</span>
        </div>
    
    </div>

</template>

<style scoped>

div{
    color: #F5F5DC;
}

.albumItem {
    display: flex;
    align-items: center;
    padding: 10px;
    margin: 0; 
    border: 1px solid transparent;
    border-radius: 10px;
    width: 100%;
    box-sizing: border-box; 
    background: var(--Quaternary-color);
    
}
.albumImage {
    width: 50px;
    height: 50px;
    border-radius: 5px;
    margin-right: 10px;
    border: var(--Primary-color) 1px solid;
}


.albumInfo {
    display: flex;
    flex-direction: column;
}
.albumName {
    font-size: 16px;
    font-weight: bold;
}



.albumArtist {
    font-size: 14px;
}

.albumType{
    display: flex;
    flex-direction: row;
    align-items: end;
    margin-left: auto;
    margin-right: 5px;
    color: #2c2828;
}


.albumName:hover {
    transition: 0.2s ease-in-out;
    color: var(--Secondary-color);
    cursor: pointer;
}

.albumImage:hover + .albumInfo .albumName {
    transition: 0.2s ease-in-out;
    color: var(--Secondary-color);
    cursor: pointer;
}

.albumImage:hover{
    cursor: pointer;
    transition: 0.2s ease-in-out;
    border: var(--Secondary-color) 1px solid;
}

.albumArtist:hover {
    transition: 0.2s ease-in-out;
    color: var(--Secondary-color);
    cursor: pointer;
}

.albumAlbum:hover {
    transition: 0.2s ease-in-out;
    color: var(--Secondary-color);
    cursor: pointer;
}

.albumItem:hover {
    border-color: var(--Secondary-color);
    box-shadow: 0 0 5px var(--Secondary-color);

}



</style>
