<script setup>

import { ref } from 'vue';
import axios from 'axios';
import AlbumItem from '../ui/AlbumItem.vue';

const search = ref("");
const albums = ref([]);
const offset = ref(0);

// get the albums from the API from a search
function GetAlbumsByName(){
    if(search.value == ""){
        alert("Please enter a search term.");
    }
    else{
        axios.get("http://localhost:5164/ConnectSpotify/SearchAlbums?q=" + search.value + "/&offset=" + 0,
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
        axios.get("http://localhost:5164/ConnectSpotify/SearchAlbums?q=" + search.value + "/&offset=" + offset.value
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



<style scoped>

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
    background: var(--Quaternary-color);
    font-size: 16px;
    margin-bottom: 10px;
}

#SearchInput::placeholder {
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
    margin-top: 8px;
    margin-left: 28%;
}

#searchResults {
    display: grid;
    grid-template-columns: repeat(auto-fill, minmax(40%, 1fr));
    gap: 1rem;

    width: 96%;
    margin-top: 40px;
    margin-left: 2%;
    margin-right: 2%;

}



#loadMoreButton{
    font-family: 'Font', sans-serif;
    background: var(--Secondary-color);
    border: none;
    color: var(--Quaternary-color);
    padding: 10px 20px;
    text-align: center;
    text-decoration: none;
    font-size: 16px;
    margin: 4px 2px;
    cursor: pointer;
    border-radius: 5px;
}

#loadMoreButtonDiv{
    display: flex;
    justify-content: center;
    align-items: center;
    margin-top: 20px;
    margin-bottom: 50px;
}



@media screen and (max-width: 1150px) {
    #searchResults {
        grid-template-columns: repeat(auto-fill, minmax(100%, 1fr));
    }
    .trackResult {
        min-width: 100%;
        max-height: 100%;
    }
    
}


@media screen and (max-width: 600px) {
    #SearchInputRegrouped {
        width: 90%;
    }

    #searchButton img {
        width: 30px;
        height: 100%;
        margin-top: 8px;
        margin-left: 20%;
    }
    
}

</style>