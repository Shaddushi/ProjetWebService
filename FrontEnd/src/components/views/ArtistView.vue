<script setup>

import { ref } from 'vue';
import axios from 'axios';
import "../../../assets/localizedCss/styleSearch.css";
import ArtistItem from '../ui/ArtistItem.vue';

const search = ref("");
const artists = ref([]);
const offset = ref(0);

//Get the artist from the API from a search
function GetArtistsByName(){
    if(search.value == ""){
        alert("Please enter a search term.");
    }
    else{
        axios.get("http://localhost:5164/GetterSpotify/SearchArtists?q=" + search.value + "/&offset=" + 0,
        {withCredentials : true}
         ).then((response) => {
            response = JSON.parse(response.data.response)
            artists.value = response.artists.items;
            offset.value += artists.value.length;
            console.log(response.artists.items);
        }).catch((error)=>{
              console.log(error)
            })
    }
    }

function loadMoreArtist(){
        axios.get("http://localhost:5164/GetterSpotify/SearchArtists?q=" + search.value + "/&offset=" + offset.value
        ,{withCredentials : true}
         ).then((response) => {
            
            response = JSON.parse(response.data.response)
            artists.value = artists.value.concat(response.artists.items);
            offset.value += response.items.length;
        }).catch((error)=>{
              console.log(error)
            })
    }
    





</script>


<template>
    <div>
        <div id="searchBar">
            <div id="SearchInputRegrouped">
                <input type="text" placeholder="Search for artists..." id="SearchInput" v-model="search" @keyup.enter="GetArtistsByName()" autocomplete="off">
                <button id="searchButton"><img src="../../../assets/img/loupe.png" @click="GetArtistsByName()"></button>
            </div>
        </div>

        <div id="searchResultsArtist" class="Font">
            <div v-for="artist in artists" class="artistResult">
                <ArtistItem :artist="artist" />
            </div>
        </div>

        <div v-if="offset>0 && artists.length >= 0" id="loadMoreButtonDiv" class="Font">
            <button  @click="loadMoreArtist()" id="loadMoreButton">Load more</button>
        </div>
    </div>
</template>


<style scoped>

#searchResultsArtist{
    display: flex;
    flex-wrap: wrap;
    justify-content: space-around;
    margin-top: 20px;
}


.artistResult{
    display: flex;
    width: 300px;
}


</style>

