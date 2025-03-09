<script setup>

const props = defineProps({
        display_name: {
            String
        },
        images: {
            String
        },
        
    })
</script>


<template>
    <div id="Header">  
        <div id="Header_Name" class="Font">  {{ display_name }} </div >
        <img :src="props.images" id="Header_Img">
    </div>
</template>


<style>

#Header{
    display: flex; 
    flex-direction: row; 
    background-color: #212121;
    padding: 20px;
    justify-content: flex-end;
}

#Header_Name{
    color:#1db954; 
    font-size:150%; 
    display: flex; 
    align-self: center;
    margin-bottom: 5px;
    font-family: 'Font', sans-serif;
}

#Header_Img{
    border-radius: 500px;
    margin-left: 2%;
    margin-right: 1%;
}


</style>