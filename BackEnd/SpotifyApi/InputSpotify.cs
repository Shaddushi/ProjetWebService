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
        var response = await _httpClient.GetAsync($"https://api.spotify.com/v1/search?offset=5&limit=50&query={q}&type=album&locale=fr,fr-FR;q%3D0.9,en;q%3D0.8,en-GB;q%3D0.7,en-US;q%3D0.6").Result.Content.ReadAsStringAsync();
        var jsonResponse = System.Text.Json.JsonSerializer.Deserialize<SpotifyTracksResponse>(response);
        return jsonResponse;
    }

}