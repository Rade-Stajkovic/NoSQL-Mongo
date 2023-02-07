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
    public class DealerService
    {
      
        
        private readonly IMongoCollection<Dealer> dealerCollection;
        private readonly IMongoCollection<Car> carsCollection;


        public DealerService(IDbClient dbClient)
        {
            this.dealerCollection = dbClient.GetDealerCollection();
            this.carsCollection = dbClient.GetCarCollection();
        }


        public void CreateDealer(string username, string password, string name,string location, IEnumerable<ObjectId> carIds)
        {
            List<MongoDBRef> cars = new List<MongoDBRef>();
            foreach (var carId in carIds)
            {
                cars.Add(new MongoDBRef("cars", carId));
            }


            Dealer dealer = new Dealer
            {
                Username = username,
                Password = password,
                Name = name,
                Location = location,
                Cars = cars
            };

             dealerCollection.InsertOne(dealer);
        }


    }

}
