using Data.Entities;

namespace Data.Repositories
{
    public class ReservationRepository : BaseRepository<Reservation>
    {
        private readonly DataContext _dataContext;

        public ReservationRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
    }
}
