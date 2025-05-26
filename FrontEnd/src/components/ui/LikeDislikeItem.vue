<script setup>

import { onMounted, ref } from 'vue';
import axios from 'axios';


const props = defineProps({
    song_id: {
        type: String,
        required: true
    },
    current_id: {
        type: String,
        required: true
    }
});



function LikeSong() {
    axios.post("http://localhost:5164/Like/PostLike", {
        isLike: true,
        songId: props.song_id,
    }, { withCredentials: true })
        .then((response) => {
            if (response.data.result) {
                console.log("Song liked successfully");
            } else {
                console.error("Failed to like the song");
            }
        })
        .catch((error) => {
            console.error("Error liking the song:", error);
        });
}





</script>




<template>

    <div id="likeDislikeItem">
        <img @click="LikeSong" src="../../../assets/img/like.png" id="likeDislikeImage" />
    </div>
    <div id="likeDislikeItem">
        <img src="../../../assets/img/dislike.png" id="likeDislikeImage" />

    </div>


</template>


<style scoped>


#likeDislikeItem {
    display: flex;
    align-items: center;
    cursor: pointer;
    margin: 0.5vw;
}

#likeDislikeImage {
    width: 2vw;
    height: 2vw;
    margin-right: 0.5vw;
}



</style>