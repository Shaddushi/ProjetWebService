using Core.Repositories.ICommentaryDataBaseAccess;
using Entities.Bdd.Commentaries;
using Repositories.Context;
class CommentaryDataBaseAccess : ICommentaryDataBaseAccess
{

    private readonly CommentifyContext _context;
    public async Task<bool> AddCommentaryAsync(Commentary commentary)
    {
        _context.Commentaries.Add(commentary);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public Task<bool> DeleteCommentaryAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Commentary>> GetAllCommentariesAsyncFromSongId(string id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateCommentaryAsync(Commentary commentary)
    {
        throw new NotImplementedException();
    }
}