namespace AS91892.Web.Models;


/// <summary>
/// View model for <see cref="Artist"/> so I don't have to implement multiple parameter binding
/// </summary>
public class ArtistViewModel : Artist
{
    /// <summary>
    /// Id for the label to retrieve from the database to supply the label
    /// </summary>
    public Guid LabelId { get; set; }
}
