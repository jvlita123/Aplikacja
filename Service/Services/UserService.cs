using Data.Dto_s;
using Data.Entities;
using Data.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Service.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly RoleRepository _roleRepository;
        private readonly ReservationRepository _reservationRepository;

        public UserService(UserRepository userRepository, IPasswordHasher<User> passwordHasher, ReservationRepository reservationRepository, RoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _roleRepository = roleRepository;
            _reservationRepository = reservationRepository;
        }

        public List<User> GetAll()
        {
            List<User> users = _userRepository.GetAll().Include(x => x.Role).Include(x => x.Reservations).ToList();

            return users;
        }

        public List<RegisterUserDto?> GetAllDto()
        {
            List<User> users = _userRepository.GetAll().Include(x => x.Role).ToList();
            List<RegisterUserDto> usersDto = new List<RegisterUserDto>();

            foreach (var v in users)
            {
                User userTmp = _userRepository.GetById(v.Id);
                RegisterUserDto? user = new RegisterUserDto()
                {
                    Email = userTmp.Email,
                    Password = userTmp.PasswordHash,
                    RoleId = (int)userTmp.RoleId,
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
                RoleId = dto.RoleId,
            };

            var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);

            newUser.PasswordHash = hashedPassword;
            _userRepository.AddAndSaveChanges(newUser);

            return newUser;
        }

        public ClaimsIdentity Login(LoginDto dto) //to be corrected
        {
            var userToLogin = _userRepository
                .GetAll()
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Email == dto.Email);

            if (userToLogin is null)
            {
                throw new Exception();//to be corrected
            }

            var result = _passwordHasher.VerifyHashedPassword(userToLogin, userToLogin.PasswordHash, dto.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                throw new Exception();//to be corrected
            }

            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, userToLogin.Id.ToString()),
                        new Claim(ClaimTypes.Name, userToLogin.Email),//need update to Name
                        new Claim(ClaimTypes.Email, userToLogin.Email),
                        new Claim(ClaimTypes.Role, userToLogin.Role.Name.ToString()),
                    };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            return claimsIdentity;
        }
    }
}
