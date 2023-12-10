using Data.Entities;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Service.Services
{
    public class AttendanceService
    {
        private readonly AttendanceRepository _attendanceRepository;
        private readonly CyclesRepository _cyclesRepository;
        public AttendanceService(AttendanceRepository attendanceRepository, CyclesRepository cyclesRepository)
        {
            _attendanceRepository = attendanceRepository;
            _cyclesRepository = cyclesRepository;
        }

        public List<Attendance> GetAll()
        {
            List<Attendance> attendances = _attendanceRepository.GetAll().Include(x=>x.Cycle).Include(x => x.User).Include(x => x.Course).ToList();

            return attendances;
        }

        public Attendance GetById(int id)
        {
            Attendance attendance = _attendanceRepository.GetAll().Include(x => x.Cycle).Include(x => x.User).Include(x => x.Course).Where(x => x.Id == id).FirstOrDefault();

            return attendance;
        }

        public Attendance addAttendance(Attendance attendance)
        {
            Attendance newAttendance = new Attendance();
            newAttendance.CourseId = attendance.CourseId;
            newAttendance.UserId = attendance.UserId;
            newAttendance.CycleId = attendance.CycleId;
            newAttendance.TimeArrive = attendance.TimeArrive;
            newAttendance.TimeLeave = attendance.TimeLeave;

            _attendanceRepository.AddAndSaveChanges(newAttendance);
            _attendanceRepository.SaveChanges();

            return attendance;
        }

        public void RemoveAttendanceForCycle(int cycleId)
        {
            List<Attendance> attendances = _attendanceRepository.GetAll().Include(x => x.Cycle).Include(x => x.User).Include(x => x.Course).Where(x => x.CycleId == cycleId).ToList();
            _attendanceRepository.RemoveRange(attendances);
            _attendanceRepository.SaveChanges();
        }
    }
}
