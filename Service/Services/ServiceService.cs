using Data.Repositories;
using Microsoft.EntityFrameworkCore;

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
            List<Data.Entities.Service> services = _serviceRepository.GetAll().Include(x => x.ReservationSlots).ToList();

            return services;
        }

        public Data.Entities.Service GetById(int id)
        {
            Data.Entities.Service service = _serviceRepository.GetAll().Where(x => x.Id == id).FirstOrDefault();

            return service;
        }

        public Data.Entities.Service AddService(Data.Entities.Service service)
        {
            Data.Entities.Service newService = new Data.Entities.Service
            {
                Name = service.Name,
                Description = service.Description,
                Reservations = service.Reservations,
                ServiceTime = service.ServiceTime
            };

            _serviceRepository.Add(newService);
            _serviceRepository.SaveChanges();

            return newService;
        }

        public Data.Entities.Service Edit(int id, Data.Entities.Service serviceUp)
        {
            Data.Entities.Service service = _serviceRepository.GetById(id);

            service.Id = serviceUp.Id;
            service.Name = serviceUp.Name;
            service.Description = serviceUp.Description;
            service.Reservations = serviceUp.Reservations;
            service.ServiceTime = serviceUp.ServiceTime;


            _serviceRepository.UpdateAndSaveChanges(service);
            _serviceRepository.SaveChanges();

            return service;
        }

        public void Remove(int id)
        {
            _serviceRepository.RemoveById(id);
            _serviceRepository.SaveChanges();
        }
    }
}