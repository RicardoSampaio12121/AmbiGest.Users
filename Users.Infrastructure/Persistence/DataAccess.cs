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

    public async Task<User> AddUser(User user)
    {
        var userCollection = ConnectToMongo<User>(UserCollection);
        await userCollection.InsertOneAsync(user);
        return user;
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

    public Task UpdateEmail(string currentEmail, string newEmail)
    {
        //TODO: Check if the newEmail is already in use
        var userCollection = ConnectToMongo<User>(UserCollection);
        
        var filter = Builders<User>.Filter.Eq("Email", currentEmail);
        var updater = Builders<User>.Update.Set(req => req.Email, newEmail);

        var existingUser = userCollection.FindOneAndUpdateAsync(filter, updater, options: new FindOneAndUpdateOptions<User>
        {
            ReturnDocument = ReturnDocument.After
        });

        return existingUser;
    }

    public Task DeleteUser(string email)
    {
        var userCollection = ConnectToMongo<User>(UserCollection);
        var filter = Builders<User>.Filter.Eq("Email", email);

        var deleted = userCollection.FindOneAndDeleteAsync(filter);

        return deleted;
    }

    public Task GetUser(string email)
    {
        var userCollection = ConnectToMongo<User>(UserCollection);
        var filter = Builders<User>.Filter.Eq("Email", email);

        return userCollection.FindAsync(filter);
    }

    public async Task<List<User>> GetAllUsers()
    {
        var userCollection = ConnectToMongo<User>(UserCollection);
        var results = await userCollection.FindAsync(_ => true);
        return results.ToList();
    }
}
