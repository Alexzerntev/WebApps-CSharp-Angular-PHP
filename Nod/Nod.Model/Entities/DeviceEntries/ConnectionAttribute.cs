using System;
using System.Collections.Generic;
using System.Text;

namespace Nod.Model.Entities.DeviceEntries
{
    public class ConnectionAttribute : Entity
    {
        public string ParentId { get; set; }
        public string DeviceId { get; set; }
        public int QualityOfService { get; set; }

        public ConnectionAttribute()
        {
        }

        public ConnectionAttribute(string deviceId, int qualityOfService, string parentId)
        {
            DeviceId = deviceId;
            QualityOfService = qualityOfService;
            ParentId = parentId;
        }
    }
}
