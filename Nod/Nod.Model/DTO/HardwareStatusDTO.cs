using Nod.Model.Entities.DeviceEntries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nod.Model.DTO
{
    public class HardwareStatusDTO
    {
        public string Id { get; set; }
        public DateTime DateTime { get; set; }
        public int MainPower { get; set; }
        public int Battery { get; set; }
        public int McuTemperature { get; set; }
        public bool IsMoving { get; set; }
        public List<Signal> Signals { get; set; }

        public HardwareStatusDTO(HardwareStatus hardwareStatus)
        {
            Id = hardwareStatus.Id;
            DateTime = hardwareStatus.DateTime;
            MainPower = hardwareStatus.MainPower;
            Battery = hardwareStatus.Battery;
            McuTemperature = hardwareStatus.McuTemperature;
            IsMoving = hardwareStatus.IsMoving;
            Signals = hardwareStatus.Signals;
        }

        public static IEnumerable<HardwareStatusDTO> ToHardwareStatusDTOList(IEnumerable<HardwareStatus> hardwareStatuses)
        {
            return hardwareStatuses.Select(x => new HardwareStatusDTO(x));
        }
    }
}
