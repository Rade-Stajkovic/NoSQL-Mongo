﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
        private readonly IMongoCollection<RentCar> rentcarCollection;





        public DealerService(IDbClient dbClient)
        {
            this.dealerCollection = dbClient.GetDealerCollection();
            this.carsCollection = dbClient.GetCarCollection();
            this.rentcarCollection = dbClient.GetRentCarCollection();
        }


        public async Task<string> CreateDealer(string username, string password, string name, string location /*IEnumerable<ObjectId> carIds*/)
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
            var testDrives = testDriveCollection.AsQueryable().Where(r => r.Dealer.Id == DealerID).ToList();
            return testDrives;
        }

        public async Task<bool> AddCarToDealer(string CarID, string DealerID)
        {
            Car c = await carsCollection.Find(p => p.Id == CarID).FirstOrDefaultAsync();
            Dealer dealer = await dealerCollection.Find(p => p.ID == DealerID).FirstOrDefaultAsync();


            if (c == null || dealer == null)
            {
                return false;

            }
            var update2 = Builders<Car>.Update.Set("Dealer", new MongoDBRef("dealers", dealer.ID));
            await carsCollection.UpdateOneAsync(p => p.Id == CarID, update2);

            List<MongoDBRef> oldcars = dealer.Cars;
            oldcars.Add(new MongoDBRef("cars", CarID));

            var update3 = Builders<Dealer>.Update.Set("Cars", oldcars);
            await dealerCollection.UpdateManyAsync(p => p.ID == DealerID, update3);

            return true;
        }




        public async Task<bool> UpdateCarPrice(string id, double price, string dealerId)
        {

            var filter = Builders<Car>.Filter.Eq(c => c.Id, id);
            var update = Builders<Car>.Update
                .Set(c => c.Price, price)
                .Set(c => c.Dealer, new MongoDBRef("dealers", dealerId));
            carsCollection.UpdateOne(filter, update);
            return true;
        }
        public async Task<bool> UpdateCarAvailability(string id, bool available, string dealerId)
        {

            var filter = Builders<Car>.Filter.Eq(c => c.Id, id);
            var update = Builders<Car>.Update
                .Set(c => c.Available, available)
                .Set(c => c.Dealer, new MongoDBRef("dealers", dealerId));

            carsCollection.UpdateOne(filter, update);
            return true;
        }

        public async Task<List<RentCar>> GetRentCars(string dealerId)
        {
            var filter = Builders<RentCar>.Filter.Eq(rentcar => rentcar.Dealer.Id, dealerId);
            var result = await rentcarCollection.Find(filter).ToListAsync();
            return result;
        }




    }


}
