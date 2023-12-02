using Data.Entities;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Service.Services
{
    public class CoursesService
    {
        private readonly CoursesRepository _coursesRepository;
        private readonly EnrollmentsRepository _enrollmentRepository;

        public CoursesService(CoursesRepository coursesRepository, EnrollmentsRepository enrollmentRepository)
        {
            _coursesRepository = coursesRepository;
            _enrollmentRepository = enrollmentRepository;
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
    }
}
