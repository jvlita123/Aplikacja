using Data.Entities;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Service.Services
{
    public class CoursesService
    {
        private readonly CoursesRepository _coursesRepository;
        private readonly EnrollmentsRepository _enrollmentRepository;
        private readonly CyclesRepository _cyclesRepository;

        public CoursesService(CoursesRepository coursesRepository, EnrollmentsRepository enrollmentRepository, CyclesRepository cyclesRepository)
        {
            _coursesRepository = coursesRepository;
            _enrollmentRepository = enrollmentRepository;
            _cyclesRepository = cyclesRepository;
        }

        public List<Course> GetAll()
        {
            List<Course> courses = _coursesRepository.GetAll().Include(x => x.Cycles).ToList();

            return courses;
        }

        public Course GetById(int id)
        {
            Course course = _coursesRepository.GetAll().Include(x => x.Cycles).Where(x => x.Id == id).ToList().FirstOrDefault();

            return course;
        }

        public Course NewCourse(Course course)
        {
            Course newCourse = _coursesRepository.AddAndSaveChanges(course);

            return newCourse;
        }

        public void RemoveCourseById(int id)
        {
            _cyclesRepository.RemoveRangeAndSaveChanges(_cyclesRepository.GetAll().Where(x => x.CourseId == id).ToList());
            _coursesRepository.RemoveById(id);
            _coursesRepository.SaveChanges();
        }

        public List<Course> GetUserCourses(int id)
        {
            List<Course> courses = _coursesRepository.GetAll().Include(x => x.Cycles).Include(x => x.Enrollments).ThenInclude(x => x.User).Where(x => x.Enrollments.Where(x => x.UserId == id).Count() > 0).ToList();
            foreach (var v in courses)
            {
                v.Cycles = GetById(v.Id).Cycles;
            }
            return courses;
        }

        public List<User> GetCourseUsers(int courseId)
        {
            List<User> users = _enrollmentRepository
                .GetAll()
                .Where(x => x.CourseId == courseId)
                .Select(x => new User
                {
                    Id = x.User.Id,
                    Email = x.User.Email,
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    Photos = x.User.Photos
                })
                .ToList();
            return users;
        }
    }
}
