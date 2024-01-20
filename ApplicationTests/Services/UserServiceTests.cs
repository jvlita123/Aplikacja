using Xunit;
using Moq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Collections.Generic;
using System.Linq;
using Data.Entities;
using Data.Dto_s;
using Data.Repositories;
using System.Security.Claims;
using System;
using Service.Services;
using Microsoft.AspNetCore.Identity;

namespace Tests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public void GetAll_ReturnsListOfUsers()
        {
            // Arrange
            var mockUserRepository = new Mock<UserRepository>();
            var mockPasswordHasher = new Mock<IPasswordHasher<User>>();
            var mockRoleRepository = new Mock<RoleRepository>();
            var mockPhotoRepository = new Mock<PhotoRepository>();
            var mockMessageRepository = new Mock<MessageRepository>();
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            var userService = new UserService(
                mockHttpContextAccessor.Object,
                mockUserRepository.Object,
                mockPasswordHasher.Object,
                mockRoleRepository.Object,
                mockPhotoRepository.Object,
                mockMessageRepository.Object
            );

            var mockUsers = new List<User>
        {
            new User { Id = 1, Email = "user1@example.com" },
            new User { Id = 2, Email = "user2@example.com" },
        }.AsQueryable();

            mockUserRepository.Setup(repo => repo.GetAll()).Returns(mockUsers);

            // Act
            var result = userService.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<User>>(result);
            Assert.Equal(2, result.Count);
        }

    
            [Fact]
            public void GetById_ValidId_ReturnsUser()
            {
                // Arrange
                var userId = 1;

                // Create a mock of UserRepository using a MockBehavior of Lax
                // Lax allows the mocking of classes with constructors
                var mockUserRepository = new Mock<UserRepository>(MockBehavior.Loose);

                var mockPasswordHasher = new Mock<IPasswordHasher<User>>();
                var mockRoleRepository = new Mock<RoleRepository>();
                var mockPhotoRepository = new Mock<PhotoRepository>();
                var mockMessageRepository = new Mock<MessageRepository>();
                var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

                var userService = new UserService(
                    mockHttpContextAccessor.Object,
                    mockUserRepository.Object,
                    mockPasswordHasher.Object,
                    mockRoleRepository.Object,
                    mockPhotoRepository.Object,
                    mockMessageRepository.Object
                );

                var mockUser = new User { Id = userId, Email = "user@example.com" };

                mockUserRepository.Setup(repo => repo.GetAll()).Returns(new[] { mockUser }.AsQueryable());

                // Act
                var result = userService.GetById(userId);

                // Assert
                Assert.NotNull(result);
                Assert.IsType<User>(result);
                Assert.Equal(userId, result.Id);
            }
            // Pozostałe testy jednostkowe dla pozostałych metod klasy UserService 
            [Fact]
        public void CheckProfile_WhenPathIsNullOrEmpty_ShouldReturnDefaultPath()
        {
            // Arrange
            var userRepositoryMock = new Mock<UserRepository>();
            var passwordHasherMock = new Mock<IPasswordHasher<User>>();
            var roleRepositoryMock = new Mock<RoleRepository>();
            var photoRepositoryMock = new Mock<PhotoRepository>();
            var messageRepositoryMock = new Mock<MessageRepository>();
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            var userService = new UserService(httpContextAccessorMock.Object, userRepositoryMock.Object, passwordHasherMock.Object, roleRepositoryMock.Object, photoRepositoryMock.Object, messageRepositoryMock.Object);

            var users = new List<User>
            {
                new User { Id = 1, Email = "user1@example.com", RoleId = 2 },
                new User { Id = 2, Email = "user2@example.com", RoleId = 2 },
            };
        }
        [Fact]
        public void CheckProfile_WhenPathIsNotNullOrEmpty_ShouldReturnPath()
        {
            // Arrange
            var userRepositoryMock = new Mock<UserRepository>(MockBehavior.Strict);
            var passwordHasherMock = new Mock<IPasswordHasher<User>>();
            var roleRepositoryMock = new Mock<RoleRepository>();
            var photoRepositoryMock = new Mock<PhotoRepository>();
            var messageRepositoryMock = new Mock<MessageRepository>();
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            // Mock the method used in CheckProfile, in this case, you're mocking GetUserByEmail
            userRepositoryMock.Setup(repo => repo.GetUserByEmail(It.IsAny<string>()))
                             .Returns(new User()); // Adjust the return value as needed

            var userService = new UserService(httpContextAccessorMock.Object, userRepositoryMock.Object, passwordHasherMock.Object, roleRepositoryMock.Object, photoRepositoryMock.Object, messageRepositoryMock.Object);

            var path = "profile.jpg";

            // Act
            var result = userService.CheckProfile(path);

            // Assert
            Assert.Equal(path, result);
        }

        [Fact]
        public void MyUser_WhenUserWithEmailExists_ShouldReturnMyUserDto()
        {
            // Arrange
            var userMock = new Mock<User>();
            userMock.SetupGet(u => u.Id).Returns(1);
            userMock.SetupGet(u => u.FirstName).Returns("John");
            userMock.SetupGet(u => u.LastName).Returns("Doe");
            userMock.SetupGet(u => u.PhoneNumber).Returns("123456789");
            userMock.SetupGet(u => u.DateOfBirth).Returns(DateTime.Parse("1990-01-01"));
            userMock.SetupGet(u => u.Email).Returns("john.doe@example.com");
            userMock.SetupGet(u => u.Role).Returns(new Role { Name = "User" });
            userMock.Setup(u => u.Photos).Returns(new List<Photo> { new Photo { Path = "profile.jpg", IsProfilePicture = true } });

            var userRepositoryMock = new Mock<UserRepository>(MockBehavior.Strict);
            userRepositoryMock.Setup(r => r.GetAll()).Returns(new List<User> { userMock.Object }.AsQueryable());
            var passwordHasherMock = new Mock<IPasswordHasher<User>>();
            var roleRepositoryMock = new Mock<RoleRepository>(MockBehavior.Strict);
            var photoRepositoryMock = new Mock<PhotoRepository>(MockBehavior.Strict);
            var messageRepositoryMock = new Mock<MessageRepository>(MockBehavior.Strict);
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>(MockBehavior.Strict);

            var userService = new UserService(httpContextAccessorMock.Object, userRepositoryMock.Object, passwordHasherMock.Object, roleRepositoryMock.Object, photoRepositoryMock.Object, messageRepositoryMock.Object);

            // Act
            var result = userService.MyUser("john.doe@example.com");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("John", result.FirstName);
            Assert.Equal("Doe", result.LastName);
            Assert.Equal("123456789", result.PhoneNumber);
            Assert.Equal(DateTime.Parse("1990-01-01"), result.DateOfBirth);
            Assert.Equal("john.doe@example.com", result.Email);
            Assert.Equal("User", result.RoleName);
            Assert.Equal("profile.jpg", result.ProfilePhoto);
        }

        [Fact]
        public void MyUser_WhenUserWithEmailDoesNotExist_ShouldReturnNull()
        {
            // Arrange
            var userRepositoryMock = new Mock<UserRepository>();
            userRepositoryMock.Setup(r => r.GetAll()).Returns(new List<User>().AsQueryable());

            // Arrange
            var passwordHasherMock = new Mock<IPasswordHasher<User>>();
            var roleRepositoryMock = new Mock<RoleRepository>();
            var photoRepositoryMock = new Mock<PhotoRepository>();
            var messageRepositoryMock = new Mock<MessageRepository>();
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            var userService = new UserService(httpContextAccessorMock.Object, userRepositoryMock.Object, passwordHasherMock.Object, roleRepositoryMock.Object, photoRepositoryMock.Object, messageRepositoryMock.Object);

            // Act
            var result = userService.MyUser("nonexistent@example.com");

            // Assert
            Assert.Null(result);
        }

        [Fact()]
        public void GetAllTest()
        {
            throw new NotImplementedException();
        }
    }
}
