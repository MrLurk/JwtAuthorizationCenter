using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Linq;
using System.Text;
using AuthLib.Cache.Redis;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using AuthLib.Extensions;
using AuthLib.Model;

namespace AuthLib.Auth
{
    public class TokenAuthenticationService : IAuthenticateService
    {
        private readonly TokenManagement _tokenManagement = null;
        public TokenAuthenticationService(IOptions<TokenManagement> tokenManagement)
        {
            _tokenManagement = tokenManagement.Value;
        }

        /// <summary>
        /// 获取 Token
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool IsAuthenticated(DTOLoginRequest request, out string token)
        {
            token = string.Empty;
            if (request.Username != "admin" || request.Password != "123456")
                return false;
            request.UserId = 1;

            var claims = new[]
            {
                new Claim(ClaimTypes.Name,request.Username),
                new Claim(ClaimTypes.NameIdentifier,request.UserId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(
                _tokenManagement.Issuer,
                _tokenManagement.Audience, claims,
                expires: DateTime.Now.AddMinutes(_tokenManagement.AccessExpiration),
                signingCredentials: credentials
                );
            token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            RedisHelper.Set(RedisPrefix.User_Login_Token_Key.GetKey(request.UserId), token, _tokenManagement.AccessExpiration * 60);
            return true;
        }


        /// <summary>
        /// Token 刷新
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool IsAuthenticated(IEnumerable<Claim> claims, out string token)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(
                _tokenManagement.Issuer,
                _tokenManagement.Audience, claims,
                expires: DateTime.Now.AddMinutes(_tokenManagement.AccessExpiration),
                signingCredentials: credentials
                );

            token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            RedisHelper.Set(RedisPrefix.User_Login_Token_Key.GetKey(claims.GetUserId()), token, _tokenManagement.AccessExpiration * 60);
            return true;
        }
    }
}
