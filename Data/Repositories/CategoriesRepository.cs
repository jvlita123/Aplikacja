using Data.Entities;

namespace Data.Repositories
{
    public class CategoriesRepository : BaseRepository<Category>
    {
        private DataContext _dataContext;

        public CategoriesRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
    }
}
