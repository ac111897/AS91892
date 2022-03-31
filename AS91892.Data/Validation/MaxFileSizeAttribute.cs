using Microsoft.AspNetCore.Http;

namespace AS91892.Data.Validation;

/// <summary>
/// Valites that a property meets a specified file size
/// </summary>
public class MaxFileSizeAttribute : ValidationAttribute
{
    private readonly int _maxFileSize;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="MaxFileSizeAttribute"/> class
    /// </summary>
    public MaxFileSizeAttribute(int maxFileSize)
    {
        _maxFileSize = maxFileSize;
    }

    /// <inheritdoc></inheritdoc>
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var file = value as IFormFile;

        if (file is not null)
        {
            if (file.Length > _maxFileSize)
            {
                return new ValidationResult(GetErrorMessage());
            }
        }

        return ValidationResult.Success;
    }

    private string GetErrorMessage()
    {
        return $"Maximum allowed file size is { _maxFileSize} bytes.";
    }
}
