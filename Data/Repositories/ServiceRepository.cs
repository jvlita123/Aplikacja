namespace Data.Repositories
{
    public class ServiceRepository : BaseRepository<Entities.Service>
    {
        private DataContext _dataContext;

        public ServiceRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
    }
}
