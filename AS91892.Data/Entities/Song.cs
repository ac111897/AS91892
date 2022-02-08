namespace AS91892.Data.Entities;

#nullable disable

/// <summary>
/// An <see cref="Artist"/>'s <see cref="Song"/> in the database
/// </summary>
public class Song
{
    /// <summary>
    /// Represents the primary key in the database
    /// </summary>
    [Key]
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// Represents the title of the <see cref="Song"/>
    /// </summary>
    [Required]
    public string Title { get; set; }
}
