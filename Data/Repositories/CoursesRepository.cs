using Data.Entities;

namespace Data.Repositories
{
    public class CoursesRepository : BaseRepository<Course>
    {
        private readonly DataContext _dataContext;

        public CoursesRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
    }
}
