using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class RoleRepository : BaseRepository<Role>
    {
        private DataContext _dataContext;

        public RoleRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
    }
}
