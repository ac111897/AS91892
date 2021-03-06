namespace AS91892.Data.Entities;

#nullable disable

/// <summary>
/// Represents a genre in music
/// </summary>
public class Genre : BaseEntity
{
    /// <summary>
    /// The name of the genre
    /// </summary>
    [Required(AllowEmptyStrings = false, ErrorMessage = $"You must provide a value for the {nameof(Genre)}")]
    [StringLength(50, ErrorMessage = $"Length of {nameof(Title)} should be between 2 & 50", MinimumLength = 2)]
    [JsonPropertyName("title")]
    public string Title { get; set; }
}
