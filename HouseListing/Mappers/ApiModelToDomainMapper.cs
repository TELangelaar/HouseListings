using HouseListing.ApiModel;
using HouseListing.Domain.Models;

namespace HouseListing.Mappers;

public static class ApiModelToDomainMapper
{
    public static User ToUser(this UserRequest userRequest)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            Email = userRequest.Email,
            Name = userRequest.Name
        };
    }
}
