using Microsoft.AspNetCore.Mvc;
using Core.Services;
using Newtonsoft.Json;
using Entities.SpotifyEntities.UserProfile;
namespace Api.Controllers;


[ApiController]
[Route("[controller]")]
public class GetterSpotify : ControllerBase
{
    private readonly IGetterSpotify _igetterspotify;

    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetterSpotify(IGetterSpotify g,IHttpContextAccessor a){
        _igetterspotify = g;
        _httpContextAccessor = a;
    }

    
    [HttpGet("SearchSongs")]
    public IActionResult SearchSongs([FromQuery] string q, string offset)
    {
        var Tracks = _igetterspotify.GetTracks(q, offset);
        return Ok(Tracks);
    }

    [HttpGet("SearchSongsFromId")]
    public IActionResult SearchSongsFromId([FromQuery] string q)
    {
        var SongsId = _igetterspotify.SearchSongsFromId(q);
        return Ok(SongsId);
    }

    [HttpGet("SearchAlbums")]
    public IActionResult SearchAlbums([FromQuery] string q, string offset)
    {
        var Albums = _igetterspotify.GetAlbums(q, offset);
        return Ok(Albums);
    }

    [HttpGet("SearchAlbumsFromId")]
    public IActionResult SearchAlbumsFromId([FromQuery] string q)
    {
        var AlbumsId = _igetterspotify.SearchAlbumsFromId(q);
        return Ok(AlbumsId);
    }
}