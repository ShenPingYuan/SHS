using System;
using System.Collections.Generic;
using System.Text;

namespace SHS.Dtos
{
    public class UserRegisterDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string VerifyCode { get; set; }
    }
}
