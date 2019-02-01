using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using Nod.Dal.Interfaces;
using Nod.Model;
using Nod.Model.Entities;
using Nod.Model.Entities.DeviceEntries;
using Nod.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Nod.Dal
{
    public class DeviceMongoContext
    {
        private readonly IMongoDatabase _database = null;

        public DeviceMongoContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<ConnectionAttribute> ConnectionAttributes
        {
            get
            {
                return _database.GetCollection<ConnectionAttribute>("connectionAttributes");
            }
        }
        public IMongoCollection<Gps> Gpses
        {
            get
            {
                return _database.GetCollection<Gps>("gpses");
            }
        }
        public IMongoCollection<HardwareStatus> HardwareStatuses
        {
            get
            {
                return _database.GetCollection<HardwareStatus>("hardwareStatuses");
            }
        }
        public IMongoCollection<Reset> Resets
        {
            get
            {
                return _database.GetCollection<Reset>("resets");
            }
        }
        public IMongoCollection<UnparsedData> UnparsedData
        {
            get
            {
                return _database.GetCollection<UnparsedData>("unparsedData");
            }
        }
        public IMongoCollection<Device> Devices
        {
            get
            {
                return _database.GetCollection<Device>("devices");
            }
        }
    }
}