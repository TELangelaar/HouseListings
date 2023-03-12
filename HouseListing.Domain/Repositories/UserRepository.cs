using HouseListing.DataAccess.Contexts;
using HouseListing.Domain.Events;
using HouseListing.Domain.Mappers;
using HouseListing.Domain.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
namespace HouseListing.Domain.Repositories;

public class UserRepository : IRepositoryBase<User>, IUserRepository
{
    private readonly ILogger<UserRepository> _logger;
    private readonly HouseListingContext _dbContext;

    public UserRepository(ILogger<UserRepository> logger, HouseListingContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public void Create(User entity)
    {
        _logger.LogInformation(LogEvents.AddEntity, "Add entity: {@entity}", entity);
        _dbContext.Users.Add(entity.ToUserDto());
    }

    public void Delete(User entity)
    {
        _logger.LogInformation(LogEvents.DeleteEntity, "Delete entity: {@entity}", entity);
        _dbContext.Remove(entity);
    }

    public IQueryable<User> FindAll()
    {
        return _dbContext.Users.Select(item => item.ToUser()).AsNoTracking();
    }

    public User? FindById(Guid id)
    {
        _logger.LogInformation(LogEvents.FindEntity, "Finding entity by id: {id}", id);
        return _dbContext.Users.Where(x => x.Id == id).Select(item => item.ToUser()).FirstOrDefault();
    }

    public void Update(User entity)
    {
        _logger.LogInformation(LogEvents.UpdateEntity, "Update entity: {@entity}", entity);
        _dbContext.Users.Update(entity.ToUserDto());
    }
}