using Data.Patterns;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public partial class Message //: ISubject
{
    public int Id { get; set; }

    public string? Text { get; set; }

    public int UserId { get; set; }

    public int UserId2 { get; set; }
    public Boolean IsNew { get; set; } = false;

    [NotMapped]
    [ForeignKey("UserId")]
    public virtual User User1 { get; set; } = null!;
   
    [ForeignKey("UserId2")]
    public virtual User User2 { get; set; } = null!;

   /* private List<IObserver> observers = new List<IObserver>();

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
        Console.WriteLine(observers);

        foreach (var observer in observers)
        {
            Console.WriteLine(observer);

            observer.Update(this);
        }
    }*/
}