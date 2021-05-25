using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace WebsiteNoiThat.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Mời bạn nhập username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Mời bạn nhập passwword")]
        public string Passwword { get; set; }
        public bool Remember { get; set; }

    }
}