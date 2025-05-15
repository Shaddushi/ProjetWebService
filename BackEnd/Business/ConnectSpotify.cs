namespace Business.ConnectSpotify;
using Core.Services;
using Core.SpotifyApi.IAuthSpotify;
using Core.SpotifyApi.IInputSpotify;
using Entities.SpotifyEntities.Track;
using SpotifyApi.InputSpotify;

public class ConnectSpotify : IConnectSpotify
{
    private readonly IAuthSpotify _authspotify;
    private readonly IInputSpotify _inputspotify;
    
    public ConnectSpotify(IAuthSpotify s, IInputSpotify i){
        _authspotify = s;
        _inputspotify = i;
    }
    public string GetSpotifyAuth(){
        return _authspotify.GetSpotifyAuth();
    }

    public void CallBack(string code){
        Task<string> token = _authspotify.GetAccessTokenAsync(code);
        _authspotify.GetUserProfileAsync(token.Result);
    }

    
}
