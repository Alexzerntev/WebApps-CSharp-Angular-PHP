using MongoDB.Driver;
using Nod.Dal.Interfaces;
using Nod.Model.Entities;
using Nod.Model.Entities.DeviceEntries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nod.Dal.Interfaces
{
    public interface IDeviceContext
    {
        IRepository<Gps> Gpses { get; }
        IRepository<ConnectionAttribute> ConnectionAttributes { get; }
        IRepository<HardwareStatus> HardwareStatuses { get; }
        IRepository<Reset> Resets { get; }
        IRepository<UnparsedData> UnparsedData { get; }
        IRepository<Device> Devices { get; }
    }
}
