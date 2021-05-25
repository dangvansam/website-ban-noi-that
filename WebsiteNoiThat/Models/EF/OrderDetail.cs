namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderDetail")]
    public partial class OrderDetail
    {
        [DisplayName("Mã chi tiết hoá đơn")]
        public int OrderDetailId { get; set; }
        [DisplayName("Mã hoá đơn")]
        public int? OrderId { get; set; }
        [DisplayName("Mã sản phẩm")]
        public int? ProductId { get; set; }
        [DisplayName("Đơn giá")]
        public int? Price { get; set; }
        [DisplayName("Số lượng")]
        public int? Quantity { get; set; }
    }
}
