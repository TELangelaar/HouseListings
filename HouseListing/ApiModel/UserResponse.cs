﻿namespace HouseListing.ApiModel;

public class UserResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
}
