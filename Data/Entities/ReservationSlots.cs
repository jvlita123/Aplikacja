using Data.Patterns;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
            if (value == true)
            {
                _isAvailable = value;
                Notify(); 
            }
            else
            {
             _isAvailable = value;
                Notify();
            }
        }
    }

    [ForeignKey("ServiceId")]
    public virtual Service Service { get; set; } = null!;

    [ForeignKey("ReservationId")]
    public virtual Reservation1? Reservation { get; set; }

    public virtual ObservableCollection<UserReservationSlots> UserReservationSlots { get; set; } = new ObservableCollection<UserReservationSlots>();
    
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
        foreach (var user in UserReservationSlots)
        {
            user.User.Update(thisSlot); 
        }
    }

    #endregion
}
