using Application.Data;
using Application.Entities;

namespace Application.Repositories
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

