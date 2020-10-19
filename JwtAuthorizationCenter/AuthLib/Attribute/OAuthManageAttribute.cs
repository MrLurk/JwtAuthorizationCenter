using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;
using AuthLib.Controller;
using Microsoft.Extensions.DependencyInjection;
using AuthLib.Common;

namespace AuthLib.Attribute {



    public class OAuthManageAttribute : ActionFilterAttribute {


        public override void OnActionExecuting(ActionExecutingContext context) {

            var http = context.HttpContext;
            var method = http.Request.Method;
            var url = http.Request.Path.ToString().ToLower();

            var Configuration = ServiceLocator.Instance.GetService<IConfiguration>();
            if (context.Filters.Any(item => item is OAuthSkipAttribute)) return;

            // Ip 校验
            var SecurityIP = Configuration["SysConfig:SecurityIP"].Split(',');
            if (SecurityIP.Contains(http.Connection.RemoteIpAddress.ToString()))
                return;

            // 调试模式
            var isDebug = Convert.ToBoolean(Configuration["SysConfig:DebugMode"]);
            if (isDebug) return;

            // 检查用户
            //var user = ((Controllers.CoreController)context.Controller).user;
            //if (user == null) {
            //    context.Result = new JsonResult(new { code = "err", msg = "请重新登录" });
            //    return;
            //}



            //if (user.Powers.FirstOrDefault(a => a.PowerMethod == method && a.PowerUrl == url) == null)
            //    context.Result = new JsonResult(new { code = "err", msg = "您无权访问" });
        }
    }
}
