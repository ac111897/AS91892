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
    public string Name { get; set; }

    /// <summary>
    /// Business address of the label
    /// </summary>
    public string Address { get; set; }
}
