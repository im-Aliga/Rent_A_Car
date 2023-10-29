using CarRent.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options)
        : base(options)
        {
        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Message> Messages { get; set; }

        public DbSet<User> Users { get; set; }


    }
}
