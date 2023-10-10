using Service.Entities;
using Service.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ResponseService
    {
        private readonly ResponseRepository _responseRepository;
        public ResponseService(ResponseRepository responseRepository)
        {
            _responseRepository = responseRepository;
        }

        public List<Response> GetAll()
        {
            List<Response> responses = _responseRepository.GetAll().ToList();

            return responses;
        }
    }
}
