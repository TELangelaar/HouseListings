using HouseListing.DataAccess.Contexts;
using HouseListing.Domain.Events;
using HouseListing.Domain.Mappers;
using HouseListing.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HouseListing.Domain.Repositories;

public class HouseListingRepository : IRepositoryBase<Listing>, IHouseListingRepository
{
    private readonly ILogger<HouseListingRepository> _logger;
    private readonly HouseListingContext _dbContext;
    public HouseListingRepository(ILogger<HouseListingRepository> logger, HouseListingContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }
    public void Create(Listing entity)
    {
        _logger.LogInformation(LogEvents.AddEntity, "Add entity: {@entity}", entity);
        _dbContext.Listings.Add(entity.ToListingDto());
    }

    public void Delete(Listing entity)
    {
        _logger.LogInformation(LogEvents.DeleteEntity, "Delete entity: {@entity}", entity);
        _dbContext.Remove(entity);
    }

    public IQueryable<Listing> FindAll()
    {
        return _dbContext.Listings.Select(item => item.ToListing()).AsNoTracking();
    }

    public Listing? FindById(Guid id)
    {
        _logger.LogInformation(LogEvents.FindEntity, "Finding entity by id: {id}", id);
        return _dbContext.Listings.Where(x => x.Id == id).Select(item => item.ToListing()).FirstOrDefault();
    }

    public void Update(Listing entity)
    {
        _logger.LogInformation(LogEvents.UpdateEntity, "Update entity: {@entity}", entity);
        _dbContext.Listings.Update(entity.ToListingDto());
    }
}
