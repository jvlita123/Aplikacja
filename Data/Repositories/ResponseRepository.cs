using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ResponseRepository : BaseRepository<Response>
    {
        private DataContext _dataContext;

        public ResponseRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
    }
}
