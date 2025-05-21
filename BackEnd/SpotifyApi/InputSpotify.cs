namespace SpotifyApi.InputSpotify;
using System.Net.Http;
using Core.SpotifyApi.IInputSpotify;
using Microsoft.AspNetCore.Http;

using System.Threading.Tasks;
using Core.SpotifyApi.IInputSpotify;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Entities.SpotifyEntities.TokenData;

public class InputSpotify : IInputSpotify
{

    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public InputSpotify(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
    }

    async public Task<String> GetTracksResponse(string q, string offset)
    {

        var stringToken = _httpContextAccessor.HttpContext?.Session.GetString("AccessToken");
        var access_token = JsonConvert.DeserializeObject<TokenData>(stringToken);

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token.AccessToken);

        return await _httpClient.GetAsync($"https://api.spotify.com/v1/search?q={q}&type=track&limit=50&offset={offset}").Result.Content.ReadAsStringAsync();

    }

    async public Task<String> GetTracksIdResponse(string q)
    {

        var stringToken = _httpContextAccessor.HttpContext?.Session.GetString("AccessToken");
        var access_token = JsonConvert.DeserializeObject<TokenData>(stringToken);

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token.AccessToken);

        return await _httpClient.GetAsync($"https://api.spotify.com/v1/tracks/{q}").Result.Content.ReadAsStringAsync();

    }

    async public Task<String> GetAlbumsResponse(string q, string offset)
    {

        var stringToken = _httpContextAccessor.HttpContext?.Session.GetString("AccessToken");
        var access_token = JsonConvert.DeserializeObject<TokenData>(stringToken);

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token.AccessToken);

        return await _httpClient.GetAsync($"https://api.spotify.com/v1/search?q={q}&type=album&limit=50&offset={offset}").Result.Content.ReadAsStringAsync();

    }

    async public Task<String> GetAlbumsIdResponse(string q)
    {

        var stringToken = _httpContextAccessor.HttpContext?.Session.GetString("AccessToken");
        var access_token = JsonConvert.DeserializeObject<TokenData>(stringToken);

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token.AccessToken);

        return await _httpClient.GetAsync($"https://api.spotify.com/v1/albums/{q}").Result.Content.ReadAsStringAsync();

    }

    async public Task<String> GetArtistsResponse(string q, string offset)
    {

        var stringToken = _httpContextAccessor.HttpContext?.Session.GetString("AccessToken");
        var access_token = JsonConvert.DeserializeObject<TokenData>(stringToken);

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token.AccessToken);

        return await _httpClient.GetAsync($"https://api.spotify.com/v1/search?q={q}&type=artist&limit=50&offset={offset}").Result.Content.ReadAsStringAsync();

    }

    async public Task<String> GetArtistsIdResponse(string q)
    {

        var stringToken = _httpContextAccessor.HttpContext?.Session.GetString("AccessToken");
        var access_token = JsonConvert.DeserializeObject<TokenData>(stringToken);

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token.AccessToken);

        return await _httpClient.GetAsync($"https://api.spotify.com/v1/artists/{q}").Result.Content.ReadAsStringAsync();

    }

    async public Task<String> GetCurrentSongResponse()
    {

        var stringToken = _httpContextAccessor.HttpContext?.Session.GetString("AccessToken");
        var access_token = JsonConvert.DeserializeObject<TokenData>(stringToken);

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token.AccessToken);

        return await _httpClient.GetAsync($"https://api.spotify.com/v1/me/player/currently-playing").Result.Content.ReadAsStringAsync();

    }

    async public Task<String> GetUserProfileByIdResponse(string id)
    {

        var stringToken = _httpContextAccessor.HttpContext?.Session.GetString("AccessToken");
        var access_token = JsonConvert.DeserializeObject<TokenData>(stringToken);

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token.AccessToken);

        return await _httpClient.GetAsync($"https://api.spotify.com/v1/users/{id}").Result.Content.ReadAsStringAsync();

    }
    
 

}