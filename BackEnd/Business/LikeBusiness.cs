namespace Business.LikeBusiness;

using Entities.Bdd.Commentaries;
using System.Threading.Tasks;
using Core.Repositories.ICommentaryDataBaseAccess;
using Core.Services.ICommentary;
using Core.Services.ILikeBusiness;
using Entities.Bdd.SongLikes;
using Core.Repositories.ILikeDataBaseAccess;
public class LikeBusiness : IlikeBusiness
{

    private readonly ILikeDataBaseAccess _likeDataBaseAccess;

    public LikeBusiness(ILikeDataBaseAccess likeDataBaseAccess)
    {
        _likeDataBaseAccess = likeDataBaseAccess;
    }

    public async Task<SongLikes> PostLikeAsync(string songId, string userId,bool isLiked)
    {
        return await _likeDataBaseAccess.PostLikeAsync(songId, userId, isLiked);
    }


}