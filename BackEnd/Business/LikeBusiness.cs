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

    public Task<SongLikes> GetLikeFromSongIdWithAuthorIdAsync(string songId,string authorId)
    {
        return _likeDataBaseAccess.GetLikeFromSongIdWithAuthorIdAsync(songId, authorId);
    }

    public async Task<bool> PostLikeAsync(string songId, string userId,bool isLiked)
    {
        
        // technically, we could make the SongLikes Object inside the function, but i prefer to create it in the business layer
        return await _likeDataBaseAccess.PostLikeAsync(new SongLikes
        {
            SongId = songId,
            AuthorId = userId,
            IsLike = isLiked,
            Date = DateTime.UtcNow
        });
    }

    public Task<bool> DeleteLikeAsync(string songId, string userId)
    {
        return _likeDataBaseAccess.DeleteLikeAsync(songId, userId);
    }

    public Task<List<int>> GetAllLikesFromSongIdAsync(string songId)
    {
        return _likeDataBaseAccess.GetAllLikesFromSongIdAsync(songId);
    }

    

    public Task<bool> UpdateLikeAsync(SongLikes songLikes)
    {
        return _likeDataBaseAccess.UpdateLikeAsync(songLikes);
    }
}