using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using MongoDbTest.Services.MongoSettings;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDbTest.Services.MongoRepository
{
    public class MongoContext : IMongoContext
    {
        private IMongoDatabase Database { get; set; }
        private readonly List<Func<Task>> _commands;
        public MongoContext(IOptions<Settings> settings)
        {
            // Set Guid to CSharp style (with dash -)
            BsonDefaults.GuidRepresentation = GuidRepresentation.CSharpLegacy;

            // Every command will be stored and it'll be processed at SaveChanges
            _commands = new List<Func<Task>>();

            RegisterConventions();

            // Configure mongo (You can inject the config, just to simplify)
            var mongoClient = new MongoClient(settings.Value.ConnectionString);

            Database = mongoClient.GetDatabase(settings.Value.Database);
        }

        private void RegisterConventions()
        {
            var pack = new ConventionPack
{
new IgnoreExtraElementsConvention(true),
new IgnoreIfDefaultConvention(true)
};
            ConventionRegistry.Register("My Solution Conventions", pack, t => true);
        }

        public int SaveChanges()
        {
            var qtd = _commands.Count;
            foreach (var command in _commands)
            {
                command();
            }

            _commands.Clear();
            return qtd;
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return Database.GetCollection<T>(name);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Task AddCommand(Func<Task> func)
        {
            _commands.Add(func);
            return Task.CompletedTask;
        }
    }
}
