namespace Business.GetterSpotify;
using Core.Services;
using Core.SpotifyApi.IAuthSpotify;
using Core.SpotifyApi.IInputSpotify;
using Entities.SpotifyEntities.Track;
using SpotifyApi.InputSpotify;

public class GetterSpotify : IGetterSpotify
{
    private readonly IInputSpotify _inputspotify;
    
    public GetterSpotify(IAuthSpotify s, IInputSpotify i){
        _inputspotify = i;
    }

    async public Task<SpotifyTracksResponse> GetTracks(string q, string offset){
        var response = await _inputspotify.GetTracksResponse(q, offset);
        return response;
    }

    async public Task<Track> SearchSongsFromId(string q){
        var response = await _inputspotify.GetTracksIdResponse(q);
        return response;
    }

    async public Task<SpotifyAlbumsResponse> GetAlbums(string q, string offset){
        var response = await _inputspotify.GetAlbumsResponse(q, offset);
        return response;
    }

    async public Task<Album> SearchAlbumsFromId(string q){
        var response = await _inputspotify.GetAlbumsIdResponse(q);
        return response;
    }
    
}
