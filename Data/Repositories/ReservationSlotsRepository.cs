using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ReservationSlotsRepository : BaseRepository<ReservationSlots>
    {
        private DataContext _dataContext;

        public ReservationSlotsRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
    }
}
