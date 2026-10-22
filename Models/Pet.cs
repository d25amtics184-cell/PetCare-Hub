using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetCareHub.Models;

public class Pet
{
    public int Id { get; set; }

    [Required, StringLength(80)]
    public string Name { get; set; } = "";

    [Required, StringLength(40)]
    public string Species { get; set; } = "Dog";

    [StringLength(80)]
    public string? Breed { get; set; }

    [Range(0, 50)]
    public int Age { get; set; }

    [StringLength(20)]
    public string? Gender { get; set; }

    [StringLength(500)]
    public string? Notes { get; set; }

    [Required]
    public string OwnerId { get; set; } = "";

    [ForeignKey(nameof(OwnerId))]
    public ApplicationUser? Owner { get; set; }

    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public ICollection<Vaccination> Vaccinations { get; set; } = new List<Vaccination>();
}
