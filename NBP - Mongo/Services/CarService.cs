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

        public async Task<bool> AddNewCarAsync(String description, String year, String interiorColor, String exteriorColor, String nameMark, String nameModel, String engineId, double price, bool av, bool rentOrSale)
        {
            Mark mark = await markCollection.Find(c => c.Name == nameMark).FirstOrDefaultAsync();
            CarModel model = await modelCollection.Find(m => m.Name == nameModel).FirstOrDefaultAsync();
            EngineType engine = await engineCollection.Find(e => e.Id == engineId).FirstOrDefaultAsync();

            if (mark == null || model == null || engine == null)
            {
                return false;

            }

         
            Car car = new Car
            {
                Mark = mark,
                CarModel = model,
                EngineType = new MongoDBRef("engine", engine.Id),
                ExteriorColor = exteriorColor,
                InteriorColor = interiorColor,
                Description = description,
                Year = year,
                Price = price,
                Available = av,
                RentOrSale = rentOrSale
                
               
                
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

        public async Task<List<Mark>> GetAllMarks()
        {
            return await markCollection.Find(f => true).ToListAsync();

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
            model.Mark = new MongoDBRef("mark", mark.Id);

           

           

            var update2 = Builders<CarModel>.Update.Set("Mark", model.Mark);
            await modelCollection.UpdateOneAsync(m => m.Name == model.Name, update2);

            MongoDBRef r = new MongoDBRef("models", model.Id);
            if (mark.Models == null)
            {
                mark.Models = new List<MongoDBRef>();
              
                mark.Models.Add(r);
            }
            else
            {
                mark.Models.Add(r);
            }


            var update = Builders<Mark>.Update.Set("Models", mark.Models);
            await markCollection.UpdateOneAsync(p => p.Name == mark.Name, update);
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

        public async Task<List<CarModel>> GetModelsFromMark(String markId)
        {
            Mark mark = await markCollection.Find(m => m.Id == markId).FirstOrDefaultAsync();

           
            return await modelCollection.Find(f => f.Mark.Id == mark.Id).ToListAsync();

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

       

        public async Task<List<Car>> GetCars()
        {
            return  await carCollection.Find(c => true).ToListAsync();

            
        }

        public async Task<List<Car>> GetCarsWithFilters(String markName, String modelName, double maxPrice, String fuelType)
        {
            


            return await carCollection.Find(c => (maxPrice != 0)? c.Price  < maxPrice : true && (markName !="")?c.Mark.Name == markName:true && (modelName != "") ? c.CarModel.Name == modelName : true && c.Available).ToListAsync();


        }

    }
}
