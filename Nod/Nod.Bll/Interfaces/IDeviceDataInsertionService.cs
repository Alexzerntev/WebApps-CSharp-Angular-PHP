using Nod.Model.Entities;
using Nod.Model.Entities.DeviceEntries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nod.Bll.Interfaces
{
    public interface IDeviceDataInsertionService
    {
        Task<ConnectionAttribute> InsertAsync(string unparsedData, ConnectionAttribute connectionAttribute);
    }
}
