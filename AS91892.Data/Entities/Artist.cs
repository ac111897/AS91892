namespace AS91892.Data.Entities;

#nullable disable

/// <summary>
/// Represents an <see cref="Artist"/> in the database
/// </summary>
/// <remarks>
/// An <see cref="Artist"/> is the top level in the database as it holds multiple <see cref="Album"/>'s which each contain <see cref="Song"/>'s
/// </remarks>
public class Artist
{
    /// <summary>
    /// Represents the primary key in the database
    /// </summary>
    [Key]
    [Required]
    public Guid Id { get; set; }
    /// <summary>
    /// Represents the albums the artists have created
    /// </summary>
    [Required]
    public IList<Album> Albums { get; set; }
}
