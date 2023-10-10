using Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repositories
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
