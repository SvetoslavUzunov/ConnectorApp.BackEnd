using Domain.Common.Constants;
using Domain.Models.User;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[Route(WebConstants.ControllerRoute)]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService userService;

    public UserController(IUserService userService)
       => this.userService = userService;

    [HttpGet(WebConstants.ActionRouteId)]
    public async Task<UserModel> GetById(Guid id)
       => await userService.GetByIdAsync(id);

    [HttpGet(WebConstants.ActionRoute)]
    public async Task<IEnumerable<UserModel>> GetAll()
       => await userService.GetAllAsync();

    [HttpPost(WebConstants.ActionRoute)]
    public async Task<UserModel> Create([FromBody] UserModel user)
       => await userService.CreateAsync(user);

    [HttpPut(WebConstants.ActionRoute)]
    public async Task<UserModel> Edit([FromBody] UserModel user)
       => await userService.EditAsync(user);

    [HttpDelete(WebConstants.ActionRouteId)]
    public async Task DeleteById(Guid id)
       => await userService.DeleteByIdAsync(id);
}
