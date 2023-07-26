using Data.Entities;

namespace Data.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        private DataContext _dataContext;

        public UserRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }

        public string GetUserNameByAccountId(int accountId)
        {
            var result = _dataContext.User.Where(u => u.Id == accountId).Select(u => u.Email).FirstOrDefault();

            return result;
        }
    }
}