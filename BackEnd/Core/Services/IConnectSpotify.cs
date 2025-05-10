namespace Core.Services;
using Entities.SpotifyEntities.Track;

public interface IConnectSpotify{
    public string GetSpotifyAuth();

    public void CallBack(string code);

    public Task<SpotifyTracksResponse> GetTracks(string q, string offset);

    public Task<Track> SearchSongsFromId(string q);

    public Task<SpotifyAlbumsResponse> GetAlbums(string q, string offset);

    public Task<Album> SearchAlbumsFromId(string q);
    
}