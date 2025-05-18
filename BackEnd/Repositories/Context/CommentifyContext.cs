using Microsoft.EntityFrameworkCore;
using Entities.Bdd.Commentaries;
using Entities.Bdd.SongLikes;
namespace Repositories.Context;

public class CommentifyContext : DbContext
{
    public CommentifyContext(DbContextOptions<CommentifyContext> options) : base(options)
    {
    }

    public DbSet<Commentary> Commentaries { get; set; }

    public DbSet<SongLikes> SongLikes { get; set; }
}