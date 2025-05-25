<script setup>
import { onMounted, ref, watch } from 'vue';
import { useRouter } from 'vue-router';
import { useWindowSize } from 'vue-window-size';



const router = useRouter();
const { width} = useWindowSize();
const maxchars = ref(25);


//Get the artist data from the props
const props = defineProps({
        artist: {
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
    <div class="artistItem">
        <img v-if="props.artist.images.length > 0" @click="changePage('artist/' + props.artist.id)" :src="props.artist.images[0].url" class="artistImage"/>
        <img v-else @click="changePage('artist/' + props.artist.id)" src="../../../assets/img/defaultpic.png" class="artistImage"/>
        <div class="artistInfo">
            <div @click="changePage('artist/' + props.artist.id)" class="artistName">{{ props.artist.name.substring(0, maxchars)}}
                <span v-if="props.artist.name.length > maxchars">...</span>
            </div>

           
        </div>

    </div>
</template>

<style scoped>

div{
    color: #F5F5DC;
}

.artistItem {
    display: flex;
    flex-direction: column;
    align-items: center;
    width: 360px;
    padding: 10px;
    margin: 0; 
    border: 1px solid transparent;
    transition: border-color 0.2s ease-in-out; 
    border-radius: 10px;
    width: 100%;
    box-sizing: border-box; 
    background: var(--Quaternary-color);
    margin-right: 10px;
    margin-bottom: 10px;
    
}
.artistImage {
    width: 100%;
    aspect-ratio: 1/1;
    border-radius: 5px;
    border: 1px solid rgba(255, 255, 255, 0);
}


.artistInfo {
    display: flex;
    flex-direction: column;
}
.artistName {
    font-size: 16px;
    font-weight: bold;
    margin-top: 10px;
}


.artistName:hover {
    transition: 0.2s ease-in-out;
    color: var(--Secondary-color);
    cursor: pointer;
}

.artistImage:hover + .artistInfo .artistName {
    transition: 0.2s ease-in-out;
    color: var(--Secondary-color);
    cursor: pointer;
}

.artistImage:hover{
    cursor: pointer;
    transition: 0.2s ease-in-out;
    border: var(--Secondary-color) 1px solid;
}


.artistItem:hover {
    border-color: var(--Secondary-color);
    box-shadow: 0 0 5px var(--Secondary-color);
}



</style>
