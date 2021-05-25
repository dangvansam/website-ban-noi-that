namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Role")]
    public partial class Role
    {
        [StringLength(50)]
        [Display(Name = "Mã chức năng")]
        public string RoleId { get; set; }

        [StringLength(50)]
        [Display(Name = "Mô tả")]
        public string Name { get; set; }
    }
}
