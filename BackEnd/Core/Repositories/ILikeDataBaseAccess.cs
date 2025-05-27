using Entities.Bdd.Commentaries;
using Entities.Bdd.SongLikes;
namespace Core.Repositories.ILikeDataBaseAccess;

public interface ILikeDataBaseAccess
{

    Task<bool> PostLikeAsync(SongLikes songLikes);

    Task<SongLikes> GetLikeFromSongIdWithAuthorIdAsync(string songId, string authorId);

    Task<bool> DeleteLikeAsync(string songId, string userId);

    Task<bool> UpdateLikeAsync(SongLikes songLikes);

    Task<List<int>> GetAllLikesFromSongIdAsync(string songId);



}