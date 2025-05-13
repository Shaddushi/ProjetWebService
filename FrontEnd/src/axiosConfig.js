import axios from 'axios';
import router from './router'; 


// fichier de configuration d'axios pour gérer si le token (cookie) est expiré coté serveur ou pas

axios.interceptors.response.use(
  response => response,
  async error => {
    if (error.response && error.response.status === 500) {
      await ConnectUserToSpotify();
      router.push('/profile');
      return Promise.reject(error);
    }
    return Promise.reject(error);
  }
);

function ConnectUserToSpotify(){
        axios.get("http://localhost:5164/ConnectSpotify/GetSpotifyAuth",{withCredentials : true}).then((response) => {
            window.location.href = response.data;
            }).catch((error)=>{
              console.log(error)
            })
    }

