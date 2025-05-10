using Entities.SpotifyEntities.Track;

namespace Core.SpotifyApi.IInputSpotify;

public interface IInputSpotify{

    public Task<SpotifyTracksResponse> GetTracksResponse(string q, string offset);
    public Task<Track> GetTracksIdResponse(string id);
    public Task<SpotifyAlbumsResponse> GetAlbumsResponse(string q, string offset);
    public Task<Album> GetAlbumsIdResponse(string id);
}