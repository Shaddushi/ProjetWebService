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


    public async Task<bool> PostLikeAsync(SongLikes like)
    {


        // Check if the like already exists for the given songId and userId
        // If it exists, we do not add a new one, technically there is already a security in the sql by using
        //  a unique constraint on the combination of songId and userId
        // But this is a good practice to check it before adding (i think)


        var existingLike = _context.SongLikes
            .FirstOrDefault(l => l.SongId == like.SongId && l.AuthorId == like.AuthorId);


        if (existingLike == null)
        {
            _context.SongLikes.Add(like);
            await _context.SaveChangesAsync();
            return true;
        }
        else
        {
            return false;
        }

    }

    public Task<SongLikes> GetLikeFromSongIdWithAuthorIdAsync(string songId, string authorId)
    {

        // Retrieve the like for the given songId and authorId
        // If it does not exist, return null
        return Task.FromResult(_context.SongLikes
            .FirstOrDefault(l => l.SongId == songId && l.AuthorId == authorId));
    }

    public Task<bool> DeleteLikeAsync(string songId, string userId)
    {
        var like = _context.SongLikes
            .FirstOrDefault(l => l.SongId == songId && l.AuthorId == userId);

        // If the like exists, remove it from the database
        if (like != null)
        {
            _context.SongLikes.Remove(like);
            return Task.FromResult(_context.SaveChanges() > 0);
        }

        return Task.FromResult(false);
    }


    public Task<bool> UpdateLikeAsync(SongLikes songLikes)
    {
        // Update the like in the database
        _context.SongLikes.Update(songLikes);
        return Task.FromResult(_context.SaveChanges() > 0);
    }
    
    public Task<List<int>> GetAllLikesFromSongIdAsync(string songId)
    {
        // Count the number of likes and dislike for the given songId
        var like = _context.SongLikes.Count(l => l.SongId == songId && l.IsLike);
        var dislike = _context.SongLikes.Count(l => l.SongId == songId && !l.IsLike);
        
        return Task.FromResult(new List<int> { like, dislike });
    }

}