using Backend.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Services;
public class TaskService
{
    private readonly IMongoCollection<TaskShare> _taskCollection;

    public TaskService()
    {
        var client = new MongoClient("mongodb://mongo:27017");
        var database = client.GetDatabase("todo_db");
        _taskCollection = database.GetCollection<TaskShare>("tasks");
    }

    public async Task<List<TaskShare>> GetTasksAsync()
    {
        return await _taskCollection.Find(task => true).ToListAsync();
    }

    public async Task<TaskShare> GetTaskByIdAsync(string id)
    {
        return await _taskCollection.Find(task => task.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateTaskAsync(TaskShare task)
    {
        await _taskCollection.InsertOneAsync(task);
    }

    public async Task UpdateTaskAsync(string id, TaskShare taskIn)
    {
        await _taskCollection.ReplaceOneAsync(task => task.Id == id, taskIn);
    }

    public async Task DeleteTaskAsync(string id)
    {
        await _taskCollection.DeleteOneAsync(task => task.Id == id);
    }
}
