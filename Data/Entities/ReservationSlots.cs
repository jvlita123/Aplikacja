using Data.Patterns;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public partial class ReservationSlots : ISubject, INotifyPropertyChanged
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
            if (_isAvailable != value)
            {
                NotifyPropertyChanged(nameof(IsAvailable));
                Notify(); 
            }
                _isAvailable = value;
        }
    }

    [ForeignKey("ServiceId")]
    public virtual Service Service { get; set; } = null!;

    [ForeignKey("ReservationId")]
    public virtual Reservation1? Reservation { get; set; }

    public virtual ObservableCollection<UserReservationSlots> UserReservationSlots { get; set; } = new ObservableCollection<UserReservationSlots>();
    
    public List<IObserver> _observers = new List<IObserver>();
  
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void NotifyPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

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
        foreach (var user in UserReservationSlots)
        {
            user.User.Update(this); 
        }
    }

    #endregion
}
