namespace Core.Services;

public interface IGetterSpotify{

    public Task<String> GetTracks(string q, string offset);

    public Task<String> SearchSongsFromId(string q);

    public Task<String> GetAlbums(string q, string offset);

    public Task<String> SearchAlbumsFromId(string q);
    
}