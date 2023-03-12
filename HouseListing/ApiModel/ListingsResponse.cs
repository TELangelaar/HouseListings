namespace HouseListing.ApiModel;

public class ListingsResponse
{
    public ICollection<ListingResponse> Data { get; set; } = default!;
}
