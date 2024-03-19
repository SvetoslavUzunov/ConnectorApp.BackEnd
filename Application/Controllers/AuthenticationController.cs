using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Domain.Models.User;
using Domain.Common.Constants;
using Infrastructure.Services;

namespace Application.Controllers;

[Route(WebConstants.ControllerRoute)]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
       => this.authenticationService = authenticationService;

    [HttpPost(WebConstants.ActionRoute)]
    public async Task<UserLoginModel> Register([FromBody] UserRegistrationModel registrationModel)
       => await authenticationService.RegisterAsync(registrationModel);

    [HttpPost(WebConstants.ActionRoute)]
    public async Task<TokenModel> Login([FromBody] UserLoginModel loginModel)
       => await authenticationService.LoginAsync(loginModel);

    [HttpPost(WebConstants.ActionRoute)]
    public string Logout([FromBody] string refreshToken)
       => authenticationService.Logout(refreshToken);

    [HttpPost(WebConstants.ActionRoute)]
    public async Task<TokenModel> RefreshToken([FromBody] string refreshToken)
       => await authenticationService.RefreshTokenAsync(refreshToken);
}
