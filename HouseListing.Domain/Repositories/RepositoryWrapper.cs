using HouseListing.DataAccess.Contexts;
using HouseListing.Domain.Events;
using Microsoft.Extensions.Logging;

namespace HouseListing.Domain.Repositories;

public class RepositoryWrapper : IRepositoryWrapper
{
    private readonly ILogger<RepositoryWrapper> _logger;
    private readonly HouseListingContext _dbContext;
    public IHouseListingRepository HouseListing { get; }
    public IUserRepository User { get; }

    public RepositoryWrapper(ILogger<RepositoryWrapper> logger, IHouseListingRepository houseListing, IUserRepository userRepository, HouseListingContext dbContext) 
    {
        _logger = logger;
        _dbContext = dbContext;
        HouseListing = houseListing;
        User = userRepository;
    }
    public bool Save()
    {
        if (!_dbContext.ChangeTracker.HasChanges()) 
        {
            _logger.LogWarning(LogEvents.SaveWithoutChanges, "Method called but no changes are tracked");
            return false;
        }

        _logger.LogInformation(LogEvents.SaveChanges, "Saving changes");
        var result = _dbContext.SaveChanges();

        if (result < 1)
        {
            _logger.LogWarning(LogEvents.SaveFailed, "No entries were written to the database");
            return false;
        }
            
        _logger.LogInformation(LogEvents.SaveSuccessful, "Entries written to database: {result}", result);
        return true;
    }
}
