using Data.Dto_s;
using Data.Entities;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
        public List<RegisterUserDto?> GetAllDto()
        {
            List<User> users = _userRepository.GetAll().Include(x=>x.Role).ToList();
            List<RegisterUserDto> usersDto = new List<RegisterUserDto>();

            foreach (var v in users)
            {
                User userTmp = _userRepository.GetById(v.Id);
                RegisterUserDto? user = new RegisterUserDto()
                {
                    Email = userTmp.Email,
                    PasswordHash = userTmp.PasswordHash,
                    RoleId = userTmp.RoleId,
                };
                usersDto.Add(user);
            }

            return usersDto;
        }

        public User Add(User user)
        {
            User? newUser = _userRepository.AddAndSaveChanges(user);

            return newUser;
        }
        public User RegisterUserDto(RegisterUserDto dto)
        {
            var newUser = new User()
            {
                Email = dto.Email,
                PasswordHash = dto.PasswordHash,
                RoleId = dto.RoleId
            };

            _userRepository.AddAndSaveChanges(newUser);

            return newUser;
        }

        public Boolean LoginUser(string email, string password)
        {
            User userToLogin = _userRepository.GetUserByEmail(email);

            if (userToLogin != null)
            {
                if(userToLogin.PasswordHash == password)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
