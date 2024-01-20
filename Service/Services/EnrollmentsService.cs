using Data.Entities;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Service.Services
{
    public class EnrollmentsService
    {
        private readonly EnrollmentsRepository _enrollmentsRepository;
        private readonly UserRepository _userRepository;
        private readonly CyclesRepository _cyclespository;
        private readonly CoursesRepository _coursespository;
        public EnrollmentsService(EnrollmentsRepository enrollmentsRepository, UserRepository userRepository, CyclesRepository cyclesRepository, CoursesRepository coursespository)
        {
            _enrollmentsRepository = enrollmentsRepository;
            _coursespository = coursespository;
            _userRepository = userRepository;
            _cyclespository = cyclesRepository;
        }

        public List<Enrollment> GetAll()
        {
            List<Enrollment> enrollments = _enrollmentsRepository.GetAll().Include(x => x.Course).ThenInclude(x => x.Cycles).Include(x => x.User).ToList();
            return enrollments;
        }

        public Enrollment GetById(int id)
        {
            Enrollment enrollment = _enrollmentsRepository.GetById(id);

            return enrollment;
        }

        public List<Enrollment> GetByCourse(int id)
        {
            List<Enrollment> enrollments = _enrollmentsRepository.GetAll().Where(x => x.CourseId == id).ToList();

            return enrollments;
        }

        public List<User> GetEnrolledUserByCourse(int id)
        {
            List<User> users = _enrollmentsRepository.GetAll().Include(x => x.User).Include(x => x.User.Photos).Where(x => x.CourseId == id).Select(x => x.User).ToList();

            return users;
        }

        public List<Enrollment> GetUserEnrollments(User usr)
        {
            if (usr.Role.Name == "admin")
            {
                return GetAll();
            }
            else
            {
                return _enrollmentsRepository.GetAll().Include(x => x.Course).Include(x => x.User).Where(x => x.UserId == usr.Id).ToList();
            }
        }

        public bool IsEnrolled(int userId, int courseId)
        {
            if (_enrollmentsRepository.GetAll().Where(x => x.UserId == userId && x.CourseId == courseId).FirstOrDefault() != null)
            {
                return true;
            }

            return false;
        }

        public Enrollment NewEnrollment(Enrollment enrollment)
        {
            Enrollment newEnrollment = new()
            {
                UserId = enrollment.UserId,
                CourseId = enrollment.CourseId,
                Cancelled = enrollment.Cancelled,
                CancellationReason = enrollment.CancellationReason,
                EnrollmentDate = enrollment.EnrollmentDate,
            };

            _enrollmentsRepository.AddAndSaveChanges(newEnrollment);

            return newEnrollment;
        }
    }
}
