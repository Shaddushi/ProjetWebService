namespace SpotifyApi.InputSpotify;
using System.Net.Http;
using Core.SpotifyApi.IInputSpotify;

using Microsoft.AspNetCore.Http;

using System.Threading.Tasks;
using Entities.SpotifyEntities.Track;
using Core.SpotifyApi.IInputSpotify;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Entities.SpotifyEntities.TokenData;

public class InputSpotify : IInputSpotify{

    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public InputSpotify(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
    }

    async public Task<SpotifyTracksResponse> GetTracksResponse( string q){

        var stringToken = _httpContextAccessor.HttpContext?.Session.GetString("AccessToken");
        var access_token = JsonConvert.DeserializeObject<TokenData>(stringToken);

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token.AccessToken);

        var response = await _httpClient.GetAsync($"https://api.spotify.com/v1/search?q={q}&type=track&limit=50").Result.Content.ReadAsStringAsync();
        
        var jsonResponse = System.Text.Json.JsonSerializer.Deserialize<SpotifyTracksResponse>(response);
        
        return jsonResponse;
    }

}