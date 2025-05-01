using Entities.SpotifyEntities.Track;

namespace Core.SpotifyApi.IInputSpotify;

public interface IInputSpotify{

    public Task<SpotifyTracksResponse> GetTracksResponse(string q);
}