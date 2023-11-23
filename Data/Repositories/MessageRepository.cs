using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class MessageRepository : BaseRepository<Message>
    {
        private DataContext _dataContext;

        public MessageRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
    }
}
