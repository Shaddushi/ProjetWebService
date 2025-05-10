using Microsoft.AspNetCore.Mvc;
using Core.Services;
using Newtonsoft.Json;
using Entities.SpotifyEntities.UserProfile;
namespace Api.Controllers;


[ApiController]
[Route("[controller]")]
public class ConnectSpotify : ControllerBase
{
    private readonly IConnectSpotify _iconnectspotify;

    private readonly IHttpContextAccessor _httpContextAccessor;

    public ConnectSpotify(IConnectSpotify c,IHttpContextAccessor a){
        _iconnectspotify = c;
        _httpContextAccessor = a;
    }

    [HttpGet("GetSpotifyAuth")]
    public IActionResult Login(){
        string val = _iconnectspotify.GetSpotifyAuth();
        return Ok(val);
    }

    [HttpGet("Callback")]
    public IActionResult CallbackLog([FromQuery] string code){
        
        _iconnectspotify.CallBack(code);
        
        return Redirect("http://localhost:5173/profile");
    }

    [HttpGet("IsConnected")]
    public IActionResult IsConnected(){
        
        var temp_profile = _httpContextAccessor.HttpContext?.Session.GetString("Profile");
        // Si l'utilisateur n'est pas connecté
        if (temp_profile == null){
            return Ok("");
        }
        var Profile = JsonConvert.DeserializeObject<UserProfile>(temp_profile);
        return Ok(Profile);
    }
    
    [HttpGet("SearchSongs")]
    public IActionResult SearchSongs([FromQuery] string q, string offset)
    {
        var Tracks = _iconnectspotify.GetTracks(q, offset);
        return Ok(Tracks);
    }

    [HttpGet("SearchSongsFromId")]
    public IActionResult SearchSongsFromId([FromQuery] string q)
    {
        var SongsId = _iconnectspotify.SearchSongsFromId(q);
        return Ok(SongsId);
    }

    [HttpGet("SearchAlbums")]
    public IActionResult SearchAlbums([FromQuery] string q, string offset)
    {
        var Albums = _iconnectspotify.GetAlbums(q, offset);
        return Ok(Albums);
    }

    [HttpGet("SearchAlbumsFromId")]
    public IActionResult SearchAlbumsFromId([FromQuery] string q)
    {
        var AlbumsId = _iconnectspotify.SearchAlbumsFromId(q);
        return Ok(AlbumsId);
    }
}