using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Nod.Dal.Interfaces;
using Nod.Model;
using Nod.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nod.Dal.Repositories
{
    public class DeviceRepository : IRepository<Device>
    {
        public DeviceMongoContext _context { get; set; }

        public DeviceRepository(IOptions<Settings> settings)
        {
            _context = new DeviceMongoContext(settings);
        }

        public async Task<IEnumerable<Device>> GetAllAsync()
        {
            try
            {
                return await _context.Devices.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<IEnumerable<Device>> FindAllAsync(
            Expression<Func<Device, bool>> predicate)
        {
            try
            {
                return await _context.Devices.Find(predicate).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<IEnumerable<Device>> FindLimitedAsync(
            Expression<Func<Device, bool>> predicate, int size)
        {
            try
            {
                return await _context.Devices.Find(predicate).Limit(size).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<IEnumerable<Device>> FindPaginatedAsync(
            Expression<Func<Device, bool>> predicate, int pageSize, int startPage)
        {
            try
            {
                return await _context.Devices.Find(predicate).Skip(startPage).Limit(pageSize).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<Device> GetOneAsync(string id)
        {
            var filter = Builders<Device>.Filter.Eq("Id", id);

            try
            {
                return await _context.Devices
                                .Find(filter)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<Device> AddOneAsync(Device item)
        {
            try
            {
                await _context.Devices.InsertOneAsync(item);
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
                DeleteResult actionResult = await _context.Devices.DeleteOneAsync(
                        Builders<Device>.Filter.Eq("Id", id));

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> UpdateOneAsync(string id, Device item)
        {
            try
            {
                ReplaceOneResult actionResult
                    = await _context.Devices
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
                    = await _context.Devices.DeleteManyAsync(new BsonDocument());

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
