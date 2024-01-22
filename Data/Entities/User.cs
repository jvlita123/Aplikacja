using Data.Patterns;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public partial class User : IObserver
{
    [Key]
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? PasswordHash { get; set; }

    public string? PhoneNumber { get; set; }

    public bool? IsBlocked { get; set; }

    public int? RoleId { get; set; }

    public virtual Role? Role { get; set; }

    public virtual ICollection<Attendance>? Attendances { get; set; }

    public virtual ICollection<Enrollment>? Enrollments { get; set; } = new List<Enrollment>();

    public virtual ICollection<Message>? Messages { get; set; } = new List<Message>();

    public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();

    public virtual ICollection<Reservation>? Reservations { get; set; }


    public virtual ObservableCollection<UserReservationSlots>? UserReservationSlots { get; set; } = new ObservableCollection<UserReservationSlots>();

    public event Action<User, string>? NotifyUserEvent;

    public void Update(ISubject subject)
    {
        if (subject is ReservationSlots ReservationSlots)
        {
            ReservationSlots reservationSlots = (ReservationSlots)subject;
            string message;

            if (reservationSlots.IsAvailable == true)
            {
                message = $"{reservationSlots.Date.ToShortDateString()} {reservationSlots.StartTime} - {reservationSlots.EndTime} is available again.";
            }
            else
            {
                message = $"{reservationSlots.Date.ToShortDateString()} {reservationSlots.StartTime} - {reservationSlots.EndTime} is unavailable again.";
            }
            NotifyUserEvent?.Invoke(this, message);
        }
    }
}