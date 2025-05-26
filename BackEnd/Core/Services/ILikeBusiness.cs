
using Entities.Bdd.SongLikes;

namespace Core.Services.ILikeBusiness;

public interface IlikeBusiness
{
    public Task<SongLikes> PostLikeAsync(string songId, string userId, bool isLiked);

}