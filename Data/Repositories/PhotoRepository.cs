﻿using Data.Entities;

namespace Data.Repositories
{
    public class PhotoRepository : BaseRepository<Photo>
    {
        private readonly DataContext _dataContext;

        public PhotoRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
    }
}
