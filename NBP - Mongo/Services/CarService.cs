using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using NBP___Mongo.DBClient;
using NBP___Mongo.Model;

namespace NBP___Mongo.Services
{
    public class CarService
    {
        private readonly IMongoCollection<Car> carCollection;
       

        public CarService(IDbClient dbClient)
        {
            this.carCollection = dbClient.GetCarCollection();
           
        }

        public void  AddNewCarAsync(String description, String year, String interiorColor, String exteriorColor)
        {




            Car car = new Car
            {
                ExteriorColor = exteriorColor,
                InteriorColor = interiorColor,
                Description = description,
                Year = year
                
            };

            carCollection.InsertOne(car);
            
        }
    }
}
