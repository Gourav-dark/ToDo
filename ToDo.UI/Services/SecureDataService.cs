using ToDo.Shared.DTO;
using ToDo.Shared.Models;
using ToDo.Shared.Services;
using ToDo.Shared.Wrapper;

namespace ToDo.UI.Services;

public interface ISecureDataService
{
    Task<ResponseWrapper<List<SecureData>>> GetAll();
    Task<ResponseWrapper<List<SecureData>>> GetByUserId(string userId);
    Task<ResponseWrapper<SecureData>> GetById(int id);
    Task<ResponseWrapper<SecureData>> Add(SecureDataDTO obj);
    Task<ResponseWrapper<SecureData>> Update(int id, SecureDataDTO obj);
    Task<ResponseWrapper<SecureData>> Delete(int id);
}

public class SecureDataService : ISecureDataService
{
    private readonly IDataService<SecureData, int> _data;

    public SecureDataService(IDataService<SecureData, int> data)
    {
        _data = data ?? throw new ArgumentNullException(nameof(data));
    }

    public async Task<ResponseWrapper<SecureData>> Add(SecureDataDTO obj)
    {
        if (obj == null || string.IsNullOrWhiteSpace(obj.SiteName))
        {
            return new ResponseWrapper<SecureData>
            {
                Succeeded = false,
                Message = "Invalid secure data details."
            };
        }

        var secureData = new SecureData
        {
            SiteName = obj.SiteName,
            Email = obj.Email,
            Password = obj.Password,
            UserId = obj.UserId
        };

        try
        {
            await _data.AddAsync(secureData);
            return new ResponseWrapper<SecureData>(secureData)
            {
                Message = "Secure data added successfully."
            };
        }
        catch (Exception ex)
        {
            return new ResponseWrapper<SecureData>
            {
                Succeeded = false,
                Message = $"Failed to add secure data: {ex.Message}"
            };
        }
    }

    public async Task<ResponseWrapper<SecureData>> Delete(int id)
    {
        if (id <= 0)
        {
            return new ResponseWrapper<SecureData>
            {
                Succeeded = false,
                Message = "Invalid secure data ID."
            };
        }

        try
        {
            await _data.DeleteAsync(id);
            return new ResponseWrapper<SecureData>
            {
                Succeeded = true,
                Message = "Secure data deleted successfully."
            };
        }
        catch (Exception ex)
        {
            return new ResponseWrapper<SecureData>
            {
                Succeeded = false,
                Message = $"Failed to delete secure data: {ex.Message}"
            };
        }
    }

    public async Task<ResponseWrapper<List<SecureData>>> GetAll()
    {
        try
        {
            var data = await _data.GetAsync();
            return new ResponseWrapper<List<SecureData>>(data)
            {
                Message = "Secure data retrieved successfully."
            };
        }
        catch (Exception ex)
        {
            return new ResponseWrapper<List<SecureData>>
            {
                Succeeded = false,
                Message = $"Failed to retrieve secure data: {ex.Message}"
            };
        }
    }

    public async Task<ResponseWrapper<SecureData>> GetById(int id)
    {
        if (id <= 0)
        {
            return new ResponseWrapper<SecureData>
            {
                Succeeded = false,
                Message = "Invalid secure data ID."
            };
        }

        try
        {
            var secureData = await _data.GetByIdAsync(id);
            if (secureData == null)
            {
                return new ResponseWrapper<SecureData>
                {
                    Succeeded = false,
                    Message = "Secure data not found."
                };
            }

            return new ResponseWrapper<SecureData>(secureData)
            {
                Message = "Secure data retrieved successfully."
            };
        }
        catch (Exception ex)
        {
            return new ResponseWrapper<SecureData>
            {
                Succeeded = false,
                Message = $"Failed to retrieve secure data: {ex.Message}"
            };
        }
    }

    public async Task<ResponseWrapper<List<SecureData>>> GetByUserId(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            return new ResponseWrapper<List<SecureData>>
            {
                Succeeded = false,
                Message = "Invalid user ID."
            };
        }

        try
        {
            var data = await _data.GetAsync(s => s.UserId == userId);
            return new ResponseWrapper<List<SecureData>>(data)
            {
                Message = "Secure data retrieved successfully."
            };
        }
        catch (Exception ex)
        {
            return new ResponseWrapper<List<SecureData>>
            {
                Succeeded = false,
                Message = $"Failed to retrieve secure data: {ex.Message}"
            };
        }
    }

    public async Task<ResponseWrapper<SecureData>> Update(int id, SecureDataDTO obj)
    {
        if (id <= 0 || obj == null || string.IsNullOrWhiteSpace(obj.SiteName))
        {
            return new ResponseWrapper<SecureData>
            {
                Succeeded = false,
                Message = "Invalid secure data details."
            };
        }

        try
        {
            var secureData = await _data.GetByIdAsync(id);
            if (secureData == null)
            {
                return new ResponseWrapper<SecureData>
                {
                    Succeeded = false,
                    Message = "Secure data not found."
                };
            }

            secureData.SiteName = obj.SiteName;
            secureData.Email = obj.Email;
            secureData.Password = obj.Password;
            secureData.UserId = obj.UserId;

            await _data.UpdateAsync(secureData);
            return new ResponseWrapper<SecureData>(secureData)
            {
                Message = "Secure data updated successfully."
            };
        }
        catch (Exception ex)
        {
            return new ResponseWrapper<SecureData>
            {
                Succeeded = false,
                Message = $"Failed to update secure data: {ex.Message}"
            };
        }
    }
}