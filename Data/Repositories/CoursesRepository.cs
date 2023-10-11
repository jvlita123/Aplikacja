using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class CoursesRepository : BaseRepository<Course>
    {
        private DataContext _dataContext;

        public CoursesRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
    }
}
