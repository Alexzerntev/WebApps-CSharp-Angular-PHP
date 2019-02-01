using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Nod.Dal.Interfaces;
using Nod.Dal.Repositories;
using Nod.Model;
using Nod.Model.Entities;
using Nod.Model.Entities.DeviceEntries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nod.Dal
{
    public class DeviceContext : IDeviceContext
    {
        private readonly IMongoDatabase _database = null;

        // Repos
        public IRepository<Gps> Gpses { get; private set; }
        public IRepository<ConnectionAttribute> ConnectionAttributes { get; private set; }
        public IRepository<HardwareStatus> HardwareStatuses { get; private set; }
        public IRepository<Reset> Resets { get; private set; }
        public IRepository<UnparsedData> UnparsedData { get; private set; }
        public IRepository<Device> Devices { get; private set; }

        public DeviceContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
            Gpses = new GpsRepository(settings);
            ConnectionAttributes = new ConnectionAttributesRepository(settings);
            HardwareStatuses = new HardwareStatusesRepository(settings);
            Resets = new ResetsRepository(settings);
            UnparsedData = new UnparsedDataRepository(settings);
            Devices = new DeviceRepository(settings);
        }
    }
}
