using Core.Repositories.ICommentaryDataBaseAccess;
using Entities.Bdd.Commentaries;
using Repositories.Context;
public class CommentaryDataBaseAccess : ICommentaryDataBaseAccess
{

    private readonly CommentifyContext _context;

    public CommentaryDataBaseAccess(CommentifyContext context)
    {
        _context = context;
    }

    public async Task<bool> AddCommentaryAsync(Commentary commentary)
    {

        _context.Commentaries.Add(commentary);
        var result = await _context.SaveChangesAsync();
        return result > 0;

    }

    public async Task<bool> DeleteCommentaryAsync(Commentary commentary)
    {
        if (commentary == null)
        {
            return await Task.FromResult(false);
        }

        _context.Commentaries.Remove(commentary);
        var result = _context.SaveChanges();
        return await Task.FromResult(result > 0);
    }

    public Task<List<Commentary>> GetAllCommentariesAsyncFromSongId(string id)
    {
        var filtered = _context.Commentaries.Where(c => c.SongId == id).OrderByDescending(c => c.Date);
        return Task.FromResult(filtered.ToList());
    }

    public Task<bool> UpdateCommentaryAsync(Commentary commentary)
    {
        if (commentary == null)
        {
            return Task.FromResult(false);
        }

        _context.Commentaries.Update(commentary);
        var result = _context.SaveChanges();
        return Task.FromResult(result > 0);
    }
    
}