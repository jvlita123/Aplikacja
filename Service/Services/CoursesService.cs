using Data.Entities;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class CoursesService
    {
        private readonly CoursesRepository _coursesRepository;
        public CoursesService(CoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }

        public List<Course> GetAll()
        {
            List<Course> courses = _coursesRepository.GetAll().ToList();

            return courses;
        }

        public Course Add(Course course)
        {
            Course newCourse = _coursesRepository.AddAndSaveChanges(course);

            return newCourse;
        }
    }
}
