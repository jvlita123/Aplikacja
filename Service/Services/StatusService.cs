using Data.Entities;
using Data.Repositories;

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

        public Status GetByName(string name)
        {
            Status status = _statusRepository.GetAll().Where(x => x.Name == name).FirstOrDefault();

            return status;
        }
    }
}
