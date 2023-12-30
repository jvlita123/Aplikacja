using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class UserReservationSlotsRepository : BaseRepository<UserReservationSlots>
    {
        private DataContext _dataContext;

        public UserReservationSlotsRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
    }
}
