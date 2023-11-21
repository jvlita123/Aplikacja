using Data.Entities;
using Data.Repositories;

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
