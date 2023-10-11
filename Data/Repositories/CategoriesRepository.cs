using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class CategoriesRepository : BaseRepository<Category>
    {
        private DataContext _dataContext;

        public CategoriesRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
    }
}
