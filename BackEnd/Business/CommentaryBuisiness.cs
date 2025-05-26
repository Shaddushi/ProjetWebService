namespace Business.CommentaryBusiness;

using Entities.Bdd.Commentaries;
using System.Threading.Tasks;
using Core.Repositories.ICommentaryDataBaseAccess;
using Core.Services.ICommentary;
public class CommentaryBusiness : ICommentary
{

    private readonly ICommentaryDataBaseAccess _commentaryDataBaseAccess;

    public CommentaryBusiness(ICommentaryDataBaseAccess commentaryDataBaseAccess)
    {
        _commentaryDataBaseAccess = commentaryDataBaseAccess;
    }

    public async Task<List<Commentary>> GetCommentaries(string q)
    {
        return await _commentaryDataBaseAccess.GetAllCommentariesAsyncFromSongId(q);
    }


    public async Task<bool> PostCommentaries(string comment, string songId, string CommenterId)
    {
        return await _commentaryDataBaseAccess.AddCommentaryAsync(new Entities.Bdd.Commentaries.Commentary
        {
            Text = comment,
            SongId = songId,
            AuthorId = CommenterId,
            Date = DateTime.Now
        });
    }

    public async Task<bool> DeleteCommentaries(Commentary comment)
    {
        return await _commentaryDataBaseAccess.DeleteCommentaryAsync(comment);
    }

    public async Task<bool> UpdateCommentaries(Commentary comment)
    {
        return await _commentaryDataBaseAccess.UpdateCommentaryAsync(comment);
    }

    public async Task<List<Commentary>> GetCommentariesFromAuthor(string id)
    {
        var allCommentaries = await _commentaryDataBaseAccess.GetCommentariesFromAuthorId(id);
        return allCommentaries.Where(c => c.AuthorId == id).ToList();
    }
}