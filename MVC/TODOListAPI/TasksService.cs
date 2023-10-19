using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MVC.Models;

namespace MVC.Service;

public class TasksService
{
        private readonly IMongoCollection<TaskEntity> _todoListCollection;

    public TasksService(
        IOptions<TasksStoreDatabaseSettings> todoListDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            todoListDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            todoListDatabaseSettings.Value.DatabaseName);

        _todoListCollection = mongoDatabase.GetCollection<TaskEntity>(
            todoListDatabaseSettings.Value.TODOListCollectionName);
    }

    public async Task<List<TaskEntity>> GetAsync() =>
        await _todoListCollection.Find(_ => true).ToListAsync();

    public async Task<TaskEntity?> GetAsync(string id) =>
        await _todoListCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(TaskEntity newBook) =>
        await _todoListCollection.InsertOneAsync(newBook);

    public async Task UpdateAsync(string id, TaskEntity updatedBook) =>
        await _todoListCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

    public async Task RemoveAsync(string id) =>
        await _todoListCollection.DeleteOneAsync(x => x.Id == id);
}
