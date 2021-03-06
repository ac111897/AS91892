using Microsoft.EntityFrameworkCore;

namespace AS91892.Data.Context;

#nullable disable 

/// <summary>
/// The applications connection to the database
/// </summary>
public class ApplicationDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class
    /// </summary>
    /// <param name="options">The options provided for the <see cref="ApplicationDbContext"/></param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        // ensures that the database has been created before providing the context
        Database.EnsureCreated();
    }

    /// <summary>
    /// The albums contained in the database
    /// </summary>
    public DbSet<Album> Albums { get; set; }
    /// <summary>
    /// The songs contained in the database
    /// </summary>
    public DbSet<Song> Songs { get; set; }
    /// <summary>
    /// The artists contained within the database
    /// </summary>
    public DbSet<Artist> Artists { get; set; }
    
    /// <summary>
    /// The images contained within the database
    /// </summary>
    public DbSet<Image> Images { get; set; }

    /// <summary>
    /// Record labels contained within the database
    /// </summary>
    public DbSet<RecordLabel> RecordLabels { get; set; }

    /// <summary>
    /// The music genres contained within the database
    /// </summary>
    public DbSet<Genre> Genres { get; set; }
}
