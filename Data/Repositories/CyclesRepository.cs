using Data.Entities;

namespace Data.Repositories
{
    public class CyclesRepository : BaseRepository<Cycle>
    {
        private readonly DataContext _dataContext;

        public CyclesRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
    }
}
