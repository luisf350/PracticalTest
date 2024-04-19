using System.ComponentModel.DataAnnotations;

namespace PracticalTest.Entities.Entities;

public class Employee : EntityBase
{
    [Required]
    public string FullName { get; set; } = null!;
    public DateTime BirthDate { get; set; }
}
