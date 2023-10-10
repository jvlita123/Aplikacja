using Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repositories
{
    public class AnswerOptionRepository : BaseRepository<AnswerOption>
    {
        private DataContext _dataContext;

        public AnswerOptionRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
    }
}
