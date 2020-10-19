using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthLib.Attribute;
using AuthLib.Controller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Server1.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class HomeController : OAuthController {

        [HttpGet, Authorize, OAuthManage]
        public ActionResult Get() {
            return Ok("授权成功");
        }
    }
}
