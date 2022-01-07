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
            return Ok(new [] { "It's Safe And Sound" });
        }
    }
}
