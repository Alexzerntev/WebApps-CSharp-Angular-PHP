using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nod.Model.Entities.DeviceEntries
{
    public abstract class DeviceData : Entity
    {
        public string ParentId { get; set; }
        public string DeviceId { get; set; }

        protected DeviceData()
        {
        }

        protected DeviceData(string parentId, string deviceId)
        {
            ParentId = parentId;
            DeviceId = deviceId;
        }
    }
}
