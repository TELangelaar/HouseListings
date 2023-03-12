using HouseListing.Domain.Models;

namespace HouseListing.Domain.Services;

public interface IUserService 
{
    bool Create(User user);
    User Get(Guid id);
    ICollection<User> GetAll();
    bool Update(User user);
    bool Delete(Guid id);
}