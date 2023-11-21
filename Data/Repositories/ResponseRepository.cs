using Data.Entities;

namespace Data.Repositories
{
    public class ResponseRepository : BaseRepository<Response>
    {
        private DataContext _dataContext;

        public ResponseRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
    }
}
