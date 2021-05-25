using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteNoiThat.Areas.Admin.Models
{
    public class OrderView
    {
        [DisplayName("Mã hoá đơn")]
        public int OrderId { get; set; }

        [DisplayName("Mã khách hàng")]
        public int? UserId { get; set; }

        [StringLength(50)]
        [DisplayName("Tên khách hàng")]
        public string ShipName { get; set; }

        [DisplayName("SĐT Người nhận")]
        public int? ShipPhone { get; set; }

        [DisplayName("Địa chỉ nhận hàng")]
        public string ShipAddress { get; set; }

        [DisplayName("Email người nhận")]
        public string ShipEmail { get; set; }
        
        //[DisplayName("Tổng tiền")]
        //public string Total { get; set; }
        
        [DisplayName("Trạng thái đơn hàng")]
        public string StatusName { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.Column(TypeName = "date")]
        [DisplayName("Ngày cập nhật")]
        public DateTime? UpdateDate { get; set; }
    }
}