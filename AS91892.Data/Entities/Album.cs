namespace AS91892.Data.Entities;

/// <summary>
/// Represents an <see cref="Album"/> in the database
/// </summary>
public class Album : BaseEntity
{
    /// <summary>
    /// An image for the album covers
    /// </summary>
    public Image? AlbumCover { get; set; }
#nullable disable
    /// <summary>
    /// Represents the songs in the album
    /// </summary>
    [Required]
    [Display(Name = "Album Songs")]
    public ICollection<Song> AlbumSongs { get; set; }

    /// <summary>
    /// Title of the album
    /// </summary>
    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string Title { get; set; }

    /// <summary>
    /// Year that the album was released
    /// </summary>
    [Required]
    public int Year { get; set; }
}
