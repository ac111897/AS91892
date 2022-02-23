namespace AS91892.Data.Repositories;


/// <summary>
/// Repository for <see cref="GenreRepository"/>
/// </summary>
public class GenreRepository : BaseRepository<GenreRepository>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GenreRepository"/>
    /// </summary>
    /// <param name="context"></param>
    public GenreRepository(ApplicationDbContext context) : base(context)
    {
    }
}
