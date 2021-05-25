namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Card")]
    public partial class Card
    {
        public int CardId { get; set; }

        public int? NumberCard { get; set; }

        public int? UserNumber { get; set; }

        public int? UserId { get; set; }

        public int? Identification { get; set; }
    }
}
