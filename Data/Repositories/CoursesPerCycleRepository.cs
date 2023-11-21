using Data.Entities;

namespace Data.Repositories
{
    public class CoursesPerCycleRepository : BaseRepository<CoursesPerCycle>
    {
        private DataContext _dataContext;

        public CoursesPerCycleRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
    }
}
