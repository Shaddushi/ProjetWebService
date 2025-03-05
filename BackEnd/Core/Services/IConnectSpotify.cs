namespace Core.Services;

public interface IConnectSpotify{
    public string GetSpotifyAuth();

    public Task<string> CallBack(string code);
    
}