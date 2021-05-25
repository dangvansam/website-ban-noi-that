namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Provider")]
    public partial class Provider
    {
        public int ProviderId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public string Address { get; set; }

        public int? Phone { get; set; }
    }
}
