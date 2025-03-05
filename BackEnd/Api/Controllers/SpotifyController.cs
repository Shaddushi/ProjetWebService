using Microsoft.AspNetCore.Mvc;
using Core.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ConnectSpotify : ControllerBase
{
    private readonly IConnectSpotify _iconnectspotify;

    public ConnectSpotify(IConnectSpotify c){
        _iconnectspotify = c;
    }

    [HttpGet("GetSpotifyAuth")]
    public IActionResult Login(){
        string val = _iconnectspotify.GetSpotifyAuth();
        return Redirect(val);
    }
    [HttpGet("Callback")]
    public IActionResult CallbackLog([FromQuery] string code){
        
        Task<string> val = _iconnectspotify.CallBack(code);
 
        return Ok(val);
    }
}
