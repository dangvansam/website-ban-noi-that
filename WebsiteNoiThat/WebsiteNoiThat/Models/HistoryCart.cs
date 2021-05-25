using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebsiteNoiThat.Models
{
    public class HistoryCart
    {
        public int OrderDetaiId { get; set; }
        public int? ProductId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
        public int? Price { get; set; }
        public int? Quantity { get; set; }
        public int? StatusId { get; set; }
        public string NameStatus { get; set; }
        public string Photo { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Discount { get; set; }
    }
}