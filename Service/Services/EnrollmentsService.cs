using Data.Entities;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class EnrollmentsService
    {
        private readonly EnrollmentsRepository _enrollmentsRepository;
        public EnrollmentsService(EnrollmentsRepository enrollmentsRepository)
        {
            _enrollmentsRepository = enrollmentsRepository;
        }

        public List<Enrollment> GetAll()
        {
            List<Enrollment> enrollments = _enrollmentsRepository.GetAll().ToList();

            return enrollments;
        }
    }
}
