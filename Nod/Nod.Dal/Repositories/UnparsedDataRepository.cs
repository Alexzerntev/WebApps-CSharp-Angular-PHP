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
    public class UnparsedDataRepository : IRepository<UnparsedData>
    {
        public DeviceMongoContext _context { get; set; }

        public UnparsedDataRepository(IOptions<Settings> settings)
        {
            _context = new DeviceMongoContext(settings);
        }

        public async Task<IEnumerable<UnparsedData>> GetAllAsync()
        {
            try
            {
                return await _context.UnparsedData.Find(_ => true).SortByDescending(x => x.ArrivalDateTime).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<IEnumerable<UnparsedData>> FindAllAsync(
            Expression<Func<UnparsedData, bool>> predicate)
        {
            try
            {
                return await _context.UnparsedData.Find(predicate).SortByDescending(x => x.ArrivalDateTime).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<IEnumerable<UnparsedData>> FindLimitedAsync(
            Expression<Func<UnparsedData, bool>> predicate, int size)
        {
            try
            {
                return await _context.UnparsedData.Find(predicate).SortByDescending(x => x.ArrivalDateTime).Limit(size).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<IEnumerable<UnparsedData>> FindPaginatedAsync(
            Expression<Func<UnparsedData, bool>> predicate, int pageSize, int startPage)
        {
            try
            {
                return await _context.UnparsedData.Find(predicate).SortByDescending(x => x.ArrivalDateTime).Skip(startPage).Limit(pageSize).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<UnparsedData> GetOneAsync(string id)
        {
            var filter = Builders<UnparsedData>.Filter.Eq("Id", id);

            try
            {
                return await _context.UnparsedData
                                .Find(filter)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<UnparsedData> AddOneAsync(UnparsedData item)
        {
            try
            {
                await _context.UnparsedData.InsertOneAsync(item);
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
                DeleteResult actionResult = await _context.UnparsedData.DeleteOneAsync(
                        Builders<UnparsedData>.Filter.Eq("Id", id));

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> UpdateOneAsync(string id, UnparsedData item)
        {
            try
            {
                ReplaceOneResult actionResult
                    = await _context.UnparsedData
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
                    = await _context.UnparsedData.DeleteManyAsync(new BsonDocument());

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
