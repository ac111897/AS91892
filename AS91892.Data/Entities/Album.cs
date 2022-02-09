namespace AS91892.Data.Entities;

/// <summary>
/// Represents an <see cref="Album"/> in the database
/// </summary>
public class Album : BaseEntity
{
#nullable disable
    /// <summary>
    /// Represents the songs in the album
    /// </summary>
    [Required]
    public IList<Song> AlbumSongs { get; set; }

#nullable restore

    /// <summary>
    /// An image for the album covers
    /// </summary>
    public Image? AlbumCover { get; set; }
}
