using Nod.Model;
using Nod.Model.DTO;
using Nod.Model.Entities;
using Nod.Model.Entities.DeviceEntries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nod.Bll.Interfaces
{
    public interface IDeviceService
    {
        Task<Result<IEnumerable<DeviceDTO>>> GetUserDevices(string userId);
        Task<Result<IEnumerable<GpsDTO>>> GetGpsesByDateTime(string deviceId, string userId, DateTime startDateTime, DateTime endDateTime);
        Task<Result<IEnumerable<HardwareStatusDTO>>> GetHardwareStatusesByDateTime(string deviceId, string userId, DateTime startDateTime, DateTime endDateTime);
    }
}
