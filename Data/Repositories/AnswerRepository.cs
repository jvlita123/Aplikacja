using Data.Entities;

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
