using ToDo.Shared.DTO;
using ToDo.Shared.Models;
using ToDo.Shared.Services;
using ToDo.Shared.Wrapper;

namespace ToDo.UI.Services;

public interface ICategoryService
{
    Task<ResponseWrapper<List<Category>>> GetAll();
    Task<ResponseWrapper<Category>> GetByCategoryId(int categoryId);
    Task<ResponseWrapper<List<Category>>> GetByUserId(string userId);
    Task<ResponseWrapper<Category>> Add(CategoryDTO obj);
    Task<ResponseWrapper<Category>> Update(int categoryId, CategoryDTO obj);
    Task<ResponseWrapper<Category>> Delete(int categoryId);
}

public class CategoryService : ICategoryService
{
    private readonly IDataService<Category, int> _data;

    public CategoryService(IDataService<Category, int> data)
    {
        _data = data ?? throw new ArgumentNullException(nameof(data));
    }

    public async Task<ResponseWrapper<Category>> Add(CategoryDTO obj)
    {
        if (obj == null || string.IsNullOrWhiteSpace(obj.Name))
        {
            return new ResponseWrapper<Category>
            {
                Succeeded = false,
                Message = "Invalid category data."
            };
        }

        var category = new Category
        {
            Name = obj.Name,
            UserId = obj.UserId,
        };

        try
        {
            await _data.AddAsync(category);
            return new ResponseWrapper<Category>(category)
            {
                Message = "Category added successfully."
            };
        }
        catch (Exception ex)
        {
            return new ResponseWrapper<Category>
            {
                Succeeded = false,
                Message = $"Failed to add category: {ex.Message}"
            };
        }
    }

    public async Task<ResponseWrapper<Category>> Delete(int categoryId)
    {
        if (categoryId <= 0)
        {
            return new ResponseWrapper<Category>
            {
                Succeeded = false,
                Message = "Invalid category ID."
            };
        }

        try
        {
            await _data.DeleteAsync(categoryId);
            return new ResponseWrapper<Category>
            {
                Succeeded=true,
                Message = "Category deleted successfully."
            };
        }
        catch (Exception ex)
        {
            return new ResponseWrapper<Category>
            {
                Succeeded = false,
                Message = $"Failed to delete category: {ex.Message}"
            };
        }
    }

    public async Task<ResponseWrapper<List<Category>>> GetAll()
    {
        try
        {
            var categories = await _data.GetAsync();
            return new ResponseWrapper<List<Category>>(categories)
            {
                Message = "Categories retrieved successfully."
            };
        }
        catch (Exception ex)
        {
            return new ResponseWrapper<List<Category>>
            {
                Succeeded = false,
                Message = $"Failed to retrieve categories: {ex.Message}"
            };
        }
    }

    public async Task<ResponseWrapper<Category>> GetByCategoryId(int categoryId)
    {
        if (categoryId <= 0)
        {
            return new ResponseWrapper<Category>
            {
                Succeeded = false,
                Message = "Invalid category ID."
            };
        }

        try
        {
            var category = await _data.GetByIdAsync(categoryId);
            if (category == null)
            {
                return new ResponseWrapper<Category>
                {
                    Succeeded = false,
                    Message = "Category not found."
                };
            }

            return new ResponseWrapper<Category>(category)
            {
                Message = "Category retrieved successfully."
            };
        }
        catch (Exception ex)
        {
            return new ResponseWrapper<Category>
            {
                Succeeded = false,
                Message = $"Failed to retrieve category: {ex.Message}"
            };
        }
    }

    public async Task<ResponseWrapper<List<Category>>> GetByUserId(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            return new ResponseWrapper<List<Category>>
            {
                Succeeded = false,
                Message = "Invalid user ID."
            };
        }

        try
        {
            var categories = await _data.GetAsync(c => c.UserId == userId || c.UserId == null);
            return new ResponseWrapper<List<Category>>(categories)
            {
                Message = "Categories retrieved successfully."
            };
        }
        catch (Exception ex)
        {
            return new ResponseWrapper<List<Category>>
            {
                Succeeded = false,
                Message = $"Failed to retrieve categories: {ex.Message}"
            };
        }
    }

    public async Task<ResponseWrapper<Category>> Update(int categoryId,CategoryDTO obj)
    {
        if (obj == null || string.IsNullOrWhiteSpace(obj.Name))
        {
            return new ResponseWrapper<Category>
            {
                Succeeded = false,
                Message = "Invalid category data."
            };
        }

        try
        {
            var category = await _data.GetByIdAsync(categoryId);
            if (category == null)
            {
                return new ResponseWrapper<Category>
                {
                    Succeeded = false,
                    Message = "Category not found."
                };
            }

            category.Name = obj.Name;
            category.UserId = obj.UserId;
            await _data.UpdateAsync(category);

            return new ResponseWrapper<Category>(category)
            {
                Message = "Category updated successfully."
            };
        }
        catch (Exception ex)
        {
            return new ResponseWrapper<Category>
            {
                Succeeded = false,
                Message = $"Failed to update category: {ex.Message}"
            };
        }
    }
}
