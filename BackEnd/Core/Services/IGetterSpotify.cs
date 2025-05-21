namespace Core.Services;

public interface IGetterSpotify
{

    public Task<String> GetTracks(string q, string offset);

    public Task<String> SearchSongsFromId(string q);

    public Task<String> GetAlbums(string q, string offset);

    public Task<String> SearchAlbumsFromId(string q);

    public Task<String> GetArtists(string q, string offset);

    public Task<String> SearchArtistsFromId(string q);

    public Task<String> GetCurrentSong();

    public Task<String> GetUserProfileById(string id);
   
    
}