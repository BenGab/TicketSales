using Microsoft.EntityFrameworkCore;
using TicketShop.DB.Models;

namespace TicketShop.DB
{
    public class TicketDbContext : DbContext
    {
        public DbSet<Venue> Venues { get; set; }

        public DbSet<Sector> Sectors { get; set; }

        public DbSet<Seller> Sellers { get; set; }

        public TicketDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string connStr = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\StadiumDB.mdf; Integrated Security = True;MultipleActiveResultSets=true";
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseSqlServer(connStr);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sector>(entity =>
            {
                entity.HasOne(sector => sector.Venue).WithMany(venue => venue.Sectors);
            });

            modelBuilder.Entity<Seller>(entity =>
            {
                entity.HasOne(seller => seller.Venue).WithMany(venue => venue.Sellers);
            });

            Venue bs = new Venue() { Id = 1, Name = "Papp Laszlo Arena" };
            Sector sectorA = new Sector() { Id = 1, Code = "A", Capacity = 1100, VenueId = bs.Id };
            Sector sectorB = new Sector() { Id = 2, Code = "B", Capacity = 2000, VenueId = bs.Id };
            Sector sectorC = new Sector() { Id = 3, Code = "C", Capacity = 1500, VenueId = bs.Id };
            Seller sellerA = new Seller() { Id = 1, Name = "Broadway", VenueId = bs.Id };
            Seller sellerB = new Seller() { Id = 2, Name = "Eventim", VenueId = bs.Id };

            modelBuilder.Entity<Venue>().HasData(bs);
            modelBuilder.Entity<Sector>().HasData(sectorA, sectorB, sectorC);
            modelBuilder.Entity<Seller>().HasData(sellerA, sellerB);
        }
    }
}
