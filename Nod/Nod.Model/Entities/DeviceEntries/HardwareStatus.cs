using System;
using System.Collections.Generic;
using System.Text;

namespace Nod.Model.Entities.DeviceEntries
{
    public class HardwareStatus : DeviceData
    {
        public DateTime DateTime { get; set; }
        public int MainPower { get; set; }
        public int Battery { get; set; }
        public int McuTemperature { get; set; }
        public bool IsMoving { get; set; }
        public List<Signal> Signals { get; set; }

        public HardwareStatus()
        {
        }

        public HardwareStatus(
            DateTime dateTime,
            int mainPower,
            int battery,
            int mcuTemperature,
            bool isMoving,
            List<Signal> signals,
            string parentId,
            string deviceId
            ) : base(parentId, deviceId)
        {
            DateTime = dateTime;
            MainPower = mainPower;
            Battery = battery;
            McuTemperature = mcuTemperature;
            IsMoving = isMoving;
            Signals = signals;
        }
    }
}
