<script setup>

import { ref } from 'vue';
import axios from 'axios';
import AlbumItem from '../ui/AlbumItem.vue';
import "../../../assets/localizedCss/styleSearch.css";


const search = ref("");
const albums = ref([]);
const offset = ref(0);

// get the albums from the API from a search
function GetAlbumsByName(){
    if(search.value == ""){
        alert("Please enter a search term.");
    }
    else{
        axios.get("http://localhost:5164/GetterSpotify/SearchAlbums?q=" + search.value + "/&offset=" + 0,
        {withCredentials : true}
         ).then((response) => {
            albums.value = response.data.result.albums.items;
            offset.value += albums.value.length;
            if (albums.value.length == 0){
                alert("No albums found");
            }
        }).catch((error)=>{
              console.log(error)
            })
    }
    }
    

//Get more albums using the offset
function loadMoreAlbum(){
        axios.get("http://localhost:5164/GetterSpotify/SearchAlbums?q=" + search.value + "/&offset=" + offset.value
        ,{withCredentials : true}
         ).then((response) => {
            albums.value = albums.value.concat(response.data.result.albums.items);
            offset.value += response.data.result.albums.items.length;
        }).catch((error)=>{
              console.log(error)
            })
    }
    




</script>


<template>
    <div>
        <div id="searchBar">
            <div id="SearchInputRegrouped">
                <input type="text" placeholder="Search for albums..." id="SearchInput" v-model="search" @keyup.enter="GetAlbumsByName()" autocomplete="off">
                <button id="searchButton"><img src="../../../assets/img/loupe.png" @click="GetAlbumsByName()"></button>
            </div>
        </div>

        <div id="searchResults" class="Font">
            <div v-for="album in albums" class="albumResult">
                <AlbumItem :album="album"  />
            </div>
        </div>
        <div v-if="offset>0 && albums.length >= 0" id="loadMoreButtonDiv" class="Font">
            <button  @click="loadMoreAlbum()" id="loadMoreButton">Load more</button>
        </div>
    </div>

    
</template>

