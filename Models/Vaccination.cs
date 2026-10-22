using System.ComponentModel.DataAnnotations;

namespace PetCareHub.Models;

public class Vaccination
{
    public int Id { get; set; }

    [Required]
    public int PetId { get; set; }
    public Pet? Pet { get; set; }

    [Required, StringLength(100)]
    public string VaccineName { get; set; } = "";

    [Required, DataType(DataType.Date)]
    public DateTime GivenOn { get; set; } = DateTime.Today;

    [DataType(DataType.Date)]
    public DateTime? NextDueDate { get; set; }

    [StringLength(250)]
    public string? Notes { get; set; }

    [Required]
    public string OwnerId { get; set; } = "";
}
