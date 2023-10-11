using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ServiceRepository : BaseRepository<Entities.Service>
    {
        private DataContext _dataContext;

        public ServiceRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
    }
}
