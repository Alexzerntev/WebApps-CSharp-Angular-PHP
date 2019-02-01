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
    public class GpsRepository : IRepository<Gps>
    {
        public DeviceMongoContext _context { get; set; }

        public GpsRepository(IOptions<Settings> settings)
        {
            _context = new DeviceMongoContext(settings);
        }

        public async Task<IEnumerable<Gps>> GetAllAsync()
        {
            try
            {
                return await _context.Gpses.Find(_ => true).SortByDescending(x => x.DateTime).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<IEnumerable<Gps>> FindAllAsync(
            Expression<Func<Gps, bool>> predicate)
        {
            try
            {
                return await _context.Gpses.Find(predicate).SortByDescending(x => x.DateTime).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<IEnumerable<Gps>> FindLimitedAsync(
            Expression<Func<Gps, bool>> predicate, int size)
        {
            try
            {
                return await _context.Gpses.Find(predicate).SortByDescending(x => x.DateTime).Limit(size).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<IEnumerable<Gps>> FindPaginatedAsync(
            Expression<Func<Gps, bool>> predicate, int pageSize, int startPage)
        {
            try
            {
                return await _context.Gpses.Find(predicate).SortByDescending(x => x.DateTime).Skip(startPage).Limit(pageSize).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<Gps> GetOneAsync(string id)
        {
            var filter = Builders<Gps>.Filter.Eq("Id", id);

            try
            {
                return await _context.Gpses
                                .Find(filter)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<Gps> AddOneAsync(Gps item)
        {
            try
            {
                await _context.Gpses.InsertOneAsync(item);
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
                DeleteResult actionResult = await _context.Gpses.DeleteOneAsync(
                        Builders<Gps>.Filter.Eq("Id", id));

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> UpdateOneAsync(string id, Gps item)
        {
            try
            {
                ReplaceOneResult actionResult
                    = await _context.Gpses
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
                    = await _context.Gpses.DeleteManyAsync(new BsonDocument());

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
