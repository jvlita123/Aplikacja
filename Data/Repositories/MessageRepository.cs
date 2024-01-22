using Data.Entities;

namespace Data.Repositories
{
    public class MessageRepository : BaseRepository<Message>
    {
        private readonly DataContext _dataContext;

        public MessageRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
    }
}
