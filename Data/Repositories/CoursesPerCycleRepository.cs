using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class CoursesPerCycleRepository: BaseRepository<CoursesPerCycle>
    {
        private DataContext _dataContext;

        public CoursesPerCycleRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
    }
}
