namespace Core.SpotifyApi.IAuthSpotify;

public interface IAuthSpotify{
    public string GetSpotifyAuth();

    public void GetUserProfileAsync(string accessToken);

    public Task<string> GetAccessTokenAsync(string code);
}