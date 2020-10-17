using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using AuthLib.Cache.Redis;
using AuthLib.Extensions;

namespace AuthLib.Auth
{
    /// <summary>
    /// 自定义 JWT Token 校验
    /// </summary>
    public class CustomJwtSecurityTokenHandler : JwtSecurityTokenHandler
    {
        public CustomJwtSecurityTokenHandler()
        {
        }

        public override ClaimsPrincipal ValidateToken(string token, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
        {
            var principal = base.ValidateToken(token, validationParameters, out validatedToken);

            //通过Redis验证Token
            if (!IsTokenActive(principal, token))
            {
                throw new Exception("登陆权限校验失败！");
            }
            return principal;
        }

        public bool IsTokenActive(ClaimsPrincipal principal, string token)
        {
            //解析ClaimsPrincipal取出UserId
            //具体的验证步骤有两个：
            //- 到Redis的黑名单里判断是否存在该Token；

            var userId = principal.GetUserId();
            var redisToken = RedisHelper.Get(RedisPrefix.User_Login_Token_Key.GetKey(userId));
            if (string.IsNullOrEmpty(redisToken) || token != redisToken)
            {
                return false;
            }

            return true;
        }
    }
}