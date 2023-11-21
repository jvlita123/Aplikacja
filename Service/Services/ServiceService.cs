using Data.Repositories;

namespace Service.Services
{
    public class ServiceService
    {
        private readonly ServiceRepository _serviceRepository;
        public ServiceService(ServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }
        public List<Data.Entities.Service> GetAll()
        {
            List<Data.Entities.Service> services = _serviceRepository.GetAll().ToList();

            return services;
        }
    }
}