using Data.Entities;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Service.Services
{
    public class PhotoService
    {
        private readonly PhotoRepository _photoRepository;
        private readonly UserRepository _userRepository;

        public PhotoService(PhotoRepository photoRepository, UserRepository userRepository)
        {
            _photoRepository = photoRepository;
            _userRepository = userRepository;
        }

        public List<Photo> GetAll()
        {
            List<Photo> photos = _photoRepository.GetAll().ToList();

            return photos;
        }

        public void AddPhoto(string path, int id)
        {
            Photo? photoToRemove = _photoRepository.GetAll().Include(x=>x.User)?.Where(x => x.UserId == id).FirstOrDefault();
            if (photoToRemove != null)
            {
                _photoRepository.RemoveById(photoToRemove.Id);
                _photoRepository.SaveChanges();

            }

            Photo photo = new Photo();
            photo.Path = path;
            photo.Date = DateTime.Now;
            photo.Description = "profilePhoto";
            photo.IsProfilePicture = true;
            photo.UserId = id;

            _photoRepository.AddAndSaveChanges(photo);
            _userRepository.UpdateAndSaveChanges(_userRepository.GetById(photo.Id));
        }

    }
}