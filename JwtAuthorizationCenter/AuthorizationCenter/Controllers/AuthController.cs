using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthLib.Model;
using AuthLib.Cache.Redis;
using AuthLib.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AuthLib.Auth;
using Microsoft.Extensions.Configuration;

namespace AuthorizationCenter.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase {

        private readonly IAuthenticateService _authService;
        private readonly IConfiguration Configuration;
        public AuthController(IAuthenticateService authService, IConfiguration configuration) {
            this._authService = authService;
            Configuration = configuration;
        }

        [HttpGet]
        public ActionResult Get() {
            return Ok(1);
        }


        [HttpPost, Route("RequestToken")]
        public ActionResult RequestToken(DTOLoginRequest request) {
            if (_authService.IsAuthenticated(request, out string token)) {
                return Ok("Bearer " + token);
            }
            return BadRequest("Invalid Request");
        }

        [HttpGet, Route("RefreshToken"), Authorize]
        public ActionResult RefreshToken() {
            var ttl = RedisHelper.Ttl(RedisPrefix.User_Login_Token_Key.GetKey(User.GetUserId()));
            if (ttl <= Configuration.GetValue<int>("TokenManagement:RefreshExpiration") * 60) {
                _authService.IsAuthenticated(User.Claims, out string token);
                return Ok(new { ttl, refreshToken = "Bearer " + token });
            }
            return Ok(new { ttl, refreshToken = string.Empty });
        }

        public ActionResult GetUser() {
            return Ok(new { Name = "", Power = new string[] { "1", "2" } });
        }
    }
}
