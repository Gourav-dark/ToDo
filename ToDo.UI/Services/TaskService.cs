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
    Task<bool> Update(int taskId,TaskItemDTO obj);
    Task<bool> Delete(int taskId);
    Task<bool> ChangeStatus(int taskId);
}
public class TaskService : ITaskService
{
    private readonly IDataService<TaskItem, int> _data;
    public TaskService(IDataService<TaskItem, int> data)
    {
        _data = data;
    }
    public Task<string> Add(TaskItemDTO obj)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(int taskId)
    {
        return _data.DeleteAsync(taskId);
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

    public async Task<bool> Update(int taskId,TaskItemDTO obj)
    {
        TaskItem taskItem = await GetById(taskId);
        if(taskItem==null) 
            return false;
        taskItem.Title = obj.Title;
        taskItem.Description = obj.Description;
        taskItem.DueTime = obj.DueTime;
        taskItem.Priority = obj.Priority;
        taskItem.IsDailyTask = obj.IsDailyTask;
        taskItem.CategoryId = obj.CategoryId;

        await _data.UpdateAsync(taskItem);
        return true;
    }
    public async Task<bool> ChangeStatus(int taskId)
    {
        TaskItem taskItem = await GetById(taskId);
        if (taskItem == null)
            return false;
        taskItem.IsCompleted=!taskItem.IsCompleted;
        await _data.UpdateAsync(taskItem);
        return true;
    }
}
