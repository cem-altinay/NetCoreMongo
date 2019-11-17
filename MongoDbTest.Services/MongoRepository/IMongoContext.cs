using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDbTest.Services.MongoRepository
{
    public interface IMongoContext
    {
        System.Threading.Tasks.Task AddCommand(Func<System.Threading.Tasks.Task> func);
        void Dispose();
        MongoDB.Driver.IMongoCollection<T> GetCollection<T>(string name);
        int SaveChanges();
    }
}
