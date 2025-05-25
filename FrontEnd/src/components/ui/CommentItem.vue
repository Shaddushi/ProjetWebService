<script setup>

import axios from 'axios';
import { nextTick, onMounted, ref } from 'vue';
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
    },
    current_id: {
        type: String,
        required: true
    }
})



onMounted(() => {
    getAuthorFromComment(props.comment);
    
});


//Get the profile of the author of the comment
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

// Part for editing a comment 

const is_editing = ref(false);
const edited_comment = ref("");
const textarea = ref(null);


// Function to toggle the editing state of the comment then resize the textarea

function isEditing(){
    is_editing.value = !is_editing.value;
    if (is_editing.value) {
        nextTick(() => {
            autoResize();
        });
    } 
}

// Auto-resize the textarea based on its content

function autoResize() {
    textarea.value.style.height = 'auto';
    textarea.value.style.height = `${textarea.value.scrollHeight}px`; 
}



</script>

<template>

    <div class="singularCommentary">
        <img :src="img" class="singularCommentaryImage" />
        <div class="singularCommentaryText">
            <div class="singularCommentaryUserDateHolder">
                <div class="singularCommentaryUser">{{ Author.display_name }}</div>
                <div class="singularCommentaryDeleteDateHolder">
                    <!-- s'affiche uniquement si l'autheur est l'utilisateur connecté, on va pas laissé le modo changé le comm tout de meme -->
                    <a class="CommentarydeleteMod" v-if="Author.id === current_id " @click="isEditing() ">
                        <img src="../../../assets/img/edit.png" alt="Delete" />
                    </a>
                    <!-- s'affiche uniquement si l'autheur est l'utilisateur connecté ou si l'utilisateur est un modo (moi :3) -->
                    <a class="CommentarydeleteMod" v-if="Author.id === current_id || current_id == '8obnofokyk7knxrbyedjodate' " @click="$emit('delete-comment', comment)">
                        <img src="../../../assets/img/delete.png" alt="Delete" />
                    </a>
                    <div class="singularCommentaryDate">{{ Since }}</div>
                </div>
                
            </div>
            <div v-if="!is_editing" class="singularCommentaryText">{{ comment.text }}</div>
            <textarea v-if="is_editing" ref="textarea" @input="autoResize"  v-model="comment.text" class="singularCommentaryText" placeholder="Edit your comment..."></textarea>
            <div class="singularCommentaryCommitChangesHolder">
                <button v-if="is_editing" @click="$emit('update-comment', comment); is_editing = !is_editing" class="singularCommentaryCommitChanges">Save</button>
            </div>
        </div>
    </div>

</template>


<style scoped>

.singularCommentaryCommitChangesHolder {
    display: flex;
    flex-direction: row;
    align-items: center;
    justify-content: flex-end;
}

.singularCommentaryCommitChanges{
    background-color: var(--Secondary-color);
    color: var(--Primary-color);
    border: none;
    width: 100%;
    font-size: 1vw;
    font-family: 'Font', sans-serif;
    margin-left: 1vw;
    width: 5vw;
    height: 1.4vw;
    border-radius: 10px;
    cursor: pointer;
    margin-top: 0.5vw;
}

textarea{
    background: none;
    border: none;
    outline: none; 
    resize: none;
    overflow-y:hidden;
}

.singularCommentaryDeleteDateHolder {
    display: flex;
    flex-direction: row;
    align-items: center;
    justify-content: space-between;
}

.CommentarydeleteMod {
    display: flex;
    align-items: center;
    justify-content: space-between;
    margin-right: 10px;
}


.singularCommentaryUserDateHolder img {
    width: 1vw;
    height: 1vw;
    cursor: pointer;
}

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
