using Data.Entities;

namespace Data.Repositories
{
    public class AttendanceRepository : BaseRepository<Attendance>
    {
        private DataContext _dataContext;

        public AttendanceRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
    }
}
