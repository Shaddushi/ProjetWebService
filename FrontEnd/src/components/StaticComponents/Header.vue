<script setup>
import { useRouter } from 'vue-router';
import { ref, watch } from 'vue';



const props = defineProps({
        display_name: {
            String
        },
        images: {
            String
        },
        
    })


const router = useRouter();
const value = router.currentRoute;


// Function to change the page
const changePage = (path) => {
    router.push(path);
};



</script>


<template>
    
    <div id="Header">  
        <nav id="nav">
        <button @click="changePage('/tracks')" :class="{OnPage: value.path === '/tracks'}">Tracks</button>
        <button @click="changePage('/albums')" :class="{OnPage: value.path === '/albums'}">Albums</button>
        <button @click="changePage('/artists')" :class="{OnPage: value.path === '/artists'}">Artists</button>
        </nav>
        <button @click="changePage('/profile')" id="Profile">
                <div id="Header_Name" class="Font">  {{ display_name }} </div >
                <img :src="props.images" id="Header_Img">
        </button>
    </div>
    
</template>


<style scoped>

#Header{
    display: flex; 
    background-color: var(--Tertiary-color);
    padding: 20px; 
    font-family: 'Font', sans-serif;
    width: 100%;
}

    #nav{
        width: 50%;
        display: flex; 
        flex-direction: row;
    }

        #nav button{
            background: none;
            color: var(--Secondary-color);
            border: none;
            font-size: large;
            margin-left: 40px;
            cursor: pointer;
            align-self: center;
            font-family: 'Font', sans-serif;
            position: relative;
            transition: color 0.3s;
        }

        #nav button::before{
            content: "";
            position: absolute;
            width: 0;
            bottom: 0;
            left: 0;
            height: 2px;
            background-color: var(--Secondary-color); 
            transition: width 0.3s;
        }

        

        #nav button:hover::before{
            width: 100%;
        }

        .OnPage::after{
            content: "";
            position: absolute;
            width: 100%;
            bottom: 0;
            left: 0;
            height: 2px;
            background-color: var(--Secondary-color); 
            transition: width 0.3s;
        }



    #Profile{
        display: flex; 
        flex-direction: row; 
        margin-left: auto;
        margin-right: 3%;
        background: none;
        border: none;
        font-family: 'Font', sans-serif;
        font-size:medium;
        cursor: pointer;
    }

        #Header_Name{
            color: var(--Secondary-color); 
            font-size:150%; 
            display: flex; 
            align-self: center;
            margin-bottom: 5px;
        }

        #Header_Img{
            aspect-ratio: 1 / 1;
            object-fit: cover; 
            border-radius: 50%;
            margin-left: 20px;
            margin-right: 20px;
            height: 64px;
            width: 64px;
        }


@media screen and (max-width:780px) {

    #Header_Name{
        display: none;
    }

    #nav button{
        font-size: 16px;
        margin-left: 20px;
    }

    
}

</style>