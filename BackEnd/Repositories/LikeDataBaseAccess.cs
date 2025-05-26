using Core.Repositories.ILikeDataBaseAccess;
using Entities.Bdd.Commentaries;
using Entities.Bdd.SongLikes;
using Repositories.Context;
public class LikeDataBaseAccess : ILikeDataBaseAccess
{

    private readonly CommentifyContext _context;

    public LikeDataBaseAccess(CommentifyContext context)
    {
        _context = context;
    }


    public async Task<SongLikes> PostLikeAsync(string songId, string userId,bool isLiked)
    {
        var like = new SongLikes
        {
            SongId = songId,
            AuthorId = userId,
            IsLike = isLiked,
            Date = DateTime.UtcNow
        };

        _context.SongLikes.Add(like);
        await _context.SaveChangesAsync();

        return like;
    }

}