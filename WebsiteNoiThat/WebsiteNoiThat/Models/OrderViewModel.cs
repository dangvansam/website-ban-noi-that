using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebsiteNoiThat.Models
{
    public class OrderViewModel
    {
        public int? OrderId { get; set; }
        public int? UserId { get; set; }

        [StringLength(50)]
        public string ShipName { get; set; }


        public int? ShipPhone { get; set; }

        public string ShipEmail { get; set; }

        public int? StatusId { get; set; }
        public string StatusName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UpdateDate { get; set; }
        public string ShipAddress { get; set; }
        public int OrderDetailId1 { get; set; }
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public int? Price { get; set; }
        public int? Quantity { get; set; }
        [StringLength(50)]
        public int? Discount { get; set; }
       

    }
}