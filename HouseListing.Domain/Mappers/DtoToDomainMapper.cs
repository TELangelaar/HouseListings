using HouseListing.DataAccess.Models;
using HouseListing.Domain.Models;

namespace HouseListing.Domain.Mappers;

public static class DtoToDomainMapper
{
    public static User ToUser(this UserDto userDto)
    {
        return new User
        {
            Id = userDto.Id,
            Email = userDto.Email,
            Name = userDto.Name
        };
    }

    public static Listing ToListing(this ListingDto listingDto)
    {
        return new Listing
        {
            Id = listingDto.Id,
            Title = listingDto.Title,
            Price = listingDto.Price
        };
    }

}