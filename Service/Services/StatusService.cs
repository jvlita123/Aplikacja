using Data.Entities;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class StatusService
    {
        private readonly StatusRepository _statusRepository;
        public StatusService(StatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public List<Status> GetAll()
        {
            List<Status> attendances = _statusRepository.GetAll().ToList();

            return attendances;
        }
    }
}
