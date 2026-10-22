using System.ComponentModel.DataAnnotations;

namespace PetCareHub.Models;

public class Appointment
{
    public int Id { get; set; }

    [Required]
    public int PetId { get; set; }
    public Pet? Pet { get; set; }

    [Required, DataType(DataType.DateTime)]
    public DateTime AppointmentDate { get; set; } = DateTime.Now.AddDays(1);

    [Required, StringLength(250)]
    public string Reason { get; set; } = "";

    [StringLength(30)]
    public string Status { get; set; } = "Pending";

    [Required]
    public string OwnerId { get; set; } = "";
}
