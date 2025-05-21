namespace Core.Services.ICommentary;

public interface ICommentary
{
    public string GetCommentaries(string q);


    public string PostCommentaries(string comment, string songId, string CommenterId);
}