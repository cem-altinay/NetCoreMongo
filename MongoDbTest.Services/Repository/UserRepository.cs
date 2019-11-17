using MongoDbTest.Services.Model;
using MongoDbTest.Services.MongoRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDbTest.Services.Repository
{
    public interface IUserRepository : IRepository<Users>
    {
    }
    public class UserRepository : BaseRepository<Users>, IUserRepository
    {
        public UserRepository(IMongoContext context) : base(context)
        {
        }
    }
}
