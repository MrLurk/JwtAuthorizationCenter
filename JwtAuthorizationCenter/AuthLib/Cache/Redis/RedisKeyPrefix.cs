using System;
namespace AuthLib.Cache.Redis
{
    public class RedisPrefix
    {
        private static string Master { get; } = "Auth_Center:";

        /// <summary>
        /// 用户登陆 Token 信息 {用户Id}
        /// </summary>
        //public static string User_Login_Token_Key { get; } = $"{Master}API:USER_LOGIN_TONKEN:{0}";

        public static RedisPrefixItem User_Login_Token_Key = new RedisPrefixItem($"{Master}API:USER_LOGIN_TONKEN:{{0}}");
    }

    public class RedisPrefixItem
    {
        internal RedisPrefixItem(string key)
        {
            this.Key = key;
        }
        private string Key { get; }

        public string GetKey(params object?[] args)
        {
            return string.Format(this.Key, args);
        }
    }
}
