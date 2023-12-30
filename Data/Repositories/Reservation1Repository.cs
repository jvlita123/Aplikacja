using Data.Entities;

namespace Data.Repositories
{
    public class Reservation1Repository : BaseRepository<Reservation1>
    {
        private readonly DataContext _dataContext;

        public Reservation1Repository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
    }
}
