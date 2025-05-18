namespace Entities.Bdd.SongLikes;


public class SongLikes
{
    public int Id { get; set; }
    public string AuthorId { get; set; }
    public string SongId { get; set; }

    public bool IsLike { get; set; }
    public DateTime Date { get; set; }
}