using Entities.Bdd.Commentaries;
namespace Core.Repositories.ICommentaryDataBaseAccess;

public interface ICommentaryDataBaseAccess
{
    Task<List<Commentary>> GetAllCommentariesAsyncFromSongId(string id);
    Task<bool> PostCommentaryAsync(Commentary commentary);
    Task<bool> UpdateCommentaryAsync(Commentary commentary);
    Task<bool> DeleteCommentaryAsync(Commentary commentary);

    Task<List<Commentary>> GetCommentariesFromAuthorId(string id);
}