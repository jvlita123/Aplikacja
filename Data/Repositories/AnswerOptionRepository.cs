using Data.Entities;

namespace Data.Repositories
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
