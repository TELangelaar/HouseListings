namespace HouseListing.DataAccess.Models;

public class ListingDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public int Price { get; set; } = default!;
}
