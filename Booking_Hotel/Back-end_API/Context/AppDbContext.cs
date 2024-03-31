using Back_end_API.Entites;
using Microsoft.EntityFrameworkCore;

namespace Back_end_API.Context
{
    public class AppDbContext:DbContext
    {
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Comments> Commments { get; set; }
        public DbSet<ConfirmEmail> Confirmemails { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Posts> Posts { get; set; }
        public DbSet<RefeshToken> RefeshTokens { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> Roomtypes { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectSQL = "Server = LAPTOP-CUATAO; initial catalog = Booking_Hotel_DB; integrated security = sspi; trustservercertificate = true;";
            optionsBuilder.UseSqlServer(connectSQL);
        }
    }
}
