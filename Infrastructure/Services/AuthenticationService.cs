﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Domain.Models;
using Domain.Models.User;
using Domain.Entities;
using Domain.Common.Exceptions;
using Domain.Common.Constants;

namespace Infrastructure.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<UserEntity> userManager;
    private readonly ITokenHandlerService tokenHandlerService;
    private readonly IMemoryCache memoryCache;

    public AuthenticationService(
       UserManager<UserEntity> userManager,
       ITokenHandlerService tokenHandlerService,
       IMemoryCache memoryCache)
    {
        this.userManager = userManager;
        this.tokenHandlerService = tokenHandlerService;
        this.memoryCache = memoryCache;
    }

    public async Task<UserLoginModel> RegisterAsync(UserRegistrationModel registrationModel)
    {
        var user = new UserEntity
        {
            UserName = registrationModel.UserName,
            Email = registrationModel.Email,
        };

        var isUserCreated = await userManager.CreateAsync(user, registrationModel.Password);

        if (!isUserCreated.Succeeded)
        {
            ErrorHandler.ExecuteErrorHandler(isUserCreated);
        }

        var isUserAddedToRole = await userManager.AddToRoleAsync(user, RoleConstants.AdminRole);

        if (!isUserAddedToRole.Succeeded)
        {
            ErrorHandler.ExecuteErrorHandler(isUserAddedToRole);
        }

        return new UserLoginModel
        {
            UserName = registrationModel.UserName,
            Email = registrationModel.Email,
            Password = registrationModel.Password
        };
    }

    public async Task<TokenModel> LoginAsync(UserLoginModel loginModel)
    {
        var user = await userManager.FindByEmailAsync(loginModel.Email);

        if (user == null)
        {
            throw new ValidationException(UserConstants.UserNotFound);
        }

        if (!await userManager.CheckPasswordAsync(user, loginModel.Password))
        {
            throw new ValidationException(ResponseConstants.UnauthorizedAccess);
        }

        var tokenModel = await tokenHandlerService.GenerateToken(user);

        var roles = await userManager.GetRolesAsync(user);

        return new TokenModel
        {
            AccessToken = tokenModel.AccessToken,
            RefreshToken = tokenModel.RefreshToken,
            Email = loginModel.Email,
            Role = roles[0]
        };
    }

    public string Logout(string refreshToken)
    {
        memoryCache.Remove(refreshToken);

        return ResponseConstants.SuccessLogout;
    }

    public async Task<TokenModel> RefreshTokenAsync(string refreshToken)
    {
        var userId = tokenHandlerService.ValidateRefreshToken(refreshToken);

        var user = await userManager.FindByIdAsync(userId);

        if (user == null)
        {
            throw new UserNotFoundException();
        }

        return await tokenHandlerService.GenerateToken(user);
    }
}
