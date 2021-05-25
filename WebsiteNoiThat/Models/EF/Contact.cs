namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Contact")]
    public partial class Contact
    {
        public int ContactId { get; set; }

        public string Content { get; set; }

        public bool? Status { get; set; }

        [StringLength(50)]
        public string EmailCC { get; set; }
    }
}
