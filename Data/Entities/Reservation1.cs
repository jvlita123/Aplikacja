using System.ComponentModel.DataAnnotations.Schema;
namespace Data.Entities;

public partial class Reservation1
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string? Title { get; set; }

    public int ServiceId { get; set; }

    public string? PrimaryColor { get; set; }

    public string? UserPhotoPath { get; set; }

    public string? AdminPhotoPath { get; set; }

    public int? StatusId { get; set; }

    public int ReservationSlotId { get; set; }

    [ForeignKey("ServiceId")]
    public virtual Service? Service { get; set; }

    [ForeignKey("StatusId")]
    public virtual Status? Status { get; set; }

    [ForeignKey("ReservationSlotId")]
    public virtual ReservationSlots? ReservationSlot { get; set; }

    [ForeignKey("UserId")]
    public virtual User? User { get; set; }

}
