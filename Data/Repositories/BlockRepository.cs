using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class BlockRepository : BaseRepository<Block>
    {
        private DataContext _dataContext;

        public BlockRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
    }
}
