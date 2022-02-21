using System.ComponentModel.DataAnnotations.Schema;

namespace AS91892.Data.Entities;

/// <summary>
/// Represents a entity in the database using an id, this is to make it simpler to create an entity
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Represents the key of the entity in the database
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Key]
    [Required]
    public Guid Id { get; set; }
}
