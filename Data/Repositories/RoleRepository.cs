using Data.Entities;

namespace Data.Repositories
{
    public class RoleRepository : BaseRepository<Role>
    {
        private DataContext _dataContext;

        public RoleRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
    }
}
