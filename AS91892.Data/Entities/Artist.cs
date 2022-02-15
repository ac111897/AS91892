﻿namespace AS91892.Data.Entities;

#nullable disable

/// <summary>
/// Represents an <see cref="Artist"/> in the database
/// </summary>
/// <remarks>
/// An <see cref="Artist"/> is the top level in the database as it holds multiple <see cref="Album"/>'s which each contain <see cref="Song"/>'s
/// </remarks>
public class Artist : BaseEntity
{
    /// <summary>
    /// Represents the albums the artists have created
    /// </summary>
    [Required]
    public ICollection<Album> Albums { get; set; }

    /// <summary>
    /// Represents the <see cref="Artist"/>'s name
    /// </summary>
    [Display(Name = "Name")]
    [Required(AllowEmptyStrings = false, ErrorMessage = $"You must provide a value for the {nameof(Artist)}")]
    [StringLength(50, MinimumLength = 1)]
    public string ArtistName { get; set; }
}
