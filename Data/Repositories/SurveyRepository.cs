using Data.Entities;

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
