using Microsoft.Extensions.Logging;

namespace HouseListing.Domain.Events;

public static class LogEvents
{
    public static readonly EventId AddEntity = new(101, "Added item to DbSet");
    public static readonly EventId DeleteEntity = new(102, "Delete item from DbSet");
    public static readonly EventId UpdateEntity = new(103, "Update item in DbSet");
    public static readonly EventId FindEntity = new(104, "Find item in DbSet");
    
    public static readonly EventId SaveChanges = new(105, "Saving changes to Database");
    public static readonly EventId SaveSuccessful = new(205, "Saving changes successful");
    public static readonly EventId SaveFailed = new(5050, "Saving changes failed");
    public static readonly EventId SaveWithoutChanges = new(5051, "No entries eligible for save");

    public static readonly EventId GetUserStarted = new(1001, "Get User request started");
    public static readonly EventId GetUserSuccessful = new(2001, "Get User successful");
    public static readonly EventId GetUserFailed = new(5001, "Get User failed");

    public static readonly EventId GetListingStarted = new(1002, "Get User request started");
    public static readonly EventId GetListingSuccessful = new(2002, "Get Listing successful");
    public static readonly EventId GetListingFailed = new(5002, "Get Listing failed");

}
