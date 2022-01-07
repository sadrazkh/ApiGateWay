using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Services.WebFramework.Api
{
    [Route("api/v{version:apiVersion}/Admin/[controller]")]
    [Authorize(Roles = "Admin,SupporterAdminOne,SupporterAdminTwo")]
    public class AdminBaseController : BaseController
    {
    }
}
