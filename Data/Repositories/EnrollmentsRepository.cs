using Data.Entities;

namespace Data.Repositories
{
    public class EnrollmentsRepository : BaseRepository<Enrollment>
    {
        private readonly DataContext _dataContext;

        public EnrollmentsRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
    }
}
