namespace Business.GetterSpotify;
using Core.Services;
using Core.SpotifyApi.IAuthSpotify;
using Core.SpotifyApi.IInputSpotify;
using SpotifyApi.InputSpotify;

public class GetterSpotify : IGetterSpotify
{
    private readonly IInputSpotify _inputspotify;

    public GetterSpotify(IAuthSpotify s, IInputSpotify i)
    {
        _inputspotify = i;
    }

    async public Task<String> GetTracks(string q, string offset)
    {
        var response = await _inputspotify.GetTracksResponse(q, offset);
        return response;
    }

    async public Task<String> SearchSongsFromId(string q)
    {
        var response = await _inputspotify.GetTracksIdResponse(q);
        return response;
    }

    async public Task<String> GetAlbums(string q, string offset)
    {
        var response = await _inputspotify.GetAlbumsResponse(q, offset);
        return response;
    }

    async public Task<String> SearchAlbumsFromId(string q)
    {
        var response = await _inputspotify.GetAlbumsIdResponse(q);
        return response;
    }

    async public Task<String> GetArtists(string q, string offset)
    {
        var response = await _inputspotify.GetArtistsResponse(q, offset);
        return response;
    }

    async public Task<String> SearchArtistsFromId(string q)
    {
        var response = await _inputspotify.GetArtistsIdResponse(q);
        return response;
    }

    async public Task<String> GetCurrentSong()
    {
        var response = await _inputspotify.GetCurrentSongResponse();
        return response;
    }

    async public Task<String> GetUserProfileById(string id)
    {
        var response = await _inputspotify.GetUserProfileByIdResponse(id);
        return response;
    }
    
    async public Task<String> GetArtistTopTracks(string id)
    {
        var response = await _inputspotify.GetArtistTopTracksResponse(id);
        return response;
    }
  
}
