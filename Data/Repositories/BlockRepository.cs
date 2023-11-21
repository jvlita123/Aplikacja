using Data.Entities;

namespace Data.Repositories
{
    public class BlockRepository : BaseRepository<Block>
    {
        private DataContext _dataContext;

        public BlockRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
    }
}
