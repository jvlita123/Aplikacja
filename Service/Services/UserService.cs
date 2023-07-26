using Data.Entities;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
namespace Service.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<User> GetAll()
        {
            List<User> users = _userRepository.GetAll().Include(x => x.Role).ToList();

            return users;
        }
        public User Add(User user)
        {
            User? newUser = _userRepository.AddAndSaveChanges(user);

            return newUser;
        }
    }
}
