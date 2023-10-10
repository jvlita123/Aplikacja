using Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repositories
{
    public class ReservationRepository : BaseRepository<Reservation>
    {
        private DataContext _dataContext;

        public ReservationRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
    }
}
