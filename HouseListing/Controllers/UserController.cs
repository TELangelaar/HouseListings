using HouseListing.ApiModel;
using HouseListing.Domain.Events;
using HouseListing.Domain.Services;
using HouseListing.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace HouseListing.Controllers;

[ApiController]
public class UserController : ControllerBase
{

    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;

    public UserController(ILogger<UserController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [HttpGet("users/{id:guid}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        using (_logger.BeginScope("Provided guid: {guid}", id))
        {
            _logger.LogInformation(LogEvents.GetUserStarted, "Processing request started");
            return Ok(_userService.Get(id).ToUserResponse());
        }
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("users")]
    public async Task<IActionResult> Post([FromBody] UserRequest userRequest, CancellationToken cancellationToken)
    {
        using (_logger.BeginScope("Provided user: {@user}", userRequest))
        {
            _logger.LogInformation(LogEvents.GetUserStarted, "Processing request started");
            var user = userRequest.ToUser();
            _userService.Create(user);
            return Created($"/users/{user.Id}", null);
        }
    }
}
