namespace SpotifyApi.InputSpotify;
using System.Net.Http;
using Core.SpotifyApi.IInputSpotify;

using Microsoft.AspNetCore.Http;

using System.Threading.Tasks;
using Entities.SpotifyEntities.Track;
using Core.SpotifyApi.IInputSpotify;
using System.Net.Http.Headers;

public class InputSpotify : IInputSpotify{

    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public InputSpotify(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
    }

    async public Task<SpotifyTracksResponse> GetTracksResponse( string q){

        var accessToken = _httpContextAccessor.HttpContext?.Session.GetString("AccessToken");

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var response = await _httpClient.GetAsync($"https://api.spotify.com/v1/search?q={q}&type=track&limit=50").Result.Content.ReadAsStringAsync();
        var jsonResponse = System.Text.Json.JsonSerializer.Deserialize<SpotifyTracksResponse>(response);
        
        return jsonResponse;
    }

}