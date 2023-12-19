using Data.Entities;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Service.Services
{
    public class CoursesService
    {
        private readonly CoursesRepository _coursesRepository;
        private readonly EnrollmentsRepository _enrollmentRepository;
        private readonly PhotoRepository _photoRepository;

        public CoursesService(CoursesRepository coursesRepository, EnrollmentsRepository enrollmentRepository, PhotoRepository photoRepository)
        {
            _coursesRepository = coursesRepository;
            _enrollmentRepository = enrollmentRepository;
            _photoRepository = photoRepository;
        }

        public List<Course> GetAll()
        {
            List<Course> courses = _coursesRepository.GetAll().Include(x => x.Cycles).ToList();

            return courses;
        }

        public Course GetById(int id)
        {
            Course course = _coursesRepository.GetAll().Include(x => x.Cycles).Where(x=>x.Id==id).ToList().FirstOrDefault();

            return course;
        }

        public Course NewCourse(Course course)
        {
            Course newCourse = _coursesRepository.AddAndSaveChanges(course);

            return newCourse;
        }

        public List<Course> GetUserCourses(int id)
        {
            List<Course> courses = _enrollmentRepository.GetAll().Include(x=>x.User).Include(x=>x.Course).Select(x=>x.Course).Distinct().ToList();
            foreach(var v in courses)
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
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    Photos = _photoRepository
                        .GetAll()
                        .Where(photo => photo.UserId == x.User.Id && photo.IsProfilePicture == true)
                        .ToList()
                })
                .ToList();
            return users;
        }
    }
}
