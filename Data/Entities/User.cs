using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string? Email { get; set; }

    [Required]
    public string? FirstName { get; set; }

    [Required]
    public string? LastName { get; set; }

    [Required]
    public DateTime? DateOfBirth { get; set; }

    [Required]
    public string? PasswordHash { get; set; }

    [Required]
    public int? RoleId { get; set; }

    public virtual Role? Role { get; set; }
}
