<script setup>

import { ref } from 'vue';
import axios from 'axios';
import TrackItem from '../ui/TrackItem.vue';
import "../../../assets/localizedCss/styleSearch.css";

const search = ref("");
const tracks = ref([]);
const offset = ref(0);



// get the songs from the API from a search
function GetSongsByName(){
    if(search.value == ""){
        alert("Please enter a search term.");
    }
    else{
        axios.get("http://localhost:5164/ConnectSpotify/SearchSongs?q=" + search.value + "/&offset=" + 0
        ,{withCredentials : true}
         ).then((response) => {
            tracks.value = response.data.result.tracks.items;
            offset.value += tracks.value.length;
            if (tracks.value.length == 0){
                alert("No albums found");
            }
        }).catch((error)=>{
              console.log(error)
            })
    }
    }


//Get more song using the offset
function loadMoreSong(){
        axios.get("http://localhost:5164/ConnectSpotify/SearchSongs?q=" + search.value + "/&offset=" + offset.value
        ,{withCredentials : true}
         ).then((response) => {
            tracks.value = tracks.value.concat(response.data.result.tracks.items);
            offset.value += response.data.result.tracks.items.length;
        }).catch((error)=>{
              console.log(error)
            })
    }
    





</script>


<template>
    <div>
        <div id="searchBar">
            <div id="SearchInputRegrouped">
                <input type="text" placeholder="Search for tracks..." id="SearchInput" v-model="search" @keyup.enter="GetSongsByName()" autocomplete="off">
                <button id="searchButton"><img src="../../../assets/img/loupe.png" @click="GetSongsByName()"></button>
            </div>
        </div>

        <div id="searchResults" class="Font">
            <div v-for="track in tracks" class="trackResult">
                <TrackItem :track="track" />
            </div>
        </div>
        <div v-if="offset>0 && tracks.length >= 0" id="loadMoreButtonDiv" class="Font">
            <button  @click="loadMoreSong()" id="loadMoreButton">Load more</button>
        </div>

    </div>
</template>


