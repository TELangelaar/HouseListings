using System.Linq.Expressions;

namespace HouseListing.Domain.Repositories;

public interface IRepositoryBase<T>
{
    IQueryable<T> FindAll();
    T? FindById(Guid id);
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
}