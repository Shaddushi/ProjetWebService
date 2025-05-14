namespace Core.Services;
using Entities.SpotifyEntities.Track;

public interface IGetterSpotify{

    public Task<SpotifyTracksResponse> GetTracks(string q, string offset);

    public Task<Track> SearchSongsFromId(string q);

    public Task<SpotifyAlbumsResponse> GetAlbums(string q, string offset);

    public Task<Album> SearchAlbumsFromId(string q);
    
}