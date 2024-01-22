using Data.Entities;

namespace Data.Repositories
{
    public class ReservationSlotsRepository : BaseRepository<ReservationSlots>
    {
        private readonly DataContext _dataContext;

        public ReservationSlotsRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
    }
}
