namespace Business;
using Core.Services;
using Core.SpotifyApi;

public class ConnectSpotify : IConnectSpotify
{
    private readonly IAuthSpotify _authspotify;
    
    public ConnectSpotify(IAuthSpotify s){
        _authspotify = s;
    }
    public string GetSpotifyAuth(){
        return _authspotify.GetSpotifyAuth();
    }

    public Task<string> CallBack(string code){
        Task<string> token = _authspotify.GetAccessTokenAsync(code);
        return _authspotify.GetUserProfileAsync(token.Result);
    }
}
