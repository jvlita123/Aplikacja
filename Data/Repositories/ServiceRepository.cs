using Data.Entities;

namespace Data.Repositories
{
    public class ServiceRepository : BaseRepository<Entities.Service>
    {
        private readonly DataContext _dataContext;

        public ServiceRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
    }
}
