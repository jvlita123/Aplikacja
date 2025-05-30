﻿using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        private readonly DataContext _dataContext;

        public UserRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }

        public string GetUserNameByAccountId(int accountId)
        {
            var result = _dataContext.User.Where(u => u.Id == accountId).Select(u => u.Email).FirstOrDefault();

            return result;
        }

        public User GetUserByEmail(string email)
        {
            User user = _dataContext.User.Include(x => x.Messages).Where(u => u.Email == email).FirstOrDefault();

            return user;
        }
    }
}