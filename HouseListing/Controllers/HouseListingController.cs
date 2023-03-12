using Microsoft.AspNetCore.Mvc;
using HouseListing.ApiModel;
using HouseListing.Domain.Services;

namespace HouseListing.Controllers;

[ApiController]
public class HouseListingController : ControllerBase
{

    private readonly ILogger<HouseListingController> _logger;
    private readonly IListingService _listingService;

    public HouseListingController(ILogger<HouseListingController> logger, IListingService listingService)
    {
        _logger = logger;
        _listingService = listingService;
    }

    [ProducesResponseType(typeof(ListingsResponse), StatusCodes.Status200OK)]
    [HttpGet("listings")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        using (_logger.BeginScope("Retrieving all listings"))
        {
            var listings = _listingService.GetAll();
            return Ok(listings);
        }
    }

    [ProducesResponseType(typeof(ListingResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("listings/{id:guid}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        using (_logger.BeginScope("Provided guid: {guid}", id))
        {
            var listing = _listingService.Get(id);
            return Ok(listing);
        }
    }
}