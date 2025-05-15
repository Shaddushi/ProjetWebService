namespace SpotifyApi.AuthSpotify;
using Entities.SpotifyEntities.TokenData;
using Entities.SpotifyEntities.UserProfile;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Core.SpotifyApi.IAuthSpotify;
public class AuthSpotify : IAuthSpotify {

    private readonly HttpClient _httpClient;
    private readonly string _clientID = "284cb1c04d27446ab68177356bc421e0";
    private readonly string _clientSecret = "1ce38611be1f444f9110b8dec5e61f7f";
    private readonly string _redirectUri = "http://localhost:5164/ConnectSpotify/Callback";
    private readonly IHttpContextAccessor _httpContextAccessor;


    public AuthSpotify(HttpClient httpClient,IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
    }

    // Renvoie vers la page de connexion spotify
    public string GetAuthorizationUrl()
    {
        string scope = "user-read-private user-read-email";
        return $"https://accounts.spotify.com/authorize?client_id={_clientID}&response_type=code&redirect_uri={_redirectUri}&scope={scope}";
    }

    // Récupère le Token
    public async Task<string> GetAccessTokenAsync(string code)
    {
        var parameters = new Dictionary<string, string>
        {
            {"grant_type", "authorization_code"},
            {"code", code},
            {"redirect_uri", _redirectUri},
            {"client_id", _clientID},
            {"client_secret", _clientSecret}
        };

        var responseContent = await _httpClient.PostAsync("https://accounts.spotify.com/api/token", new FormUrlEncodedContent(parameters)).Result.Content.ReadAsStringAsync();
        var response = System.Text.Json.JsonSerializer.Deserialize<TokenData>(responseContent);
        var httpContext = _httpContextAccessor.HttpContext;
        httpContext.Session.SetString("AccessToken",JsonConvert.SerializeObject(response));
        return response.AccessToken;
        }   

    //Récupère le Profil dans la session
    public async void GetUserProfileAsync(string accessToken)
    {
       
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        
        var responseContent = await _httpClient.GetAsync("https://api.spotify.com/v1/me").Result.Content.ReadAsStringAsync();
        var response = System.Text.Json.JsonSerializer.Deserialize<UserProfile>(responseContent);

        var httpContext = _httpContextAccessor.HttpContext;
        httpContext.Session.SetString("Profile",JsonConvert.SerializeObject(response));
    }

    public string GetSpotifyAuth(){
        return GetAuthorizationUrl();
    }
}