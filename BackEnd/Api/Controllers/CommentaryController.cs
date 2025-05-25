using Microsoft.AspNetCore.Mvc;
using Core.Services.ICommentary;
using Entities.SpotifyEntities.UserProfile;
using System.Text.Json;
using Api.Models.RequestModel;
using Newtonsoft.Json;

namespace Api.Controllers.Commentary;


[ApiController]
[Route("[controller]")]
public class CommentaryController : ControllerBase
{

    private readonly ICommentary _icommentary;

    private readonly IHttpContextAccessor _httpContextAccessor;
    public CommentaryController(ICommentary c, IHttpContextAccessor a)
    {
        _icommentary = c;
        _httpContextAccessor = a;
    }

    [HttpGet("GetCommentaries")]
    public IActionResult GetCommentaries([FromQuery] string q)
    {
        var Commentaries = _icommentary.GetCommentaries(q);
        return Ok(Commentaries);
    }

    [HttpPost("PostCommentaries")]
    public IActionResult PostCommentaries([FromBody] RequestModel requestModel)
    {
        var comment = requestModel.comment;
        var songId = requestModel.songId;
        var profileJson = _httpContextAccessor.HttpContext?.Session.GetString("Profile");

        // Si l'utilisateur n'est pas connecté
        if (string.IsNullOrEmpty(profileJson))
        {
            return BadRequest(profileJson);
        }

        // Deserialize le JSON pour obtenir l'ID de l'utilisateur
        using var document = JsonDocument.Parse(profileJson);
        string id = document.RootElement.GetProperty("Id").GetString();
        var result = _icommentary.PostCommentaries(comment, songId, id);
        
        return Ok(result);

    }

    [HttpDelete("DeleteCommentaries")]
    public IActionResult DeleteCommentaries([FromBody] Entities.Bdd.Commentaries.Commentary data)
    {

        _icommentary.DeleteCommentaries(data);

        return Ok("Comment deleted successfully");
    }

    [HttpPut("UpdateCommentaries")]
    public IActionResult UpdateCommentaries([FromBody] Entities.Bdd.Commentaries.Commentary data)
    {
        _icommentary.UpdateCommentaries(data);

        return Ok("Comment updated successfully");
    }
}
