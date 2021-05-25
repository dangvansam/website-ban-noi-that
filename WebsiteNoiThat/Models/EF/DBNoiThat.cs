namespace Models.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBNoiThat : DbContext
    {
        public DBNoiThat() : base("name=DBNoiThat")
        {
        }
        public virtual DbSet<Card> Cards { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Credential> Credentials { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Provider> Providers { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserGroup> UserGroups { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //    modelBuilder.Entity<User>()
            //        .Property(e => e.Password)
            //        .IsFixedLength();

            //    modelBuilder.Entity<UserGroup>()
            //        .Property(e => e.Name)
            //        .IsFixedLength();
        }

        //public System.Data.Entity.DbSet<WebsiteNoiThat.Models.ProductViewModel> ProductViewModels { get; set; }
    }
}
