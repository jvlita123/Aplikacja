﻿using Data.Patterns;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public partial class ReservationSlots : ISubject
{
    public int Id { get; set; }

    public int ServiceId { get; set; }

    public int? ReservationId { get; set; }

    public DateTime Date { get; set; }

    public TimeSpan StartTime { get; set; }

    public TimeSpan EndTime { get; set; }

    private bool _isAvailable;

    public bool IsAvailable
    {
        get { return _isAvailable; }
        set
        {
            _isAvailable = value;
            Notify();
        }
    }

    [ForeignKey("ServiceId")]
    public virtual Service Service { get; set; } = null!;

    [ForeignKey("ReservationId")]
    public virtual Reservation? Reservation { get; set; }

    public virtual ObservableCollection<UserReservationSlots>? UserReservationSlots { get; set; }

    public List<IObserver> _observers = new List<IObserver>();

    #region Observer

    public void Attach(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        _observers.Remove(observer);
    }

    public void Notify()
    {
        ReservationSlots thisSlot = this;
        if (UserReservationSlots != null)
        {
            foreach (var user in UserReservationSlots)
            {
                user.User.Update(thisSlot);
            }
        }
    }

    #endregion
}
