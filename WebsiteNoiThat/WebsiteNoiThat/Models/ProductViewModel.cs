using Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebsiteNoiThat.Models
{
    public class ProductViewModel
    {
        [Key]
        public int ProductId { get; set; }

        [StringLength(50)]
        [Required]
        [DisplayName("Tên sản phẩm")]
        public string Name { get; set; }

        [DisplayName("Mô tả sản phẩm")]
        public string Description { get; set; }

        [DisplayName("Đơn giá")]
        [Required]
        public int? Price { get; set; }

        [Required]
        [DisplayName("Số lượng")]
        public int? Quantity { get; set; }

        [DisplayName("Nhà cung cấp")]
        public int? ProviderId { get; set; }

        [DisplayName("Danh mục sản phẩm")]
        public int? CateId { get; set; }

        [DisplayName("Danh mục sản phẩm")]
        public string CateName { get; set; }

        [DisplayName("Nhà cung cấp")]
        public string ProviderName { get; set; }

        [DisplayName("Ảnh")]
        [Required]
        public string Photo { get; set; }

        [DisplayName("Ngày bắt đầu KM")]
        [Column(TypeName = "date")]
        public DateTime? StartDate { get; set; }

        [DisplayName("Ngày kết thúc KM")]
        [Column(TypeName = "date")]
        //[DateGreaterThanAttribute(otherPropertyName = "StartDate", ErrorMessage = "Ngày kết thúc phải muộn hơn ngày bắt đầu")]
        public DateTime? EndDate { get; set; }

        [DisplayName("Giảm giá (%)")]
        public int? Discount { get; set; }
    }
}