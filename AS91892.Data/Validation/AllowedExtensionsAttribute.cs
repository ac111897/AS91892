using Microsoft.AspNetCore.Http;

namespace AS91892.Data.Validation;

/// <summary>
/// Validation attribute to validate a file is one of the allowed extensions
/// </summary>
public class AllowedExtensionsAttribute : ValidationAttribute
{
    private readonly string[] _extensions;

    /// <summary>
    /// Initializes a new instance of the <see cref="AllowedExtensionsAttribute"/> class
    /// </summary>
    public AllowedExtensionsAttribute(string[] extensions)
    {
        _extensions = extensions;
    }

    /// <inheritdoc></inheritdoc>
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var file = value as IFormFile; // cast the object to a form file

        if (file is not null)
        {
            ReadOnlySpan<char> extension = Path.GetExtension(file.FileName.AsSpan()); // validate that this is the right extension

            if (!_extensions.Contains(extension.ToString().ToLower()))
            {
                return new ValidationResult(GetErrorMessage()); 
            }
        }

        return ValidationResult.Success;
    }

    private static string GetErrorMessage()
    {
        return $"This photo extension is not allowed!";
    }
}
