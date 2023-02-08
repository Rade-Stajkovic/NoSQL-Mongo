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

    }

}
