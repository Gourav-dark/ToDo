using ToDo.Shared.DTO;
using ToDo.Shared.Models;
using ToDo.Shared.Services;

namespace ToDo.UI.Services;
public interface ISecureDataService
{
    Task<List<SecureData>> GetAll();
    Task<List<SecureData>> GetByUserId(string userId);
    Task<SecureData> GetById(int id);
    Task<bool> Add(SecureDataDTO obj);
    Task<bool> Update(int id,SecureDataDTO obj);
    Task<bool> Delete(int id);
}
public class SecureDataService : ISecureDataService
{
    private readonly IDataService<SecureData, int> _data;
    public SecureDataService(IDataService<SecureData, int> data)
    {
        _data=data;
    }
    public async Task<bool> Add(SecureDataDTO obj)
    {
        SecureData secureData = new SecureData()
        {
            SiteName= obj.SiteName,
            Email= obj.Email,
            Password= obj.Password,
            UserId= obj.UserId
        };
        await _data.AddAsync(secureData);
        return true;
    }

    public async Task<bool> Delete(int id)
    {
        return await _data.DeleteAsync(id);
    }

    public async Task<List<SecureData>> GetAll()
    {
        return await _data.GetAsync();
    }

    public async Task<SecureData> GetById(int id)
    {
        var secureData=await _data.GetByIdAsync(id);
        if (secureData == null)
        {
            return new SecureData();
        }
        return secureData;
    }

    public Task<List<SecureData>> GetByUserId(string userId)
    {
        return _data.GetAsync(s=>s.UserId==userId);
    }

    public async Task<bool> Update(int id,SecureDataDTO obj)
    {
        SecureData secureData=await GetById(id);
        if(secureData == null)
            return false;
        secureData.SiteName = obj.SiteName;
        secureData.Email = obj.Email;
        secureData.Password = obj.Password;
        secureData.UserId = obj.UserId;
        await _data.UpdateAsync(secureData);
        return true;
    }
}
