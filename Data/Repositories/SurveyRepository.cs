using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class SurveyRepository : BaseRepository<Survey>
    {
        private DataContext _dataContext;

        public SurveyRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
    }
}
