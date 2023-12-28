using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public partial class Message
{
    public int Id { get; set; }

    public string? Text { get; set; }

    public int UserId { get; set; }

    public int UserId2 { get; set; }

    [NotMapped]
    [ForeignKey("UserId")]
    public virtual User User1 { get; set; } = null!;
   
    [ForeignKey("UserId2")]
    public virtual User User2 { get; set; } = null!;
}
/*using Data.Patterns;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public partial class Message : ISubject
{
    public int Id { get; set; }

public string? Text { get; set; }

public int UserId { get; set; }

public int UserId2 { get; set; }

public bool IsNew { get; set; }


[ForeignKey("UserId")] // Specify the foreign key column for User1 navigation property
public virtual User User1 { get; set; } = null!;

[ForeignKey("UserId2")] // Specify the foreign key column for User2 navigation property
public virtual User User2 { get; set; } = null!;


/*
    private List<IObserver> observers = new List<IObserver>();

    public void Attach(IObserver observer)
    {
        observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void Notify()
    {
        foreach (var observer in observers)
        {
            observer.Update(this);
        }
    }

}

*/