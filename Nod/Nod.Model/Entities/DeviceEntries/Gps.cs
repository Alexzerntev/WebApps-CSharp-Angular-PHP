using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nod.Model.Entities.DeviceEntries
{
    public class Gps : DeviceData
    {
        public DateTime DateTime { get; set; }
        [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        public float Lattitude { get; set; }
        [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        public float Longtitude { get; set; }
        [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        public float CurrentSpeed { get; set; }
        [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        public float AverageSpeed { get; set; }
        [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        public float MaxSpeed { get; set; }
        public int SpeedAccuracy { get; set; }

        public Gps()
        {
        }

        public Gps(
            DateTime dateTime,
            float lattitude, 
            float longtitude, 
            float currentSpeed, 
            float averageSpeed,
            float maxSpeed,
            int speedAccuracy,
            string parentId,
            string deviceId
            ) : base(parentId, deviceId)
        {
            DateTime = dateTime;
            Lattitude = lattitude;
            Longtitude = longtitude;
            CurrentSpeed = currentSpeed;
            AverageSpeed = averageSpeed;
            MaxSpeed = maxSpeed;
            SpeedAccuracy = speedAccuracy;
        }
    }
}
