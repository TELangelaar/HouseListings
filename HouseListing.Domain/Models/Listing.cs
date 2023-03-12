namespace HouseListing.Domain.Models;

public class Listing
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public int Price { get; set; } = default!;
}
