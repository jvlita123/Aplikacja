using Data.Dto_s;
using Data.Entities;
using Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Service.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(UserRepository userRepository, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
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
                    Password = userTmp.PasswordHash,
                    RoleId = userTmp.RoleId,
                };
                usersDto.Add(user);
            }

            return usersDto;
        }

        public User RegisterUserDto(RegisterUserDto dto)
        {
            var newUser = new User()
            {
                Email = dto.Email,
                RoleId = dto.RoleId
            };
            var hashedPassword =  _passwordHasher.HashPassword(newUser, dto.Password);
            
            newUser.PasswordHash = hashedPassword;
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
