namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Credential
    {
        [Key]
        public int CredenId { get; set; }

        [StringLength(50)]
        [Display(Name = "Nhóm người dùng")]
        public string UserGroupId { get; set; }

        [StringLength(50)]
        [Display(Name = "Chức năng")]
        public string RoleId { get; set; }
    }
}
