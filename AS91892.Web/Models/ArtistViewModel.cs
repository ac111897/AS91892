using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AS91892.Web.Models;

#nullable disable

/// <summary>
/// View model for <see cref="Artist"/> so I don't have to implement multiple parameter binding
/// </summary>
public class ArtistViewModel : Artist
{
    /// <summary>
    /// Id for the label to retrieve from the database to supply the label
    /// </summary>
    [DisplayName(nameof(Label))]
    [Required]
    public string LabelId { get; set; }
}
