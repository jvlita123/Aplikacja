using Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repositories
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
