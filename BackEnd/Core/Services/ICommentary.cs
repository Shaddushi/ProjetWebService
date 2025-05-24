namespace Core.Services.ICommentary;

public interface ICommentary
{
    public Task<List<Entities.Bdd.Commentaries.Commentary>> GetCommentaries(string q);


    public void PostCommentaries(string comment, string songId, string CommenterId);
}