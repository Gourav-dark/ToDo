using ToDo.Shared.DTO;
using ToDo.Shared.Models;
using ToDo.Shared.Services;
using ToDo.Shared.Wrapper;

namespace ToDo.UI.Services;
public interface ITaskService
{
    Task<ResponseWrapper<List<TaskItem>>> GetAllTasks();
    Task<ResponseWrapper<List<TaskItem>>> GetAllTasksByUserId(string userId);
    Task<ResponseWrapper<List<TaskItem>>> GetDailyTasksByUserId(string userId);
    Task<ResponseWrapper<TaskItem>> GetById(int taskId);
    Task<ResponseWrapper<TaskItem>> Add(TaskItemDTO obj);
    Task<ResponseWrapper<TaskItem>> Update(int taskId, TaskItemDTO obj);
    Task<ResponseWrapper<TaskItem>> Delete(int taskId);
    Task<ResponseWrapper<TaskItem>> ChangeStatus(int taskId);
}

public class TaskService : ITaskService
{
    private readonly IDataService<TaskItem, int> _data;

    public TaskService(IDataService<TaskItem, int> data)
    {
        _data = data;
    }

    public async Task<ResponseWrapper<TaskItem>> Add(TaskItemDTO obj)
    {
        if (obj == null)
        {
            return new ResponseWrapper<TaskItem>
            {
                Succeeded = false,
                Message = "Invalid task data."
            };
        }

        var taskItem = new TaskItem
        {
            Title = obj.Title,
            Description = obj.Description,
            DueTime = obj.DueTime,
            Priority = obj.Priority,
            IsDailyTask = obj.IsDailyTask,
            CategoryId = obj.CategoryId,
            UserId = obj.UserId,
            IsCompleted = false
        };

        try
        {
            var createdTask = await _data.AddAsync(taskItem);
            return new ResponseWrapper<TaskItem>(taskItem)
            {
                Message = "Task added successfully."
            };
        }
        catch (Exception ex)
        {
            return new ResponseWrapper<TaskItem>
            {
                Succeeded = false,
                Message = $"Failed to add task: {ex.Message}"
            };
        }
    }

    public async Task<ResponseWrapper<TaskItem>> Delete(int taskId)
    {
        try
        {
            var result = await _data.DeleteAsync(taskId);
            return new ResponseWrapper<TaskItem>
            {
                Succeeded=result,
                Message = result ? "Task deleted successfully." : "Task not found."
            };
        }
        catch (Exception ex)
        {
            return new ResponseWrapper<TaskItem>
            {
                Succeeded = false,
                Message = $"Failed to delete task: {ex.Message}"
            };
        }
    }

    public async Task<ResponseWrapper<List<TaskItem>>> GetAllTasks()
    {
        var tasks = await _data.GetAsync(null, c => c.Category);
        return new ResponseWrapper<List<TaskItem>>(tasks);
    }

    public async Task<ResponseWrapper<List<TaskItem>>> GetAllTasksByUserId(string userId)
    {
        var tasks = await _data.GetAsync(x => x.UserId == userId && !x.IsDailyTask, c => c.Category);
        return new ResponseWrapper<List<TaskItem>>(tasks);
    }

    public async Task<ResponseWrapper<TaskItem>> GetById(int taskId)
    {
        var task = await _data.GetByIdAsync(taskId);
        if (task == null)
        {
            return new ResponseWrapper<TaskItem>
            {
                Succeeded = false,
                Message = "Task not found."
            };
        }
        return new ResponseWrapper<TaskItem>(task);
    }

    public async Task<ResponseWrapper<List<TaskItem>>> GetDailyTasksByUserId(string userId)
    {
        var tasks = await _data.GetAsync(x => x.UserId == userId && x.IsDailyTask);
        return new ResponseWrapper<List<TaskItem>>(tasks);
    }

    public async Task<ResponseWrapper<TaskItem>> Update(int taskId, TaskItemDTO obj)
    {
        var taskItem = await _data.GetByIdAsync(taskId);
        if (taskItem == null)
        {
            return new ResponseWrapper<TaskItem>
            {
                Succeeded = false,
                Message = "Task not found."
            };
        }

        taskItem.Title = obj.Title;
        taskItem.Description = obj.Description;
        taskItem.DueTime = obj.DueTime;
        taskItem.Priority = obj.Priority;
        taskItem.IsDailyTask = obj.IsDailyTask;
        taskItem.CategoryId = obj.CategoryId;

        try
        {
            await _data.UpdateAsync(taskItem);
            return new ResponseWrapper<TaskItem>(taskItem)
            {
                Message = "Task updated successfully."
            };
        }
        catch (Exception ex)
        {
            return new ResponseWrapper<TaskItem>
            {
                Succeeded = false,
                Message = $"Failed to update task: {ex.Message}"
            };
        }
    }

    public async Task<ResponseWrapper<TaskItem>> ChangeStatus(int taskId)
    {
        var taskItem = await _data.GetByIdAsync(taskId);
        if (taskItem == null)
        {
            return new ResponseWrapper<TaskItem>
            {
                Succeeded = false,
                Message = "Task not found."
            };
        }

        taskItem.IsCompleted = !taskItem.IsCompleted;

        try
        {
            await _data.UpdateAsync(taskItem);
            return new ResponseWrapper<TaskItem>(taskItem)
            {
                Message = "Task status updated successfully."
            };
        }
        catch (Exception ex)
        {
            return new ResponseWrapper<TaskItem>
            {
                Succeeded = false,
                Message = $"Failed to update task status: {ex.Message}"
            };
        }
    }
}
