using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ToDo.Shared.Data;

namespace ToDo.Shared.Services;

// Generic data service interface
public interface IDataService<T, TKey> where T : class
{
    Task<List<T>> GetAsync(Expression<Func<T, bool>>? predicate = null, params Expression<Func<T, object>>[] includes);
    Task<T?> GetByIdAsync(TKey id);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<bool> DeleteAsync(TKey id);
}

// Implementation of the data service
public class DataService<T, TKey> : IDataService<T, TKey> where T : class
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;

    public DataService(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    // Unified GetAsync method
    public async Task<List<T>> GetAsync(
        Expression<Func<T, bool>>? predicate = null,
        params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;

        // Apply includes
        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        // Apply predicate if provided
        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        return await query.ToListAsync();
    }

    // Get entity by ID
    public async Task<T?> GetByIdAsync(TKey id)
    {
        return await _dbSet.FindAsync(id);
    }

    // Add a new entity
    public async Task<T> AddAsync(T entity)
    {
        _dbSet.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    // Update an existing entity
    public async Task<T> UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    // Delete an entity by ID
    public async Task<bool> DeleteAsync(TKey id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null) return false;

        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}
