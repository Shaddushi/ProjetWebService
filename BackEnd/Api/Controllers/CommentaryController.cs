using Microsoft.AspNetCore.Mvc;
using Core.Services.ICommentary;
namespace Api.Controllers.ConnectSpotify;


[ApiController]
[Route("[controller]")]
public class CommentaryController : ControllerBase
{

    private readonly ICommentary _icommentary;

    public CommentaryController(ICommentary c){
        _icommentary = c;
    }

    [HttpGet("GetCommentaries")]
    public IActionResult GetCommentaries([FromQuery] string q)
    {
        var Commentaries = _icommentary.GetCommentaries(q);
        return Ok(Commentaries);
    }
}