using Data.Patterns;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Tracing;

namespace Data.Entities;

public partial class User //: INotifyPropertyChanged, IObserver
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? PasswordHash { get; set; }

    public string? PhoneNumber { get; set; }

    public bool? IsBlocked { get; set; }

    public int? RoleId { get; set; }

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    [NotMapped]
    public virtual ICollection<Block> BlockBlockedUsers { get; set; } = new List<Block>();

    public virtual ICollection<Block> Blocks { get; set; } = new List<Block>();

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();


    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual ICollection<Response> Responses { get; set; } = new List<Response>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<Survey> Surveys { get; set; } = new List<Survey>();

   // public event PropertyChangedEventHandler? PropertyChanged;
/*
    public virtual  ObservableCollection<Message> Messages { get; set; } = new ObservableCollection<Message>();
    public void Update(ISubject subject)
    {
        Console.WriteLine("Wysylamy powiadominie");
        this.Messages.Add(new Message());
    }*/
}