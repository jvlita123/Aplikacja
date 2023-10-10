using Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repositories
{
    public class QuestionRepository : BaseRepository<Question>
    {
        private DataContext _dataContext;

        public QuestionRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
    }
}
