using Data.Entities;
using Data.Repositories;

namespace Service.Services
{
    public class PhotoService
    {
        private readonly PhotoRepository _photoRepository;
        public PhotoService(PhotoRepository photoRepository)
        {
            _photoRepository = photoRepository;
        }

        public List<Photo> GetAll()
        {
            List<Photo> photos = _photoRepository.GetAll().ToList();

            return photos;
        }
    }
}