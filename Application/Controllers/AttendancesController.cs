using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services;

namespace Application.Controllers
{
    public class AttendancesController : Controller
    {
        private UserService _userService;
        private AttendanceService _attendanceService;
        private EnrollmentsService _enrollmentService;  
        private CoursesService _coursesService;

        public AttendancesController(UserService userService, AttendanceService attendanceService, EnrollmentsService enrollmentsService, CoursesService coursesService)
        {
            _userService = userService;
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

        public bool CheckAttendance(int userId, int cycleId, int courseId)
        {
            if (_attendanceService.GetAll().Where(x => x.CycleId == cycleId && x.CourseId == courseId && x.UserId == userId).FirstOrDefault() == null)
            {
                return false;
            }
            return true;
        }

        public PartialViewResult GetAttendancesForCycle(int id,int courseId)
        {
            var enrolledUsers = _enrollmentService.GetEnrolledUserByCourse(courseId);
            var cycles = _coursesService.GetById(courseId).Cycles.OrderBy(x => x.StartDate);
            var course = _coursesService.GetById(courseId);

            ViewData["cycles"] = cycles;
            ViewData["enrolledUsers"] = enrolledUsers;
            ViewData["course"] = course;
            if(_attendanceService.GetAll().Where(x => x.CycleId == id).Count() <= 0)
            {
                return PartialView(new List<Attendance>());  
            }
            
            return PartialView(_attendanceService.GetAll().Where(x => x.CycleId == id));
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public PartialViewResult AddAttendance()
        {
            return PartialView();
        }

        [HttpPost]
        public void AddAttendance(int courseId, int cycleId, int[] selectedUsers)
        {
            _attendanceService.RemoveAttendanceForCycle(cycleId);

            foreach(var v in selectedUsers)
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

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
