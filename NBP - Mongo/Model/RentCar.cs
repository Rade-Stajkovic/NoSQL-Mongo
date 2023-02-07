﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace NBP___Mongo.Model
{
    public class RentCar
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID {  get; set; }

        public DateTime OccupiedFrom { get; set; }  

        public DateTime OccupiedUntill { get; set; }

        public MongoDBRef User { get; set; }

        public Car Car { get; set; }

        public Dealer Dealer { get; set; }
    }
}
