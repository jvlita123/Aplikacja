using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Options;

namespace Application.Data
{
	public class DataContext: DbContext
	{
	//	public DbSet<Account> Account { get; set; }
		public DataContext()
		{
		}

		public DataContext(DbContextOptions<DataContext> options)
			: base(options)
		{
		}

	}
}
