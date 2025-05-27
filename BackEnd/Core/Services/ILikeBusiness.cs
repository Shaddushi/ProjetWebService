
using Entities.Bdd.SongLikes;

namespace Core.Services.ILikeBusiness;

public interface IlikeBusiness
{
    public Task<bool> PostLikeAsync(string songId, string userId, bool isLiked);

    public Task<SongLikes> GetLikeFromSongIdWithAuthorIdAsync(string songId, string authorId);

    public Task<bool> DeleteLikeAsync(string songId, string userId);
    public Task<bool> UpdateLikeAsync(SongLikes songLikes);

    public Task<List<int>> GetAllLikesFromSongIdAsync(string songId);

}