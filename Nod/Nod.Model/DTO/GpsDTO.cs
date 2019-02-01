using Nod.Model.Entities.DeviceEntries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nod.Model.DTO
{
    public class GpsDTO
    {
        public string Id { get; set; }
        public DateTime DateTime { get; set; }
        public float Lattitude { get; set; }
        public float Longtitude { get; set; }
        public float CurrentSpeed { get; set; }
        public float AverageSpeed { get; set; }
        public float MaxSpeed { get; set; }
        public int SpeedAccuracy { get; set; }

        public GpsDTO(Gps gps)
        {
            Id = gps.Id;
            DateTime = gps.DateTime;
            Lattitude = gps.Lattitude;
            Longtitude = gps.Longtitude;
            CurrentSpeed = gps.CurrentSpeed;
            AverageSpeed = gps.AverageSpeed;
            MaxSpeed = gps.MaxSpeed;
            SpeedAccuracy = gps.SpeedAccuracy;
        }

        public static IEnumerable<GpsDTO> ToGpsDTOList(IEnumerable<Gps> gpses)
        {
            return gpses.Select(x => new GpsDTO(x));
        }
    }
}
