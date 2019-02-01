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
    public class ResetsRepository : IRepository<Reset>
    {
        public DeviceMongoContext _context { get; set; }

        public ResetsRepository(IOptions<Settings> settings)
        {
            _context = new DeviceMongoContext(settings);
        }

        public async Task<IEnumerable<Reset>> GetAllAsync()
        {
            try
            {
                return await _context.Resets.Find(_ => true).SortByDescending(x => x.DateTime).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<IEnumerable<Reset>> FindAllAsync(
            Expression<Func<Reset, bool>> predicate)
        {
            try
            {
                return await _context.Resets.Find(predicate).SortByDescending(x => x.DateTime).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<IEnumerable<Reset>> FindLimitedAsync(
            Expression<Func<Reset, bool>> predicate,int size)
        {
            try
            {
                return await _context.Resets.Find(predicate).SortByDescending(x => x.DateTime).Limit(size).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<IEnumerable<Reset>> FindPaginatedAsync(
            Expression<Func<Reset, bool>> predicate, int pageSize, int startPage)
        {
            try
            {
                return await _context.Resets.Find(predicate).SortByDescending(x => x.DateTime).Skip(startPage).Limit(pageSize).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<Reset> GetOneAsync(string id)
        {
            var filter = Builders<Reset>.Filter.Eq("Id", id);

            try
            {
                return await _context.Resets
                                .Find(filter)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<Reset> AddOneAsync(Reset item)
        {
            try
            {
                await _context.Resets.InsertOneAsync(item);
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
                DeleteResult actionResult = await _context.Resets.DeleteOneAsync(
                        Builders<Reset>.Filter.Eq("Id", id));

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> UpdateOneAsync(string id, Reset item)
        {
            try
            {
                ReplaceOneResult actionResult
                    = await _context.Resets
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
                    = await _context.Resets.DeleteManyAsync(new BsonDocument());

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
