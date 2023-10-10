using Service.Entities;
using Service.Repositories;

namespace Service.Services
{
    public class CoursesPerCycleService
    {
        private readonly CoursesPerCycleRepository _coursesPerCycleRepository;
        public CoursesPerCycleService(CoursesPerCycleRepository coursesPerCycleRepository)
        {
            _coursesPerCycleRepository = coursesPerCycleRepository;
        }

        public List<CoursesPerCycle> GetAll()
        {
            List<CoursesPerCycle> coursesPerCycle = _coursesPerCycleRepository.GetAll().ToList();

            return coursesPerCycle;
        }
    }
}
