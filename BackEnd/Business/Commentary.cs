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


    public async void PostCommentaries(string comment, string songId, string CommenterId)
    {
        await _commentaryDataBaseAccess.AddCommentaryAsync(new Entities.Bdd.Commentaries.Commentary
        {
            Text = comment,
            SongId = songId,
            AuthorId = CommenterId,
            Date = DateTime.Now
        });
    }
}