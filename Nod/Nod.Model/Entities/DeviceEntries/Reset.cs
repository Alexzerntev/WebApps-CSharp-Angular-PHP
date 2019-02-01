using System;
using System.Collections.Generic;
using System.Text;
using Nod.Model;

namespace Nod.Model.Entities.DeviceEntries
{
    public class Reset : DeviceData
    {
        public ResetCauses ResetCause { set; get; }
        public DateTime DateTime { set; get; }

        public Reset()
        {
        }

        public Reset(ResetCauses resetCause, DateTime dateTime, string parentId, string deviceId) : base(parentId, deviceId)
        {
            ResetCause = resetCause;
            DateTime = dateTime;
        }
    }
}
