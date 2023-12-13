using FashionHexa.Entities;
using Microsoft.EntityFrameworkCore;


namespace FashionHexa.Database
{
    public class MyContext : DbContext
    {
        private IConfiguration config;
        public MyContext(IConfiguration cfg) 
        {
            config = cfg;
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        //public DbSet<Role> Roles { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<UserRatings> UserRatings { get; set; }
        public DbSet<Cart> carts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data source = DESKTOP-FA8GT7U\\SQLEXPRESS; Initial Catalog=FashionProjectHexaware1 ;User Id=sathwik;Password=1234;TrustServerCertificate=true");
            base.OnConfiguring(optionsBuilder);
        }


    }
}
