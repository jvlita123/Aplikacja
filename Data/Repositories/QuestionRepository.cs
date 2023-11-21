using Data.Entities;

namespace Data.Repositories
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
