using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services;

namespace Application.Controllers
{
    public class AttendancesController : Controller
    {
        private readonly AttendanceService _attendanceService;
        private readonly EnrollmentsService _enrollmentService;
        private readonly CoursesService _coursesService;

        public AttendancesController(AttendanceService attendanceService, EnrollmentsService enrollmentsService, CoursesService coursesService)
        {
            _attendanceService = attendanceService;
            _enrollmentService = enrollmentsService;
            _coursesService = coursesService;
        }

        public ActionResult Index()
        {
            var courses = _coursesService.GetAll();

            ViewData["courses"] = courses;

            return View(_attendanceService.GetAll());
        }

        [Authorize(Roles = "admin")]
        public bool CheckAttendance(int userId, int cycleId, int courseId)
        {
            if (_attendanceService.GetAll().Where(x => x.CycleId == cycleId && x.CourseId == courseId && x.UserId == userId).FirstOrDefault() == null)
            {
                return false;
            }
            return true;
        }

        [Authorize]
        public PartialViewResult GetAttendancesForCycle(int id, int courseId)
        {
            var enrolledUsers = _enrollmentService.GetEnrolledUserByCourse(courseId);
            var cycles = _coursesService.GetById(courseId).Cycles.OrderBy(x => x.StartDate);
            var course = _coursesService.GetById(courseId);

            ViewData["cycles"] = cycles;
            ViewData["enrolledUsers"] = enrolledUsers;
            ViewData["course"] = course;

            if (!_attendanceService.GetAll().Where(x => x.CycleId == id).Any())
            {
                return PartialView(new List<Attendance>());
            }

            return PartialView(_attendanceService.GetAll().Where(x => x.CycleId == id));
        }

        [Authorize]
        public PartialViewResult AddAttendance()
        {
            return PartialView();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public void AddAttendance(int courseId, int cycleId, int[] selectedUsers)
        {
            _attendanceService.RemoveAttendanceForCycle(cycleId);

            foreach (var v in selectedUsers)
            {
                Attendance attendance = new Attendance();
                attendance.CourseId = courseId;
                attendance.CycleId = cycleId;
                attendance.UserId = v;
                attendance.TimeArrive = new TimeSpan(2, 14, 18);
                attendance.TimeLeave = new TimeSpan(2, 14, 18);

                _attendanceService.addAttendance(attendance);
            }
        }
    }
}
