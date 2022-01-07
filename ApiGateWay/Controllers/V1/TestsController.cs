using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.WebFramework.Api;


namespace Web.Controllers.V1
{
    [ApiVersion("1")]
    [AllowAnonymous]
    public class TestsController : BaseController
    {
        private readonly IWebHostEnvironment _env;

        [HttpGet]
        public ApiResult Get()
        {
            var dard = User.Identity;
            var test = User.IsInRole("Admin");
            return Ok(new [] { "It's Safe And Sound" });
        }
    }
}
