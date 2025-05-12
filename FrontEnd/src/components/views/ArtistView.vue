<script setup>

import { ref } from 'vue';
import axios from 'axios';
import "../../../assets/localizedCss/styleSearch.css";

const search = ref("");


//Get the artist from the API from a search
function GetArtistsByName(){
    if(search.value == ""){
        alert("Please enter a search term.");
    }
    else{
        axios.get("http://localhost:5164/ConnectSpotify/SearchArtist?q=" + search.value,
        {withCredentials : true}
         ).then((response) => {
            console.log(response.data);
        }).catch((error)=>{
              console.log(error)
            })
    }
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

        <div id="searchResults" class="Font">
            <div v-for="artist in artists" class="artistResult">
                <img :src="artist.image" class="artistImage"/>
                <div class="artistInfo">
                    <div class="artistName">{{ artist.name }}</div>
                    <div class="artistArtist">{{ artist.artist }}</div>
                </div>
            </div>
        </div>
    </div>
</template>


