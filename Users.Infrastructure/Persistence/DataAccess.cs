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

    public async Task<User> AddUser(User user)
    {
        var userCollection = ConnectToMongo<User>(UserCollection);
        await userCollection.InsertOneAsync(user);
        return user;
    }
}
