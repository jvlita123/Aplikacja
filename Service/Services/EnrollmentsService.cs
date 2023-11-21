using Data.Entities;
using Data.Repositories;

namespace Service.Services
{
    public class EnrollmentsService
    {
        private readonly EnrollmentsRepository _enrollmentsRepository;
        public EnrollmentsService(EnrollmentsRepository enrollmentsRepository)
        {
            _enrollmentsRepository = enrollmentsRepository;
        }

        public List<Enrollment> GetAll()
        {
            List<Enrollment> enrollments = _enrollmentsRepository.GetAll().ToList();

            return enrollments;
        }
    }
}
