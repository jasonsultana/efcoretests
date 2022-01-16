using System.ComponentModel.DataAnnotations;

namespace EFSamples.Data.Entities;

public class Customer
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty;
}