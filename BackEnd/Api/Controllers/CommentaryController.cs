using Microsoft.AspNetCore.Mvc;
using Core.Services.ICommentary;
using Entities.SpotifyEntities.UserProfile;
using System.Text.Json;
using Api.Models.RequestModel;
using Newtonsoft.Json;
using Entities.Bdd.Commentaries;

namespace Api.Controllers.CommentaryBusiness;


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

        return Ok(result.Result ? "Commentary posted successfully." : "Failed to post commentary.");

    }

    [HttpDelete("DeleteCommentaries")]
    public IActionResult DeleteCommentaries([FromBody] Commentary data)
    {

        var result = _icommentary.DeleteCommentaries(data);

        return Ok(result.Result ? "Comment deleted successfully" : "Failed to delete comment");
    }

    [HttpPut("UpdateCommentaries")]
    public IActionResult UpdateCommentaries([FromBody] Commentary data)
    {
        var result = _icommentary.UpdateCommentaries(data);

        return Ok(result.Result ? "Commentary updated successfully." : "Failed to update commentary.");
    }



    // Could have not askd for the authorId in the query, but if latter on we want to create a page
    // where you can see someoone else's profile, we won't need to change the endpoint
    [HttpGet("GetCommentariesFromAuthorId")]
    public IActionResult GetCommentariesFromAuthorId([FromQuery] string authorId)
    {
        var commentaries = _icommentary.GetCommentariesFromAuthor(authorId);
        return Ok(commentaries);
    }
}
