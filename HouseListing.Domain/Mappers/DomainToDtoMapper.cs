using HouseListing.DataAccess.Models;
using HouseListing.Domain.Models;

namespace HouseListing.Domain.Mappers;

public static class DomainToDtoMapper
{
    public static UserDto ToUserDto(this User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            Name = user.Name
        };
    }

    public static ListingDto ToListingDto(this Listing listing)
    {
        return new ListingDto
        {
            Id = listing.Id,
            Title = listing.Title,
            Price = listing.Price,
        };
    }
}
