using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using Users.Application.Common.Interfaces;
using Users.Domain.Entities;

namespace Users.Infrastructure.Persistence;
public class DataAccess: IDataAccess
{
    private const string ConnectionString = "mongodb://localhost:27018";
    private const string DatabaseName = "Users";
    private const string UserCollection = "users";

    private IMongoCollection<T> ConnectToMongo<T>(in string collection)
    {
        var client = new MongoClient(ConnectionString);
        var db = client.GetDatabase(DatabaseName);
        return db.GetCollection<T>(collection);
    }

    private async Task<List<User>> GetAllUsers()
    {
        var userCollection = ConnectToMongo<User>(UserCollection);
        var results = await userCollection.FindAsync(_ => true);
        return results.ToList();
    }

    public Task AddUser(User user)
    {
        var userCollection = ConnectToMongo<User>(UserCollection);
        return userCollection.InsertOneAsync(user);
    }

    public Task UpdateUser(User user)
    {
        var userCollection = ConnectToMongo<User>(UserCollection);

        var filter = Builders<User>.Filter.Eq("Email", user.Email);
        var updater = Builders<User>.Update
            .Set(req => req.Name, user.Name)
            .Set(req => req.Surname, user.Surname)
            .Set(req => req.BirthDate, user.BirthDate);

        var existingUser = userCollection.FindOneAndUpdateAsync(filter, updater, options: new FindOneAndUpdateOptions<User>
        {
            ReturnDocument = ReturnDocument.After
        });

        return existingUser;
    }
}
