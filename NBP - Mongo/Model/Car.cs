
using MongoDB.Driver;
﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace NBP___Mongo.Model
{
    public class Car
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public CarModel CarModel { get; set; }

        public Mark Mark { get; set; }

        public  String ExteriorColor { get; set; }

        public String InteriorColor { get; set; }

        public String Drivetrain  { get; set; }

        public List<EngineType> Engine { get; set; }

        public String Description { get; set; }

        public String Year { get; set; }

        public MongoDBRef Dealer{ get; set; }


    }
}
