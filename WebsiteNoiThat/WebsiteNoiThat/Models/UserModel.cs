using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteNoiThat.Models
{
    public class UserModel
    {
        [Required]
        [DisplayName("Mã")]
        public int UserId { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("Tên")]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Địa chỉ")]
        public string Address { get; set; }

        [Required]
        [DisplayName("Số điện thoại")]
        public int? Phone { get; set; }

        [Required]
        [StringLength(10)]
        [DisplayName("Tên đăng nhập")]
        public string Username { get; set; }

        [Required]
        [StringLength(32)]
        [DisplayName("Mật khẩu")]
        public string Password { get; set; }
      
        [Display(Name = "Email")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "The email address is not valid")]
        public string Email { get; set; }

        [Required]
        [DisplayName("Nhóm người dùng")]
        public string GroupId { get; set; }

        [Required]
        [DisplayName("Trạng thái")]
        public bool Status { get; set; }
      
        
    }
}