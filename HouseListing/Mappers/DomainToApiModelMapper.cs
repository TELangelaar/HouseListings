using HouseListing.ApiModel;
using HouseListing.Domain.Models;

namespace HouseListing.Mappers;

public static class DomainToApiModelMapper
{
    public static UserResponse ToUserResponse(this User user)
    {
        return new UserResponse
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email
        };
    }
}
