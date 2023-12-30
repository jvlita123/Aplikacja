using Data.Patterns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Data.Entities;

public partial class Reservation
{ 
    public int Id { get; set; }

    public int UserId { get; set; }

    public DateTime Start { get; set; }

    public DateTime End { get; set; }

    public string? Title { get; set; }

    public int ServiceId { get; set; }

    public string? PrimaryColor { get; set; }

    public string? SecondaryColor { get; set; }

    public int? StatusId { get; set; }
    [JsonIgnore]

    public virtual Service Service { get; set; } = null!;

    public virtual Status? Status { get; set; }

/*    private int? _statusId;
    public int? StatusId
    {
        get { return _statusId; }
        set { _statusId = value; NotifyPropertyChanged("Reservation"); Notify(); }
    }*/
    public virtual User User { get; set; } = null!;

/*
    #region Obserwator

    private List<IObserver> _observers = new List<IObserver>();

    public void Attach(IObserver observer)
    {
        Console.WriteLine("Subject: Attached an observer.");
        this._observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        this._observers.Remove(observer);
        Console.WriteLine("Subject: Detached an observer.");
    }

    public void Notify()
    {
        Console.WriteLine("Subject: Notifying observers...");

        foreach (var observer in _observers)
        {
            observer.Update(this);
        }
    }
    #endregion

    public event PropertyChangedEventHandler PropertyChanged;

    private void NotifyPropertyChanged(string property)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }*/
}
