using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class PhotoRepository : BaseRepository<Photo>
    {
        private DataContext _dataContext;

        public PhotoRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
    }
}
