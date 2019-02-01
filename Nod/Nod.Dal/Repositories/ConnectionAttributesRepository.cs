using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Nod.Dal.Interfaces;
using Nod.Model;
using Nod.Model.Entities.DeviceEntries;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nod.Dal.Repositories
{
    public class ConnectionAttributesRepository : IRepository<ConnectionAttribute>
    {
        public DeviceMongoContext _context { get; set; }

        public ConnectionAttributesRepository(IOptions<Settings> settings)
        {
            _context = new DeviceMongoContext(settings);
        }

        public async Task<IEnumerable<ConnectionAttribute>> GetAllAsync()
        {
            try
            {
                return await _context.ConnectionAttributes.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<IEnumerable<ConnectionAttribute>> FindAllAsync(
            Expression<Func<ConnectionAttribute, bool>> predicate)
        {
            try
            {
                return await _context.ConnectionAttributes.Find(predicate).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<IEnumerable<ConnectionAttribute>> FindLimitedAsync(
            Expression<Func<ConnectionAttribute, bool>> predicate, int size)
        {
            try
            {
                return await _context.ConnectionAttributes.Find(predicate).Limit(size).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<IEnumerable<ConnectionAttribute>> FindPaginatedAsync(
            Expression<Func<ConnectionAttribute, bool>> predicate, int pageSize, int startPage)
        {
            try
            {
                return await _context.ConnectionAttributes.Find(predicate).Skip(startPage).Limit(pageSize).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<ConnectionAttribute> GetOneAsync(string id)
        {
            var filter = Builders<ConnectionAttribute>.Filter.Eq("Id", id);

            try
            {
                return await _context.ConnectionAttributes
                                .Find(filter)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<ConnectionAttribute> AddOneAsync(ConnectionAttribute item)
        {
            try
            {
                await _context.ConnectionAttributes.InsertOneAsync(item);
                return item;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> RemoveOneAsync(string id)
        {
            try
            {
                DeleteResult actionResult = await _context.ConnectionAttributes.DeleteOneAsync(
                        Builders<ConnectionAttribute>.Filter.Eq("Id", id));

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> UpdateOneAsync(string id, ConnectionAttribute item)
        {
            try
            {
                ReplaceOneResult actionResult
                    = await _context.ConnectionAttributes
                                    .ReplaceOneAsync(n => n.Id.Equals(id)
                                            , item
                                            , new UpdateOptions { IsUpsert = true });
                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> RemoveAllAsync()
        {
            try
            {
                DeleteResult actionResult
                    = await _context.ConnectionAttributes.DeleteManyAsync(new BsonDocument());

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
    }
}
