using Microsoft.AspNetCore.Mvc;
using Core.Services.ILikeBusiness;
using System.Text.Json;
using Api.Models.RequestModelLike;
using Entities.Bdd.SongLikes;
using Api.Models.RequestDeleteLike;

namespace Api.Controllers.CommentaryBusiness;


[ApiController]
[Route("[controller]")]
public class LikeController : ControllerBase
{

    private readonly IlikeBusiness _iLike;

    private readonly IHttpContextAccessor _httpContextAccessor;
    public LikeController(IlikeBusiness c, IHttpContextAccessor a)
    {
        _iLike = c;
        _httpContextAccessor = a;
    }

    [HttpPost("PostLike")]
    public IActionResult PostLike([FromBody] RequestModelLike requestModel)
    {
        var songId = requestModel.songId;
        bool isLike = requestModel.isLike;

        // Récupère le profil de l'utilisateur depuis la session
        var profileJson = _httpContextAccessor.HttpContext?.Session.GetString("Profile");

        // Si l'utilisateur n'est pas connecté
        if (string.IsNullOrEmpty(profileJson))
        {
            return BadRequest("User not authenticated.");
        }

        // Deserialize le JSON pour obtenir l'ID de l'utilisateur
        using var document = JsonDocument.Parse(profileJson);
        string userId = document.RootElement.GetProperty("Id").GetString();

        var result = _iLike.PostLikeAsync(songId, userId, isLike);

        return Ok(result.Result);
    }


    [HttpGet("GetLikeFromSongId")]
    public IActionResult GetLikeFromSongId([FromQuery] string songId)
    {
        // Récupère le profil de l'utilisateur depuis la session
        var profileJson = _httpContextAccessor.HttpContext?.Session.GetString("Profile");

        // Si l'utilisateur n'est pas connecté
        if (string.IsNullOrEmpty(profileJson))
        {
            return BadRequest("User not authenticated.");
        }

        // Deserialize le JSON pour obtenir l'ID de l'utilisateur
        using var document = JsonDocument.Parse(profileJson);
        string userId = document.RootElement.GetProperty("Id").GetString();

        var result = _iLike.GetLikeFromSongIdWithAuthorIdAsync(songId, userId);

        return Ok(result.Result);
    }

    [HttpDelete("DeleteLike")]
    public IActionResult DeleteLike([FromBody] RequestDeleteLike data)
    {
        // Récupère le profil de l'utilisateur depuis la session
        var profileJson = _httpContextAccessor.HttpContext?.Session.GetString("Profile");

        // Si l'utilisateur n'est pas connecté
        if (string.IsNullOrEmpty(profileJson))
        {
            return BadRequest("User not authenticated.");
        }

        // Deserialize le JSON pour obtenir l'ID de l'utilisateur
        using var document = JsonDocument.Parse(profileJson);
        string userId = document.RootElement.GetProperty("Id").GetString();

        var result = _iLike.DeleteLikeAsync(data.songId, userId);

        return Ok(result.Result);
    }

    [HttpPut("UpdateLike")]
    public IActionResult UpdateLike([FromBody] SongLikes songLikes)
    {

        var result = _iLike.UpdateLikeAsync(songLikes);

        return Ok(result.Result);
    }

    [HttpGet("GetAllLikesFromSongId")]
    public IActionResult GetAllLikesFromSongId([FromQuery] string songId)
    {
        // Récupère le nombre total de likes pour la chanson spécifiée
        var result = _iLike.GetAllLikesFromSongIdAsync(songId);

        return Ok(result.Result);
    }

}
