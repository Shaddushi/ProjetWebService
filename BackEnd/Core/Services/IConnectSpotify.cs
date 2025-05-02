namespace Core.Services;
using Entities.SpotifyEntities.Track;

public interface IConnectSpotify{
    public string GetSpotifyAuth();

    public void CallBack(string code);

    public Task<SpotifyTracksResponse> GetTracks(string q);
    
}