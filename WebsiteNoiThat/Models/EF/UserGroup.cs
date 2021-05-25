namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserGroup
    {
        [Key]
        [StringLength(50)]
        [Display(Name = "Mã nhóm người dùng")]
        public string GroupId { get; set; }

        [StringLength(10)]
        [Display(Name = "Tên mô tả")]
        public string Name { get; set; }
    }
}
