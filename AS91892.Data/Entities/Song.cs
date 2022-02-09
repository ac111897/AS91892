namespace AS91892.Data.Entities;

#nullable disable

/// <summary>
/// An <see cref="Artist"/>'s <see cref="Song"/> in the database
/// </summary>
public class Song : BaseEntity
{
    /// <summary>
    /// Represents the title of the <see cref="Song"/>
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    [StringLength(50, MinimumLength = 1)]
    public string Title { get; set; }
}
