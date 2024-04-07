using Back_end_API.Entites;
using Microsoft.EntityFrameworkCore;

namespace Back_end_API.Context
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> option) : base(option) { }
        public AppDbContext() { }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Comments> Commments { get; set; }
        public DbSet<ConfirmEmail> Confirmemails { get; set; }
        public DbSet<Locations> Locations { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Posts> Posts { get; set; }
        public DbSet<RefeshToken> RefeshTokens { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> Roomtypes { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
