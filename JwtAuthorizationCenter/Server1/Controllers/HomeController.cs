using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Server1.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase {

        [HttpGet,Authorize]
        public ActionResult Get() {
            return Ok("授权成功");
        }
    }
}
