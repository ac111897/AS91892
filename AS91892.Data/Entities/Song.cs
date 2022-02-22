namespace AS91892.Data.Entities;

/// <summary>
/// An <see cref="Artist"/>'s <see cref="Song"/> in the database
/// </summary>
public class Song : BaseEntity
{
#nullable disable
    /// <summary>
    /// Represents the title of the <see cref="Song"/>
    /// </summary>
    [Required(AllowEmptyStrings = false, ErrorMessage = $"You must provide a {nameof(Title)} for {nameof(Song)}")]
    [StringLength(50, MinimumLength = 1)]
    public string Title { get; set; }

    /// <summary>
    /// Represents other artists that may appear on this track
    /// </summary>
    public ICollection<Artist> Features { get; set; }

    /// <summary>
    /// The music genre that thet the <see cref="Song"></see>
    /// </summary>
    public Genre Genre { get; set; }
#nullable restore
    /// <summary>
    /// The length of the song
    /// </summary>
    [Required]
    [DataType(DataType.Duration)]
    public TimeSpan Duration { get; set; }

    /// <summary>
    /// The cover of the song
    /// </summary>
    public Image? Cover { get; set; }
}
