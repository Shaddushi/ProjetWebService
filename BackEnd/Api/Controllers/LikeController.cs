using Microsoft.AspNetCore.Mvc;
using Core.Services.ILikeBusiness;
using System.Text.Json;

namespace Api.Controllers.CommentaryBusiness;


[ApiController]
[Route("[controller]")]
public class LikeController : ControllerBase
{

    private readonly IlikeBusiness _icommentary;

    private readonly IHttpContextAccessor _httpContextAccessor;
    public LikeController(IlikeBusiness c, IHttpContextAccessor a)
    {
        _icommentary = c;
        _httpContextAccessor = a;
    }

    [HttpPost("PostLike")]
    public IActionResult PostLike([FromBody] string songId,bool isLike)
    {
        var profileJson = _httpContextAccessor.HttpContext?.Session.GetString("Profile");

        // Si l'utilisateur n'est pas connecté
        if (string.IsNullOrEmpty(profileJson))
        {
            return BadRequest("User not authenticated.");
        }

        // Deserialize le JSON pour obtenir l'ID de l'utilisateur
        using var document = JsonDocument.Parse(profileJson);
        string userId = document.RootElement.GetProperty("Id").GetString();

        var result = _icommentary.PostLikeAsync(songId, userId, isLike);

        return Ok(result.Result);
    }

}
