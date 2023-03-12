using HouseListing.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace HouseListing.DataAccess.Contexts;

public class HouseListingContext : DbContext
{
    public DbSet<ListingDto> Listings { get; set; }
    public DbSet<UserDto> Users { get; set; }

    public HouseListingContext(DbContextOptions<HouseListingContext> dbContextOptions) : base(dbContextOptions) { }
}