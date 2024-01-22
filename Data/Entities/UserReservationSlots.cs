using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public partial class UserReservationSlots
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ReservationSlotsId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        [ForeignKey("ReservationSlotsId")]
        public ReservationSlots? ReservationSlots { get; set; }

    }
}
