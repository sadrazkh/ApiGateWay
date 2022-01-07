using System.ComponentModel.DataAnnotations;
using Data.Entities.User;
using Services.Services.WebFramework.Api;

namespace Services.Dtos.Authentication;

public class RegisterDto : BaseDto<RegisterDto, User, int>
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    public string Password { get; set; }
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
}