namespace SpotifyApi.InputSpotify;
using System.Net.Http;
using Core.SpotifyApi.IInputSpotify;

using System.Threading.Tasks;
using Entities.SpotifyEntities.Track;
using Core.SpotifyApi.IInputSpotify;

public class InputSpotify : IInputSpotify{

    private readonly HttpClient _httpClient;

    public InputSpotify(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    async public Task<SpotifyTracksResponse> GetTracksResponse(string q){
        var response = await _httpClient.GetAsync($"https://api.spotify.com/v1/search?q={q}&type=track&limit=50").Result.Content.ReadAsStringAsync();
        var jsonResponse = System.Text.Json.JsonSerializer.Deserialize<SpotifyTracksResponse>(response);
        return jsonResponse;
    }

}