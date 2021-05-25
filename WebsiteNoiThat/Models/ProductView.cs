using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
   public class ProductView
    {
        public int OrderDetailId1 { get; set; }

        public int? OrderId { get; set; }

        public int? ProductId { get; set; }

        public int? Price { get; set; }

        public int? Quantity { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public string Photo { get; set; }

        [Column(TypeName = "date")]
        public DateTime? StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }

        public int? Discount { get; set; }



    }
}
