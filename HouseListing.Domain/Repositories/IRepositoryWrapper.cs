namespace HouseListing.Domain.Repositories
{
    public interface IRepositoryWrapper
    {
        IHouseListingRepository HouseListing { get; }
        IUserRepository User { get; }
        bool Save();
    }
}