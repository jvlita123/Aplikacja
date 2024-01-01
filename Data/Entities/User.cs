using Data.Patterns;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public partial class User : INotifyPropertyChanged, IObserver
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

    public virtual ICollection<Attendance> Attendances { get; set; }
    [NotMapped]
    public virtual ICollection<Block> BlockBlockedUsers { get; set; }

    public virtual ICollection<Block> Blocks { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; }

    public virtual ICollection<Message> Messages { get; set; }

    public virtual ICollection<Photo> Photos { get; set; }

    public virtual ICollection<Reservation1> Reservations { get; set; }

    public virtual ICollection<Response> Responses { get; set; }

    public virtual Role? Role { get; set; }

    public virtual ICollection<Survey> Surveys { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;

    public virtual ObservableCollection<UserReservationSlots> UserReservationSlots { get; set; } = new ObservableCollection<UserReservationSlots>();

    public event Action<User, string>? NotifyUserEvent; // Event do informowania o potrzebie powiadomienia użytkownika

    public void Update(ISubject subject)
    {

        NotifyUserEvent?.Invoke(this, "Zaszła zmiana w slocie. Proszę sprawdzić.");

    }

}