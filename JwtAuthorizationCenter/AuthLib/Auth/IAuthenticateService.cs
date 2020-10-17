using AuthLib.Model;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace AuthLib.Auth
{
    public interface IAuthenticateService
    {
        /// <summary>
        /// 用于用户登陆发 Token
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        bool IsAuthenticated(DTOLoginRequest request, out string token);

        /// <summary>
        /// 用于用户登陆状态续期
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        bool IsAuthenticated(IEnumerable<Claim> claims, out string token);
    }
}
