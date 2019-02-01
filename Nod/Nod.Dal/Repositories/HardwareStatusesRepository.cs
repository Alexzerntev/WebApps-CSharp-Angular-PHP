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
    public class HardwareStatusesRepository : IRepository<HardwareStatus>
    {
        public DeviceMongoContext _context { get; set; }

        public HardwareStatusesRepository(IOptions<Settings> settings)
        {
            _context = new DeviceMongoContext(settings);
        }

        public async Task<IEnumerable<HardwareStatus>> GetAllAsync()
        {
            try
            {
                return await _context.HardwareStatuses.Find(_ => true).SortByDescending(x => x.DateTime).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<IEnumerable<HardwareStatus>> FindAllAsync(
            Expression<Func<HardwareStatus, bool>> predicate)
        {
            try
            {
                return await _context.HardwareStatuses.Find(predicate).SortByDescending(x => x.DateTime).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
        public async Task<IEnumerable<HardwareStatus>> FindLimitedAsync(
            Expression<Func<HardwareStatus, bool>> predicate, int size)
        {
            try
            {
                return await _context.HardwareStatuses.Find(predicate).SortByDescending(x => x.DateTime).Limit(size).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<IEnumerable<HardwareStatus>> FindPaginatedAsync(
            Expression<Func<HardwareStatus, bool>> predicate, int pageSize, int startPage)
        {
            try
            {
                return await _context.HardwareStatuses.Find(predicate).SortByDescending(x => x.DateTime).Skip(startPage).Limit(pageSize).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<HardwareStatus> GetOneAsync(string id)
        {
            var filter = Builders<HardwareStatus>.Filter.Eq("Id", id);

            try
            {
                return await _context.HardwareStatuses
                                .Find(filter)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<HardwareStatus> AddOneAsync(HardwareStatus item)
        {
            try
            {
                await _context.HardwareStatuses.InsertOneAsync(item);
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
                DeleteResult actionResult = await _context.HardwareStatuses.DeleteOneAsync(
                        Builders<HardwareStatus>.Filter.Eq("Id", id));

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> UpdateOneAsync(string id, HardwareStatus item)
        {
            try
            {
                ReplaceOneResult actionResult
                    = await _context.HardwareStatuses
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
                    = await _context.HardwareStatuses.DeleteManyAsync(new BsonDocument());

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
