using Nod.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nod.Model.DTO
{
    public class DeviceDTO
    {
        public string Id { get; set; }
        public string NickName { get; set; }

        public DeviceDTO(Device device)
        {
            Id = device.Id;
            NickName = device.NickName;
        }

        public static IEnumerable<DeviceDTO> ToDeviceDTOList(IEnumerable<Device> devices)
        {
            return devices.Select(x => new DeviceDTO(x));
        }
    }
}
