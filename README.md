# ProjetWebService

# Info Randoms

On a duoqueue (coder à 2 sur ma session)  la semaine du 11 mai et 18 mai 
Aussi si au démarrage du projet et que vous arrivez sur le projet et que vous ne voyez pas votre profil comme il le faut, juste reloader c'est que le back s'était pas bien encore démarrer.

On a aussi ajouté sur la partie profil un truc pour voir la musique que vous écoutez actuellement
(c'est joli)

# Technologie utilisé 

Nous avons utilisé vue.js en front et .NET 9 en back

J'vous donne pas le git ici, car bon si vous voyez ca vous êtes déjà dessus...

# Expérience Générale

En vrai on s'est bien amusé, on a tout les deux bien aimé le dotnet (le vue on l'a vue (ahah) au premier semestre donc rien de vraiment nouveau mais c'est toujours cool de travailler avec cette framework) et franchement pour faire des API c'est niquel, tant que l'on range bien tout bien sur, et personnellement (Honoré) j'aimerai bien en faire plus tard en entreprise.
De plus, on pense que si on aurait eu plus de temps, on aurait pu encore plus l'amélioré mais bon c'est déjà plutôt propre on trouve.

# Comment lancer notre projet 

C'est très simple ! 

Il suffit de :

- Avoir vue et dotnet d'installer
- Aller dans le dossier FrontEnd, ouvrir un terminal et taper la commande npm install puis npm run dev
- Aller dans le dossier Backend/Api, ouvrir un autre terminal et taper la commande dotnet run
- Ensuite, vous pouvez profiter de notre projet (qui mérite un 20) en allant sur le lien qui est donné dans le terminal du FrontEnd. 

Normalement le projet est lançable aussi bien sur Windows que Linux car on est des boss
On a un swagger d'ailleurs, NORMALEMENT, c'est http://localhost:5164/swagger/index.html
Mais bon il est pas très utile car en général faut s'être connecté pour l'utiliser et on peut pas faire ca dans un swagger me semble


# Que faire ensuite ?

C'est là que les choses se compliquent :

Vous allez devoir vous connecter a spotify, admirer le site, et ensuite, être si heureux du travail rendu que vous allez mettre immédiatement un 20/20 à Honoré Duportail et Robin Capitaine.

Je vous invite bien sûr à tester le site comme vous le souhaitez !



# Les routes API


La majorité de nos routes nécessite une connexion donc le swagger fonctionne probablement pas :3

## Commentary

### Controller qui s'occupe de tout ce qui s'approche des commentaires.



    -> GET GetCommentaries

Permet de récupérer tout les commentaires d'un track


    -> POST PostCommentaries

Permet d'ajouter un commentaire en donnant le text du commentaire et l'id de la musique (necessite d'être connecté)


    -> DELETE DeleteCommentaries

Permet de supprimer le commentaire de la bdd envoyer dans le body de la requête.


    -> PUT UpdateCommentaries

Permet d'update un commentaire de la bdd en le renvoyant updater (dotnet reconnait quel commentaire il s'agit grace a la clé primaire)

    -> GET GetCommentariesFromAuthorId

Permet de récupérer toiut les commentaires fait par un utilisateur en passant son id par query (on a pas fait en sorte que c'est directement le back qui gère l'id car on s'est dit que plus tard (meme si jamais dans le cas présent car pas un vrai projet) on pourrait potentiellement utilisé cette route sans la modifié pour une page d'un autre utilisateur)


## ConnectSpotify


### Controller qui s'occupe de faire la connexion avec spotify et de récupérer son profil

    -> GET GetSpotifyAuth

Permet a l'utilisateur de se connecter a spotify. Cette route est en lien avec la suivante.


    -> GET CallBack

Spotify renvoie sur cette addresse après la connexion avec un code. Nous utilisons ce code pour récupérer un token de Spotify qu'on stock dans le back et ensuite avec ce token le profil qu'on stock dans le back aussi puis renvoie l'utilisateur sur le front.


    -> GET IsConnected

Permet de vérifier si l'utilisateur est connecté en checkant en utilisant le cookie. Si l'utilisateur est connecté, renvoie sont profil, sinon renvoie une chaîne vide.


## GetterSpotify

### Controller qui s'occupe principalement de faire les appels API vers spotify pour divers choses

    -> GET SearchSongs

Permet de récupérer 50 tracks en passant en query le nom recherché et l'offset (pour récupérer plus tard d'autres tracks)

    -> GET SearchSongsFromId

Permet de récupérer un track en donnant en query son id

    -> GET SearchAlbums

Permet de récupérer 50 albums en passant en query le nom recherché et l'offset (pour récupérer plus tard d'autres albums)

    -> GET SearchAlbumsFromId

Permet de récupérer un album en donnant en query son id

    -> GET SearchArtists

Permet de récupérer 50 artists en passant en query le nom recherché et l'offset (pour récupérer plus tard d'autres artists)

    -> GET SearchArtistsFromId

Permet de récupérer un artist en donnant en query son id

    -> GET GetCurrentlyPlayingSong

Permet de récupérer la musique qui est actuellement écouté par l'utilisateur sur spotify.

    -> GET GetUserProfileById

Permet de récupérer le profil d'un autre utilisateur. On l'utilise ici pour afficher le nom et la pfp d'une personne sur un commentaire qu'elle est fait

    -> GET ArtistTopTracks

Permet de récupérer les meilleurs tracks d'un artist en ayant son id en query


## Like

### Controller qui s'occupe de tout ce qui ce rapproche des Likes sur les musiques.

    -> POST PostLike

Permet d'ajouter a la bdd un like en donnant isLike (true si pouce bleu, false si pouce rouge) et l'id du track (l'id de la personne est récupérer dans le back)

    -> GET GetLikeFromSongId

Permet de récupérer si la personne connecté a liké le track dont l'id est passé en query et si oui renvoie l'object complet

    -> DELETE DeleteLike

Permet de supprimer un like en passant dans le body l'id du track a supprimer et le back s'occupe de récupérer l'id de la personne connecté

    -> PUT UpdateLike

Permet d'update un like en repassant l'objet complet (comme pour les coms la bdd sait qui modifié grace a l'id du like)

    -> GET GetAllLikesFromSongId

Permet de récupérer sous forme de liste  [ int,int ] le nombre de like et de dislike (première valeur nb de like, deuxième nb de dislike)

# Infos supplémentaires sur les routes API

On sait que nous étions sensé avoir 4 controllers avec DELETE PUT POST GET mais dans le sens que le projet se dirige c'était pas possible comme on en a discuté avec vous en cours.

