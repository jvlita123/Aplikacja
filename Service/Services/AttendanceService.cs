using Data.Entities;
using Data.Repositories;

namespace Service.Services
{
    public class AttendanceService
    {
        private readonly AttendanceRepository _attendanceRepository;
        public AttendanceService(AttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        public List<Attendance> GetAll()
        {
            List<Attendance> attendances = _attendanceRepository.GetAll().ToList();

            return attendances;
        }
    }
}
