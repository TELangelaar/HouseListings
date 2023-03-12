using HouseListing.Domain.Models;

namespace HouseListing.Domain.Services;

public interface IListingService
{
    bool Create(Listing listing);
    Listing Get(Guid id);
    ICollection<Listing> GetAll();
    bool Update(Listing listing);
    bool Delete(Guid id);
}
