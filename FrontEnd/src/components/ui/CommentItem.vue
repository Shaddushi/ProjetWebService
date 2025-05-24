<script setup>

import axios from 'axios';
import { onMounted, ref } from 'vue';
import dayjs from 'dayjs';
import relativeTime from 'dayjs/plugin/relativeTime';
import utc from 'dayjs/plugin/utc';
import 'dayjs/locale/fr';

const Author = ref({});
var Since;
dayjs.extend(relativeTime);
dayjs.extend(utc);
dayjs.locale('en');

var img = "../../../assets/img/defaultpic.png"
const props = defineProps({
    comment: {
        type: Object,
        required: true
    }
})

onMounted(() => {
    getAuthorFromComment(props.comment);
});

function getAuthorFromComment(comment) {
    axios.get("http://localhost:5164/GetterSpotify/GetUserProfileById?id=" + comment.authorId, { withCredentials: true })
        .then((response) => {
            response = JSON.parse(response.data.response);
            Author.value = response;
            if (response.images.length > 0) {
                img = response.images[0].url;
            } 

            // Parse the date and format it
            const date = new Date(comment.date)
            Since = dayjs(date).fromNow();

        }).catch((error) => {
            console.log(error);
        });
}

</script>



<template>

    <div class="singularCommentary">
        <img :src="img" class="singularCommentaryImage" />
        <div class="singularCommentaryText">
            <div class="singularCommentaryUserDateHolder">
                <div class="singularCommentaryUser">{{ Author.display_name }}</div>
                <div class="singularCommentaryDate">{{ Since }}</div>
            </div>
            <div class="singularCommentaryText">{{ comment.text }}</div>
        </div>
    </div>

</template>


<style scoped>

.singularCommentaryUserDateHolder {
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;
    font-size: 0.8vw;
}

.singularCommentary {
    display: flex;
    flex-direction: row;
    margin-bottom: 10px;
    padding: 10px;
    border-radius: 5px;
    min-width: 90vw;
    max-width: 90vw;
    margin-top: 3vw;
}
.singularCommentaryUser {
    font-weight: bold;
    color: #1DB954;
    font-size: 1.1vw;
    margin-bottom: 5px;
}
.singularCommentaryText {
    margin-left: 10px;
    color: #fff; 
    min-width: 80vw;
    max-width: 80vw;
    font-size: 1vw;
}

.singularCommentaryImage {
    width: 3vw;
    height: 3.2vw;
    border-radius: 50%;
    aspect-ratio: 1 / 1;
}

@media (max-width: 1000px) {
    .singularCommentary {
        width: 90vw;
        font-size: 2vw;
    }
    .singularCommentaryImage {
        width: 5vw;
        height: 5vw;
    }
    .singularCommentaryUser {
        font-size: 3vw;
    }
}


</style>
