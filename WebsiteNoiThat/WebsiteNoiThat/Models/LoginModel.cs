using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteNoiThat.Models
{
    public class LoginModel
    {
       
        [DisplayName("Tên đăng nhập")]
        [Required(ErrorMessage = "Bạn phải nhập tài khoản!")]
        public string UserName { get; set; }

        [DisplayName("Mật khẩu")]
        [Required(ErrorMessage = "Bạn phải nhập mật khẩu!")]
        public string Password { get; set; }
    }
}