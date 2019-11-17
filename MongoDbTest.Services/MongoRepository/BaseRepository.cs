﻿using MongoDB.Bson;
using MongoDB.Driver;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbTest.Services.MongoRepository
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        protected readonly IMongoContext _context;
        protected readonly IMongoCollection<TEntity> DbSet;

        public BaseRepository(IMongoContext context)
        {
            _context = context;
            DbSet = _context.GetCollection<TEntity>(typeof(TEntity).Name);
        }
        public virtual Task Add(TEntity obj)
        {
            return _context.AddCommand(async () => await DbSet.InsertOneAsync(obj));
        }

        public virtual async Task<TEntity> GetById(ObjectId id)
        {
            var data = await DbSet.FindAsync(Builders<TEntity>.Filter.Eq(" _id ", id));
            return data.FirstOrDefault();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            var all = await DbSet.FindAsync(Builders<TEntity>.Filter.Empty);
            return all.ToList();
        }

        public virtual Task Update(TEntity obj)
        {
            return _context.AddCommand(async () =>
            {
                await DbSet.ReplaceOneAsync(Builders<TEntity>.Filter.Eq(" _id ", obj.GetId()), obj);
            });
        }

        public virtual Task Remove(Guid id) => _context.AddCommand(() => DbSet.DeleteOneAsync(Builders<TEntity>.Filter.Eq(" _id ", id)));

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
