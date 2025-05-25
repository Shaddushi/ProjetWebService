namespace Business.Commentary;

using System.Threading.Tasks;
using Core.Repositories.ICommentaryDataBaseAccess;
using Core.Services.ICommentary;
public class Commentary : ICommentary
{

    private readonly ICommentaryDataBaseAccess _commentaryDataBaseAccess;

    public Commentary(ICommentaryDataBaseAccess commentaryDataBaseAccess)
    {
        _commentaryDataBaseAccess = commentaryDataBaseAccess;
    }

    public async Task<List<Entities.Bdd.Commentaries.Commentary>> GetCommentaries(string q)
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

    public async Task<bool> DeleteCommentaries(Entities.Bdd.Commentaries.Commentary comment)
    {
        return await _commentaryDataBaseAccess.DeleteCommentaryAsync(comment);
    }

    public async Task<bool> UpdateCommentaries(Entities.Bdd.Commentaries.Commentary comment)
    {
        return await _commentaryDataBaseAccess.UpdateCommentaryAsync(comment);
    }
}