using Data.Entities;

namespace Data.Repositories
{
    public class StatusRepository : BaseRepository<Status>
    {
        private readonly DataContext _dataContext;

        public StatusRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
    }
}
