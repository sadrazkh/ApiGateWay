using AutoMapper;
using Common;
using Common.Exceptions;
using Data.Entities.User;
using Data.Repositories.UsersRepositories;
using DNTPersianUtils.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Contract;
using Services.Dtos.Authentication;
using Services.WebFramework.Api;

namespace ApiGateWay.Controllers.V1;

[ApiVersion("1")]
[AllowAnonymous]
public class AuthenticationController : BaseController
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IJwtService _jwtService;
    private readonly IMapper _mapper;

    public AuthenticationController(UserManager<User> userManager, SignInManager<User> signInManager, IJwtService jwtService, IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtService = jwtService;
        _mapper = mapper;
    }

    [HttpPost("Register")]
    public async Task<ApiResult> Register(RegisterDto dto, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(dto);
        try
        {
            //var hashedToken = _passwordHasher.HashPassword(user, dto.Password);
            //user.PasswordHash = hashedToken;
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, dto.Password);
            var res = await _userManager.CreateAsync(user);
            if (res.Succeeded)
                return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        return BadRequest();
    }


    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ApiResult<AccessToken>> Login(LoginDto dto, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(dto.UserName);

        if (user == null)
        {
            throw new NotFoundException("نام کاربری یا رمز عبور اشتباه است");
        }
        else
        {
            var result = await _signInManager.PasswordSignInAsync(
                dto.UserName, dto.Password, dto.RememberMe, true);
            if (result.Succeeded)
            {
                var token = await _jwtService.GenerateAsync(user);
                return token;
            }
            else if (result.IsLockedOut)
            {
                throw new NotFoundException("دسترسی شما محدود شده است لطفا با پشتیبان تماس بگیرید");
            }
            else
            {
                throw new NotFoundException("نام کاربری یا رمز عبور اشتباه است");
            }
        }
    }
}
