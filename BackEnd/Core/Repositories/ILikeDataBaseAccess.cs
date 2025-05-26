using Entities.Bdd.Commentaries;
using Entities.Bdd.SongLikes;
namespace Core.Repositories.ILikeDataBaseAccess;

public interface ILikeDataBaseAccess
{

    Task<SongLikes> PostLikeAsync(string songId, string userId,bool isLiked);
    
}