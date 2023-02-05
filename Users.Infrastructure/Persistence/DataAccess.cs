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
    private const string ConnectionString = "mongodb://usersMongoDb:Qwertyuiop9@example-mongodb-0.example-mongodb-svc.mongodb.svc.cluster.local:27017/?authMechanism=SCRAM-SHA-256&ssl=false";
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

    public Task<User> UpdateUser(User user)
    {
        var userCollection = ConnectToMongo<User>(UserCollection);

        var filter = Builders<User>.Filter.Eq("Email", user.Email);
        var updater = Builders<User>.Update
            .Set(req => req.Name, user.Name)
            .Set(req => req.Surname, user.Surname)
            .Set(req => req.Birthdate, user.Birthdate);

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

    public Task<User> DeleteUser(string email)
    {
        var userCollection = ConnectToMongo<User>(UserCollection);
        var filter = Builders<User>.Filter.Eq("Email", email);

        var deleted = userCollection.FindOneAndDeleteAsync(filter);

        return deleted;
    }

    public async Task<List<User>> GetUser(string email)
    {
        var userCollection = ConnectToMongo<User>(UserCollection);
        var filter = Builders<User>.Filter.Eq("Email", email);

        var users = await userCollection.FindAsync(filter);
        return users.ToList();
    }

    public async Task<List<User>> GetAllUsers()
    {
        var userCollection = ConnectToMongo<User>(UserCollection);
        var results = await userCollection.FindAsync(_ => true);
        return results.ToList();
    }
}
