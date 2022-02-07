using Data.Entities.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProfileService.Controllers;


[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private static readonly string[] names = new[]
    {
        "Sadra", "Ali", "Mohammad", "Ehsan", "Younes", "AmirReza", "Amir", "Reza", "Hossein", "Hasan"};

    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    [Authorize(Roles = "test")]
    public IEnumerable<User> Get(int id)
    {
        var claim = Request.HttpContext.Items;
        var dard = User.Identity;
        var dard2 = User.Identities;

        var res = Request.Headers.Where(c => c.Key == "Role")
            .Select(c => c.Value).First().ToString().Split(",").ToList();

        return Enumerable.Range(1, 5).Select(index => new User
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Name = names[Random.Shared.Next(names.Length)]
        })
        .ToList();
    }
}
