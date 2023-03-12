using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace HouseListing.Infrastructure.Authentication;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IConfiguration _configuration;
    public BasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options, 
        ILoggerFactory loggerFactory, 
        UrlEncoder encoder, 
        ISystemClock clock, 
        IConfiguration configuration) : base(options, loggerFactory, encoder, clock)
    {
        _configuration = configuration;
    }
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
        {
            return AuthenticateResult.NoResult();
        }

        string username;
        string password;

        try
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var credentialBytes = Convert.FromBase64String(authHeader.Parameter!);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(":");
            username = credentials[0];
            password = credentials[1];
        } catch
        {
            return AuthenticateResult.Fail("Invalid Authorization Header");
        }

        if (!AreValidCredentials(username, password)) 
        {
            return AuthenticateResult.Fail("Invalid Username or Password");
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, 1.ToString()),
            new Claim(ClaimTypes.Name, username)
        };

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return await Task.FromResult(AuthenticateResult.Success(ticket));
    }

    private bool AreValidCredentials(string username, string password)
    {
        return username == _configuration.GetValue<string>("BasicAuthentication:Username")
            && password == _configuration.GetValue<string>("BasicAuthentication:Password");
    }
}
