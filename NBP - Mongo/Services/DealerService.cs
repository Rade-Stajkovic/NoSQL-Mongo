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
        private readonly IMongoCollection<TestDrive> testDriveCollection;
        private readonly IMongoCollection<User> userCollection;


        public DealerService(IDbClient dbClient)
        {
            this.dealerCollection = dbClient.GetDealerCollection();
            this.carsCollection = dbClient.GetCarCollection();
        }


        public async Task<string> CreateDealer(string username, string password, string name,string location /*IEnumerable<ObjectId> carIds*/)
        {
            //List<MongoDBRef> cars = new List<MongoDBRef>();
            //foreach (var carId in carIds)
            //{
            //    cars.Add(new MongoDBRef("cars", carId));
            //}
            var dealer = dealerCollection.Find(p => p.Username == username).FirstOrDefaultAsync();
            if (dealer != null)
            {
                return "Korisnik sa tim korisničkim imenom već postoji.";
            }
            else
            {

                Dealer dealer1 = new Dealer
                {
                    Username = username,
                    Password = password,
                    Name = name,
                    Location = location,
                   
                };

                dealerCollection.InsertOne(dealer1);
                return "Uspešno kreiran korisnik.";
            }
        }

        public async Task<Dealer> LogInDealer(String username, String password)
        {

            var dealer = await dealerCollection.Find(x => x.Username == username && x.Password == password).FirstOrDefaultAsync();
            if (dealer != null)
            {
                return dealer;
            }
            return null;
        }

        public async Task<List<TestDrive>> GetDealersTestDrives(string DealerID)
        {
            List<TestDrive> testDrives = new List<TestDrive>();
            testDrives = await testDriveCollection.Find(p => p.Dealer.Id == DealerID).ToListAsync();
            return testDrives;

        }

        public async Task<bool> AddCarToDealer(string CarID, string DealerID)
        {
            Car c = await carsCollection.Find(p => p.Id == CarID).FirstOrDefaultAsync();
            Dealer dealer = await dealerCollection.Find(p => p.ID == DealerID).FirstOrDefaultAsync();

            //...
            var update2 = Builders<Car>.Update.Set("Dealer", new MongoDBRef("dealers", dealer.ID));
            await carsCollection.UpdateOneAsync(p=>p.Id == CarID, update2);

            List<MongoDBRef> oldcars = dealer.Cars;
            oldcars.Add(new MongoDBRef("cars", CarID));

            var update3 = Builders<Dealer>.Update.Set("Cars", oldcars);
            await dealerCollection.UpdateManyAsync(p => p.ID == DealerID, update3);

            return true;
        }


    }

}
