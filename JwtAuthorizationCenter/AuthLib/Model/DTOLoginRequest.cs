using System;
using System.Collections.Generic;
using System.Text;

namespace AuthLib.Model {

    /// <summary>
    /// 测试登陆
    /// </summary>
    public class DTOLoginRequest {
        
        public int UserId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
