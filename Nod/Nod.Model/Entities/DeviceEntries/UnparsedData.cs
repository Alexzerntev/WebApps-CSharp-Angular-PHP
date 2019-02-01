using System;
using System.Collections.Generic;
using System.Text;

namespace Nod.Model.Entities.DeviceEntries
{
    public class UnparsedData : Entity
    {
        public DateTime ArrivalDateTime { get; set; }
        public string Data { get; set; }

        public UnparsedData()
        {
        }

        public UnparsedData(DateTime arrivalDateTime, string data) 
        {
            ArrivalDateTime = arrivalDateTime;
            Data = data;
        }
    }
}
