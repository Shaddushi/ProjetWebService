<script setup>

import { ref } from 'vue';
import axios from 'axios';

const search = ref("");

function GetSongsByName(){
    if(search.value == ""){
        alert("Please enter a search term.");
    }
    else{
        axios.get("http://localhost:5164/ConnectSpotify/SearchSongs?q=" + search.value,
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
                <input type="text" placeholder="Search for tracks..." id="SearchInput" v-model="search" @keyup.enter="GetSongsByName()" autocomplete="off">
                <button id="searchButton"><img src="../../../assets/img/loupe.png" @click="GetSongsByName()"></button>
            </div>
        </div>

        <div id="searchResults" class="Font">
            <div v-for="track in tracks" class="trackResult">
                <img :src="track.image" class="trackImage"/>
                <div class="trackInfo">
                    <div class="trackName">{{ track.name }}</div>
                    <div class="trackArtist">{{ track.artist }}</div>
                </div>
            </div>
        </div>
    </div>
</template>



<style>

#searchBar {
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    margin-top: 5svh;
}

#SearchInput {
    border: none;
    background: none;
    width: 80%;
    height: 100%;
    padding: 13px;;
    margin-top: 3px;
    font-family: 'Font', sans-serif;
    padding-left: 20px;
    
    font-size: large;
}

#SearchInputRegrouped {
    display: flex;
    flex-direction: row;
    border: none;
    width: 500px;
    border: 2px solid var(--Secondary-color) ;
    border-radius: 1000px;
    background: rgb(105, 102, 102);
    font-size: 16px;
    margin-bottom: 10px;
}

#SearchInput::placeholder {
    color: var(--Primary-color);
    font-family: 'Font', sans-serif;
    
    font-size: large;
}



#SearchInput:focus{
    outline: none;
}

#SearchInputRegrouped:has(#SearchInput:focus) {
    outline: none;
    border-bottom: 5px solid var(--Secondary-color);
    transition: all 0.09s ease-in-out;
    
    
}


#searchButton {
    background: none;
    border: none;
    cursor: pointer;
    width: 20%;
    height: 100%;
}

#searchButton img {
    width: 40%;
    height: 40%;
    margin-top: 5px;
    margin-left: 25%;
}

</style>