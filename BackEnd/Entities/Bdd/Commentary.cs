namespace Entities.Bdd.Commentaries;

public class Commentary
{
    public int Id { get; set; }
    public string AuthorId { get; set; }
    public string SongId { get; set; }

    public string Text { get; set; }

    public DateTime Date { get; set; }
}