namespace ViajaBrasil.Domain.Interfaces;

public interface IRepository<T>
{
    IUnitOfWork UnitOfWork { get; }
    
    Task AddAsync(T entity);

    Task UpdateAsync(T entity);

    void Remove(T entity);
}