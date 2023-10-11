using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class CyclesRepository : BaseRepository<Cycle>
    {
        private DataContext _dataContext;

        public CyclesRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
    }
}
