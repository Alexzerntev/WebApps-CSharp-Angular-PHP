using Nod.Bll.Interfaces;
using Nod.Dal;
using Nod.Dal.Interfaces;
using Nod.Model;
using Nod.Model.DTO;
using Nod.Model.Entities;
using Nod.Model.Entities.DeviceEntries;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Nod.Bll.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceContext _context;

        public DeviceService(IDeviceContext context)
        {
            _context = context;
        }

        public async Task<Result<IEnumerable<DeviceDTO>>> GetUserDevices(string userId)
        {
            var devices = await _context.Devices.FindAllAsync(x => x.UserId == userId);
            return Result<IEnumerable<DeviceDTO>>.CreateSuccessful(DeviceDTO.ToDeviceDTOList(devices));
        }

        public async Task<Result<IEnumerable<GpsDTO>>> GetGpsesByDateTime(string deviceId, string userId, DateTime startDateTime, DateTime endDateTime)
        {
            var device = await _context.Devices.FindAllAsync(x => x.Id == deviceId && x.UserId == userId);
            if (device == null)
            {
                return Result<IEnumerable<GpsDTO>>.CreateFailed(
                    HttpStatusCode.NotFound, "Device not found");
            }
            var gpses = await _context.Gpses.FindAllAsync(x => x.DeviceId == deviceId && x.DateTime >= startDateTime && x.DateTime <= endDateTime);
            return Result<IEnumerable<GpsDTO>>.CreateSuccessful(GpsDTO.ToGpsDTOList(gpses));
        }

        public async Task<Result<IEnumerable<HardwareStatusDTO>>> GetHardwareStatusesByDateTime(string deviceId, string userId, DateTime startDateTime, DateTime endDateTime)
        {
            var device = await _context.Devices.FindAllAsync(x => x.Id == deviceId && x.UserId == userId);
            if (device == null)
            {
                return Result<IEnumerable<HardwareStatusDTO>>.CreateFailed(
                    HttpStatusCode.NotFound, "Device not found");
            }
            var hardwareStatuses = await _context.HardwareStatuses.FindAllAsync(x => x.DeviceId == deviceId && x.DateTime >= startDateTime && x.DateTime <= endDateTime);
            return Result<IEnumerable<HardwareStatusDTO>>.CreateSuccessful(HardwareStatusDTO.ToHardwareStatusDTOList(hardwareStatuses));
        }
    }
}
