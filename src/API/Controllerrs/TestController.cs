using AspNet.Security.OpenIdConnect.Extensions;
using AspNet.Security.OpenIdConnect.Primitives;
using AspNet.Security.OpenIdConnect.Server;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    public class TestController : Controller
    {
            [Authorize, HttpGet("~/api/test")]
            public IActionResult GetMessage()
            {
                return Json(new { Subject = User.GetClaim(OpenIdConnectConstants.Claims.Subject), User.Identity.Name });
            }
        }
    }
