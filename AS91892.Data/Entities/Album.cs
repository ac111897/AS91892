namespace AS91892.Data.Entities;

#nullable disable

/// <summary>
/// Represents an <see cref="Album"/> in the database
/// </summary>
public class Album
{
    /// <summary>
    /// Represents the primary key in the database
    /// </summary>
    [Required]
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// Represents the songs in the album
    /// </summary>
    [Required]
    public IList<Song> AlbumSongs { get; set; }

    /// <summary>
    /// An image for the album covers
    /// </summary>
    public object AlbumCover { get; set; }
}
