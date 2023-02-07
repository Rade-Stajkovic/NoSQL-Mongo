using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using NBP___Mongo.DBClient;
using NBP___Mongo.Model;

namespace NBP___Mongo.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> userCollection;

        public UserService(IDbClient dbClient)
        {
            this.userCollection = dbClient.GetUserCollection();

        }

        public void CreateUser(string name, string surname, string username, string password)
        {
            User user = new User
            {
                Name = name,
                Surname = surname,
                Username = username,
                Password = password
            };
            userCollection.InsertOne(user);
        }
    }
}
