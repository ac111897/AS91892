namespace AS91892.Data.Entities;

#nullable disable

/// <summary>
/// Represent's an <see cref="Artist"/>'s managing corporation
/// </summary>
public class RecordLabel : BaseEntity
{
    /// <summary>
    /// Name of the <see cref="RecordLabel"/>
    /// </summary>
    [Required]
    [StringLength(50, ErrorMessage = $"Length of {nameof(Name)} should be between 2 & 50", MinimumLength = 2)]
    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>
    /// Business address of the label
    /// </summary>
    [Required]
    [StringLength(50, ErrorMessage = $"Length of {nameof(Address)} should be between 2 & 50", MinimumLength = 2)]
    [JsonPropertyName("address")]
    public string Address { get; set; }

    /// <inheritdoc></inheritdoc>/>
    public override string ToString()
    {
        return $"{Name} - {Address}";
    }
}
