using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using MongoDB.Driver;
using NBP___Mongo.DBClient;
using NBP___Mongo.Model;
using MongoDB.Driver.Linq;

namespace NBP___Mongo.Services
{
    public class CarService
    {
        private readonly IMongoCollection<Car> carCollection;
        private readonly IMongoCollection<Mark> markCollection;
        private readonly IMongoCollection<CarModel> modelCollection;
        private readonly IMongoCollection<EngineType> engineCollection;



        public CarService(IDbClient dbClient)
        {
            this.carCollection = dbClient.GetCarCollection();
            this.markCollection = dbClient.GetMarkCollection();
            this.modelCollection = dbClient.GetCarModelCollection();
            this.engineCollection = dbClient.GetEngineTypeCollection();


        }

        public async Task<bool> AddNewCarAsync(String description, String year, String interiorColor, String exteriorColor, String nameMark, String nameModel, String engineId, double price, bool av)
        {
            Mark mark = await markCollection.Find(c => c.Name == nameMark).FirstOrDefaultAsync();
            CarModel model = await modelCollection.Find(m => m.Name == nameModel).FirstOrDefaultAsync();
            EngineType engine = await engineCollection.Find(e => e.Id == engineId).FirstOrDefaultAsync();

            if (mark == null || model == null || engine == null)
            {
                return false;

            }

            List<EngineType> list = new List<EngineType>();
            list.Add(engine);
            Car car = new Car
            {
                Mark = mark,
                CarModel = model,
                EngineTypes = list,
                ExteriorColor = exteriorColor,
                InteriorColor = interiorColor,
                Description = description,
                Year = year,
                Price = price,
                Available = av
               
                
            };

            await carCollection.InsertOneAsync(car);
            return true;
        
        }

        public async Task<bool> DeleteCar(String id)
        {
            Car car = await carCollection.Find(c => c.Id == id).FirstOrDefaultAsync();

            if (car == null)
            {

                return false;
            }
            await carCollection.DeleteOneAsync(c => c.Id == id);
            return true;
        }



        // Mark service
        public async Task AddNewMark(String name, String origin)
        {
            Mark mark = new Mark
            {
                Name = name,
                Origin = origin
            

            };
            
            await markCollection.InsertOneAsync(mark);

        }

        public async Task<bool> AddModelToMark(String nameMark, String nameModel)
        {

            // Ova funkcija nije dobra, mora da se menja
            Mark mark = await markCollection.Find(c => c.Name == nameMark).FirstOrDefaultAsync();
            CarModel model = await modelCollection.Find(m => m.Name == nameModel).FirstOrDefaultAsync();

            if (mark == null || model == null)
            {
                return false;

            }

            //
            model.Mark = mark;

           

           

            var update2 = Builders<CarModel>.Update.Set("Mark", model.Mark);
            await modelCollection.UpdateOneAsync(m => m.Name == model.Name, update2);


            //if (mark.Models == null)
            //{
            //    mark.Models = new List<CarModel>();
            //    mark.Models.Add(model);
            //}
            //else
            //{
            //    mark.Models.Add(model);
            //}
            

            //var update = Builders<Mark>.Update.Push("Models", JsonSerializer.Serialize(model));
            //await markCollection.UpdateOneAsync(p => p.Name == mark.Name, update);
            return true;
        }



        // Model service

        public async Task AddNewModel(String name)
        {
            CarModel model = new CarModel
            {
                Name = name,
            };

            await modelCollection.InsertOneAsync(model);

        }

        // EngineType service

        public async Task AddNewEngine(String fuelType, int power, String displacement)
        {
            EngineType engine = new EngineType
            {
                FuelType = fuelType,
                Displacement = displacement,
                Power = power
        

            };

            await engineCollection.InsertOneAsync(engine);

        }

        public async Task<bool> AddEngineToCar(String carId, String engineId)
        {
            Car car = await carCollection.Find(c => c.Id == carId).FirstOrDefaultAsync();
            EngineType engine = await engineCollection.Find(e => e.Id == engineId).FirstOrDefaultAsync();

            if (car == null || engine == null)
            {
                return false;

            }

            car.EngineTypes.Add(engine);

            var update = Builders<Car>.Update.Set("EngineTypes", car.EngineTypes);
            await carCollection.UpdateOneAsync(c => c.Id == car.Id, update);

            return true;

        }

    }
}
