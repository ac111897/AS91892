namespace AS91892.Data.Entities;

#nullable disable

/// <summary>
/// Represents an image in the database
/// </summary>
public class Image : BaseEntity
{
    /// <summary>
    /// Represents the title of the image in the database
    /// </summary>
    public string ImageTitle { get; set; }

    /// <summary>
    /// Represents the file path of the image
    /// </summary>
    [Required]
    public string FilePath { get; set; }
}
