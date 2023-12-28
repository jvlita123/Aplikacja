using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public partial class User
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
}

/*

using Data.Patterns;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public partial class User : IObserver
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

    // public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
    public virtual ICollection<Message> messages { get; set; } = new ObservableCollection<Message>();

    public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual ICollection<Response> Responses { get; set; } = new List<Response>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<Survey> Surveys { get; set; } = new List<Survey>();

    public User()
    {
        //  messages.CollectionChanged += MessagesChanged; // Dodajemy event do obsługi zmian w kolekcji
    }

    public ObservableCollection<Message> Messages
    {
        get => (ObservableCollection<Message>)messages;
        set
        {
            if (messages != null)
                //     messages.CollectionChanged -= MessagesChanged;

                messages = value;

            if (messages != null) { }
            //   messages.CollectionChanged += MessagesChanged;
        }
    }

    private void MessagesChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        Console.WriteLine($"User {Id} received a notification about Messages collection change.");
        // Tutaj możesz dodać logikę reakcji użytkownika na zmiany w kolekcji Messages
    }

    public void Update(ISubject subject)
    {
        // Jeśli metoda zostanie wywołana z powodu zmian w Messages, ta logika zostanie również wykonana
        if (subject == messages)
        {
            Console.WriteLine($"User {Id} received a notification about Messages collection change.!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            // Tutaj możesz dodać logikę reakcji użytkownika na zmiany w Messages
        }
    }

}
*/