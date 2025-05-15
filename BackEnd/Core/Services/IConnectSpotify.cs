namespace Core.Services;

public interface IConnectSpotify{
    public string GetSpotifyAuth();

    public void CallBack(string code);

    
}