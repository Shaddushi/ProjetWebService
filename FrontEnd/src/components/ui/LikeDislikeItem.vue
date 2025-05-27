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

const song_like = ref()
const song_likeCount = ref(0);
const song_dislikeCount = ref(0);
const img_like = ref("../../../assets/img/like.png");
const img_dislike = ref("../../../assets/img/dislike.png");


onMounted(() => {
    GetLikeStatus();
});


async function GetLikeStatus() {
    await axios.get("http://localhost:5164/Like/GetLikeFromSongId?songId=" + props.song_id, { withCredentials: true })
        .then((response) => {
            song_like.value = response.data;
            if (song_like.value != "") {
                if (song_like.value.isLike) {
                    img_like.value = "../../../assets/img/likeActive.png";
                    img_dislike.value = "../../../assets/img/dislike.png";
                } else {
                    img_like.value = "../../../assets/img/like.png";
                    img_dislike.value = "../../../assets/img/dislikeActive.png";
                }
            } 
            else {
                img_like.value = "../../../assets/img/like.png";
                img_dislike.value = "../../../assets/img/dislike.png";
            }
        })
        .catch((error) => {
            console.error("Error fetching like status:", error);
        });

    await axios.get("http://localhost:5164/Like/GetAllLikesFromSongId?songId=" + props.song_id, { withCredentials: true })
        .then((response) => {
            song_likeCount.value = response.data[0];
            song_dislikeCount.value = response.data[1];
            
        })
        .catch((error) => {
            console.error("Error fetching current song like status:", error);
        });
}


async function LikeSong() {
    if (song_like.value != "" && song_like.value.isLike) {
        await axios.delete("http://localhost:5164/Like/DeleteLike",
         {
            data : {
            songId: props.song_id
            },
            withCredentials: true

         }).catch((error) => {
            console.error("Error disliking song:", error);
        });
    }
    else if (song_like.value != "" && !song_like.value.isLike) {
        song_like.value.isLike = true;
        await axios.put("http://localhost:5164/Like/UpdateLike", song_like.value , { withCredentials: true }
    ).catch((error) => {
            console.error("Error liking song:", error);
        });
    }
    else {
        // la chanson a déjà été likée / dislikée
        await axios.post("http://localhost:5164/Like/PostLike", {
        songId: props.song_id,
        isLike: true
        }, { withCredentials: true }).catch((error) => {
            console.error("Error liking song:", error);
        });
        
    }
    GetLikeStatus();
    // mise a jour dans le front

}


async function DislikeSong() {
    if (song_like.value != "" && !song_like.value.isLike) {
        await axios.delete("http://localhost:5164/Like/DeleteLike",
         {
            data : {
            songId: props.song_id
            },
            withCredentials: true

         }).catch((error) => {
            console.error("Error disliking song:", error);
        });
    }
    else if (song_like.value != "" && song_like.value.isLike) {
        song_like.value.isLike = false;
        await axios.put("http://localhost:5164/Like/UpdateLike", song_like.value , { withCredentials: true }
    ).catch((error) => {
            console.error("Error disliking song:", error);
        });
    }
    else {
        // la chanson a déjà été likée / dislikée
        await axios.post("http://localhost:5164/Like/PostLike", {
        songId: props.song_id,
        isLike: false
        }, { withCredentials: true }).catch((error) => {
            console.error("Error disliking song:", error);
        });
        
    }
    GetLikeStatus();
    // mise a jour dans le front

}





</script>




<template>

    <div id="likeDislikeItem">
        <img @click="LikeSong":src="img_like" id="likeDislikeImage" />
        <div class="numberLike"> {{ song_likeCount }}</div>
    </div>
    <div id="likeDislikeItem">
        <img @click="DislikeSong" :src="img_dislike" id="likeDislikeImage" />
        <div class="numberLike"> {{ song_dislikeCount }}</div>

    </div>


</template>


<style scoped>

.numberLike {
    font-size: 1.5vw;
    color: var(--Quinary-color);
}

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