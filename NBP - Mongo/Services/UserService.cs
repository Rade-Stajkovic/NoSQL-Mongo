using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using NBP___Mongo.DBClient;
using NBP___Mongo.Model;

namespace NBP___Mongo.Services
{
    public class UserService
    {

       
        private readonly IMongoCollection<RentCar> rentCarsCollection;
        private readonly IMongoCollection<TestDrive> testDrivesCollection;

        private readonly IMongoCollection<User> userCollection;

        public UserService(IDbClient dbClient)
        {
            this.userCollection = dbClient.GetUserCollection();
            this.rentCarsCollection = dbClient.GetRentCarCollection();
            this.testDrivesCollection = dbClient.GetTestDriveCollection();

        }

        public void CreateUser(string name, string surname, string username, string password/*, IEnumerable<ObjectId> carIds, IEnumerable<ObjectId> driveIds*/)
        {
            //List<MongoDBRef> rentCars = new List<MongoDBRef>();
            //foreach (var carId in carIds)
            //{
            //    rentCars.Add(new MongoDBRef("rentCars", carId));
            //}

            //List<MongoDBRef> testDrives = new List<MongoDBRef>();
            //foreach (var driveId in driveIds)
            //{
            //    testDrives.Add(new MongoDBRef("testDrives", driveId));
            //}
            User user = new User
            {
                Name = name,
                Surname = surname,
                Username = username,
                Password = password,
                //RentCars = rentCars,
                //TestDrives = testDrives
            };
            userCollection.InsertOne(user);
        }
    }
}
