using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using MongoDB.Driver;
using NBP___Mongo.DBClient;
using NBP___Mongo.Model;

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

        public async Task AddNewCarAsync(String description, String year, String interiorColor, String exteriorColor)
        {




            Car car = new Car
            {
                ExteriorColor = exteriorColor,
                InteriorColor = interiorColor,
                Description = description,
                Year = year
                
            };

            await carCollection.InsertOneAsync(car);
        
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

            if (mark.Models == null)
            {
                mark.Models = new List<CarModel>();
                mark.Models.Add(model);
            }
            mark.Models.Add(model);
            model.Mark = mark;


            var update = Builders<Mark>.Update.Set("Models", JsonSerializer.Serialize(mark.Models));
            await markCollection.UpdateOneAsync(p => p.Name == mark.Name, update);

            var update2 = Builders<CarModel>.Update.Set("Mark", JsonSerializer.Serialize(mark));
            await modelCollection.UpdateOneAsync(m => m.Name == model.Name, update2);

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

    }
}
