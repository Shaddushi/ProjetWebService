namespace SpotifyApi.AuthSpotify;
using Entities.TokenData;
using Core.SpotifyApi;
using System.Text.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;

public class AuthSpotify : IAuthSpotify {

    private readonly HttpClient _httpClient;
    private readonly string _clientID = "284cb1c04d27446ab68177356bc421e0";
    private readonly string _clientSecret = "1ce38611be1f444f9110b8dec5e61f7f";
    private readonly string _redirectUri = "http://localhost:5164/ConnectSpotify/Callback";
    

    public AuthSpotify(HttpClient httpClient)
    {
        _httpClient = httpClient;
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
        var response = JsonSerializer.Deserialize<TokenData>(responseContent);
        return response.AccessToken;
        }   

    //Récupère le Profil
    public async Task<string> GetUserProfileAsync(string accessToken)
    {
       
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var responseContent = await _httpClient.GetAsync("https://api.spotify.com/v1/me").Result.Content.ReadAsStringAsync();
         Console.WriteLine(responseContent);
        return responseContent;
        
    }

    public string GetSpotifyAuth(){
        return GetAuthorizationUrl();
    }
}