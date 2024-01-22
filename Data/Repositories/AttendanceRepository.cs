using Data.Entities;

namespace Data.Repositories
{
    public class AttendanceRepository : BaseRepository<Attendance>
    {
        private readonly DataContext _dataContext;

        public AttendanceRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
    }
}
