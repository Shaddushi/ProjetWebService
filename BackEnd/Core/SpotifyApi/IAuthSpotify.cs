namespace Core.SpotifyApi;

public interface IAuthSpotify{
    public string GetSpotifyAuth();

    public Task<string> GetUserProfileAsync(string accessToken);

    public Task<string> GetAccessTokenAsync(string code);
}