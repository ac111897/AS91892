namespace AS91892.Web.Models;

/// <summary>
/// A view of an erro
/// </summary>
public class ErrorViewModel
{
    /// <summary>
    /// The request id of the <see cref="ErrorViewModel"/>
    /// </summary>
    public string? RequestId { get; set; }

    /// <summary>
    /// If the request id is <see langword="null"/> or empty
    /// </summary>
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
