using Microsoft.AspNetCore.Mvc;
using Core.Services;
using Newtonsoft.Json;
using Entities.SpotifyEntities.UserProfile;
namespace Api.Controllers.ConnectSpotify;


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
    
}