using Data.Dto_s;
using Data.Entities;
using Data.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

namespace Service.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly RoleRepository _roleRepository;
        private readonly ReservationRepository _reservationRepository;
        private readonly PhotoRepository _photoRepository;
        private readonly MessageRepository _messageRepository;

        public UserService(UserRepository userRepository, IPasswordHasher<User> passwordHasher, ReservationRepository reservationRepository, RoleRepository roleRepository, PhotoRepository photoRepository, MessageRepository messageRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _roleRepository = roleRepository;
            _reservationRepository = reservationRepository;
            _photoRepository = photoRepository;
            _messageRepository = messageRepository;
        }

        public List<User> GetAll()
        {
            List<User> users = _userRepository.GetAll().Include(x => x.Role).Include(x => x.Reservations).ToList();

            return users;
        }

        public User GetById(int id)
        {
            User user = _userRepository.GetAll().Where(x=>x.Id==id).FirstOrDefault();

            return user;
        }

        public User GetByEmail(string email)
        {
            User user = _userRepository.GetAll().Include(x => x.Role).Include(x => x.Reservations).Where(x => x.Email == email).FirstOrDefault();

            return user;
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
                .Include(u => u.Photos)
                .Include(u => u.Role)
                .Include(u => u.Messages)
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

            string ProfilePhoto = "/"+userToLogin.Photos.Where(p => p.IsProfilePicture == true).Select(p => p.Path).FirstOrDefault();
            if(userToLogin.Photos.Where(p => p.IsProfilePicture == true).Select(p => p.Path).FirstOrDefault() == null)
            {
                ProfilePhoto = "/blang-user.png";
            }
            String newMessage = "false";
            if(_messageRepository.GetAll().Any(x => x.UserId2 == userToLogin.Id && x.IsNew == true) )
            {
                newMessage = "true";
            }
            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, userToLogin.Id.ToString()),
                        new Claim(ClaimTypes.Name, userToLogin.Email),//need update to Name
                        new Claim(ClaimTypes.Email, userToLogin.Email),
                        new Claim(ClaimTypes.Role, userToLogin.Role.Name.ToString()),
                        new Claim("ProfilePhotoPath", ProfilePhoto),
                        new Claim("newMessage", newMessage.ToString()),
                        new Claim("FullName", userToLogin.FirstName +" "+ userToLogin.LastName)
                    };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            return claimsIdentity;
        }
        public void UpdateUser(User user)
        {
            User userToSave = _userRepository.GetById(user.Id);
            userToSave.FirstName = user.FirstName;
            userToSave.LastName = user.LastName;
            userToSave.Email = user.Email;
            userToSave.PhoneNumber = user.PhoneNumber;
            userToSave.DateOfBirth = user.DateOfBirth;
            userToSave.RoleId = user.RoleId;
            userToSave.IsBlocked = user.IsBlocked;
            userToSave.PasswordHash = user.PasswordHash;
            userToSave.Role = user.Role;
    
            _userRepository.UpdateAndSaveChanges(userToSave);
            _userRepository.SaveChanges();
        }

        public MyUserDto? MyUser(string email)
        {
            var users = _userRepository.GetAll().ToList();
            User? user = _userRepository.GetAll()
                .Include(x => x.Photos)
                .Include(x => x.Role)
                .Include(x => x.Messages)
                .Where(x => x.Email == email)
                .FirstOrDefault();
            if (user == null)
            {
                return null;
            }

            MyUserDto myUserDto = new MyUserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                RoleName = user.Role.Name,


                ProfilePhoto = user.Photos.Where(p => p.IsProfilePicture==true).Select(p => p.Path).FirstOrDefault(),
                Photos = user.Photos.Where(p => p.IsProfilePicture == false).Select(p => p.Path).ToList(),
            };

            myUserDto.ProfilePhoto = CheckProfile(myUserDto.ProfilePhoto);

            return myUserDto;
        }

        private string CheckProfile(string? path)
        {
            if (String.IsNullOrEmpty(path))
            {
                path = "noProfile.jpg";
            }

            return path;
        }
    }
}
