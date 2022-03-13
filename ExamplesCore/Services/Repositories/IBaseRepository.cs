namespace ExamplesCore.Services.Repositories;

public interface IBaseRepository<T>
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<T>> ListAllAsync();
    Task<T> AddAndSaveAsync(T entity);
    Task UpdateAndSaveAsync(T entity);
    Task DeleteAndSaveAsync(T entity);
    Task<T> AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}