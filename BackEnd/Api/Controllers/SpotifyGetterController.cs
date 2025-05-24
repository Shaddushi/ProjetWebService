using Microsoft.AspNetCore.Mvc;
using Core.Services;
using Newtonsoft.Json;
using Entities.SpotifyEntities.UserProfile;
using System.Threading.Tasks;
using Api.Models.SpotifyResponse;
namespace Api.Controllers.GetterSpotify;


[ApiController]
[Route("[controller]")]
public class GetterSpotify : ControllerBase
{
    private readonly IGetterSpotify _igetterspotify;

    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetterSpotify(IGetterSpotify g, IHttpContextAccessor a)
    {
        _igetterspotify = g;
        _httpContextAccessor = a;
    }


    [HttpGet("SearchSongs")]
    public async Task<IActionResult> SearchSongs([FromQuery] string q, string offset)
    {
        var Tracks = await _igetterspotify.GetTracks(q, offset);
        return Ok(new SpotifyResponseModel { Response = Tracks });
    }

    [HttpGet("SearchSongsFromId")]
    public async Task<IActionResult> SearchSongsFromId([FromQuery] string q)
    {
        var SongsId = await _igetterspotify.SearchSongsFromId(q);

        return Ok(new SpotifyResponseModel { Response = SongsId });
    }

    [HttpGet("SearchAlbums")]
    public async Task<IActionResult> SearchAlbums([FromQuery] string q, string offset)
    {
        var Albums = await _igetterspotify.GetAlbums(q, offset);

        return Ok(new SpotifyResponseModel { Response = Albums });
    }

    [HttpGet("SearchAlbumsFromId")]
    public async Task<IActionResult> SearchAlbumsFromId([FromQuery] string q)
    {
        var AlbumsId = await _igetterspotify.SearchAlbumsFromId(q);

        return Ok(new SpotifyResponseModel { Response = AlbumsId });
    }

    [HttpGet("SearchArtists")]
    public async Task<IActionResult> SearchArtists([FromQuery] string q, string offset)
    {
        var Artists = await _igetterspotify.GetArtists(q, offset);

        return Ok(new SpotifyResponseModel { Response = Artists });
    }

    [HttpGet("SearchArtistsFromId")]
    public async Task<IActionResult> SearchArtistsFromId([FromQuery] string q)
    {
        var ArtistsId = await _igetterspotify.SearchArtistsFromId(q);

        return Ok(new SpotifyResponseModel { Response = ArtistsId });
    }

    [HttpGet("GetCurrentlyPlayingSong")]
    public async Task<IActionResult> GetCurrentSong()
    {
        var CurrentSong = await _igetterspotify.GetCurrentSong();

        return Ok(new SpotifyResponseModel { Response = CurrentSong });
    }

    [HttpGet("GetUserProfileById")]
    public async Task<IActionResult> GetUserProfileById([FromQuery] string id)
    {
        var UserProfile = await _igetterspotify.GetUserProfileById(id);
        return Ok(new SpotifyResponseModel { Response = UserProfile });

    }




}