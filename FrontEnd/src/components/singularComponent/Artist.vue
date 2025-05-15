<script setup>
import { onMounted } from 'vue';
import { ref } from 'vue';
import {useRoute} from "vue-router";
import { useRouter } from 'vue-router';
import axios from 'axios';
const query = ref();
const album = ref();
const router = useRouter();

//get the ID from the URL
onMounted(() => {
    const route = useRoute();    
    query.value = route.params.id;
    getSingularArtistFromID()
});

//get the song from the API using the ID
function getSingularArtistFromID(){
        axios.get("http://localhost:5164/GetterSpotify/SearchArtistsFromId?q=" + query.value
        ,{withCredentials : true}
         ).then((response) => {
            console.log(response.data)
        }).catch((error)=>{
              console.log(error)
            })
    }

// Function to change the page
const changePage = (path) => {
    router.push(path);
};
    
</script>