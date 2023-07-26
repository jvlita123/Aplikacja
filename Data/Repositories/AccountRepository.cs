using Data.Entities;
using Data;

namespace Data.Repositories
{
    public class AccountRepository : BaseRepository<Account>
	{
		private DataContext _dataContext;

		public AccountRepository(DataContext context) : base(context)
		{
			_dataContext = context;
		}
	}
}

