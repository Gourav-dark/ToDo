using ToDo.Shared.DTO;
using ToDo.Shared.Models;
using ToDo.Shared.Services;

namespace ToDo.UI.Services;
public interface ITaskService
{
    Task<List<TaskItem>> GetAllTasks();
    Task<List<TaskItem>> GetAllTasksByUserId(string userId);
    Task<List<TaskItem>> GetDailyTasksByUserId(string userId);
    Task<TaskItem> GetById(int taskId);
    Task<string> Add(TaskItemDTO obj);
    Task<string> Update(TaskItemDTO obj);
    Task<string> Delete(int taskId);
}
public class TaskService : ITaskService
{
    private readonly IDataService<TaskItem, int> _data;
    private readonly ICategoryService _categoryService;
    public TaskService(IDataService<TaskItem, int> data, ICategoryService categoryService)
    {
        _data = data;
        _categoryService = categoryService;
    }
    public Task<string> Add(TaskItemDTO obj)
    {
        throw new NotImplementedException();
    }

    public Task<string> Delete(int taskId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<TaskItem>> GetAllTasks()
    {
        return await _data.GetAsync(null, c => c.Category);
    }

    public async Task<List<TaskItem>> GetAllTasksByUserId(string userId)
    {
        return await _data.GetAsync(x=>x.UserId==userId && x.IsDailyTask == false,c=>c.Category);
    }

    public async Task<TaskItem> GetById(int taskId)
    {
        return await _data.GetByIdAsync(taskId);
    }

    public async Task<List<TaskItem>> GetDailyTasksByUserId(string userId)
    {
        return await _data.GetAsync(x => x.UserId == userId && x.IsDailyTask==true);
    }

    public Task<string> Update(TaskItemDTO obj)
    {
        throw new NotImplementedException();
    }
}
