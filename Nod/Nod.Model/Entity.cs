using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Nod.Model.Interfaces;
using System;

namespace Nod.Model
{
    public abstract class Entity : IEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
