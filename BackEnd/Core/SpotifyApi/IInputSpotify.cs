namespace Core.SpotifyApi.IInputSpotify;

public interface IInputSpotify
{

    public Task<String> GetTracksResponse(string q, string offset);
    public Task<String> GetTracksIdResponse(string id);
    public Task<String> GetAlbumsResponse(string q, string offset);
    public Task<String> GetAlbumsIdResponse(string id);
    public Task<String> GetArtistsResponse(string q, string offset);
    public Task<String> GetArtistsIdResponse(string id);
    public Task<String> GetCurrentSongResponse();
    public Task<String> GetUserProfileByIdResponse(string id);
    
}