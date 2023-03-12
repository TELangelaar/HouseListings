namespace HouseListing.ApiModel;

public class ListingResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public int Price { get; set; } = default!;
}
