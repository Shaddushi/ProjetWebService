using Entities.Bdd.Commentaries;

namespace Core.Services.ICommentary;

public interface ICommentary
{
    public Task<List<Entities.Bdd.Commentaries.Commentary>> GetCommentaries(string q);


    public Task<bool> PostCommentaries(string comment, string songId, string CommenterId);

    public Task<bool> DeleteCommentaries(Commentary comment);

    public Task<bool> UpdateCommentaries(Commentary comment);
}