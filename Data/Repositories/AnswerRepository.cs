using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class AnswerRepository : BaseRepository<Answer>
    {
        private DataContext _dataContext;

        public AnswerRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
    }
}
