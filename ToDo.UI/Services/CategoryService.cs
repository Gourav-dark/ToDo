using ToDo.Shared.DTO;
using ToDo.Shared.Models;
using ToDo.Shared.Services;

namespace ToDo.UI.Services;
public interface ICategoryService
{
    Task<List<Category>> GetAll();
    Task<Category> GetByCategoryId(int categoryId);
    Task<List<Category>> GetByUserId(string userId);
    Task<bool> Add(CategoryDTO obj);
    Task<bool> Update(CategoryDTO obj);
    Task<bool> Delete(int categoryId);
}
public class CategoryService:ICategoryService
{
    private readonly IDataService<Category, int> _data;
    public CategoryService(IDataService<Category, int> data)
    {
        _data = data;
    }

    public async Task<bool> Add(CategoryDTO obj)
    {
        Category category = new Category()
        {
            Name = obj.Name,
            UserId = obj.UserId
        };
        await _data.AddAsync(category);
        return true;
    }

    public Task<bool> Delete(int categoryId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Category>> GetAll()
    {
        List<Category> list = await _data.GetAllAsync();
        return list;
    }

    public async Task<Category> GetByCategoryId(int categoryId)
    {
        return await _data.GetByIdAsync(categoryId);
    }

    public async Task<List<Category>> GetByUserId(string userId)
    {
        List<Category> list = await _data.FindAsync(c=>c.UserId==userId || c.UserId==null);
        return list;
    }

    public Task<bool> Update(CategoryDTO obj)
    {
        throw new NotImplementedException();
    }
}
