using Microsoft.EntityFrameworkCore;
using RouteeFailoverMessages.Domain.Models;

namespace RouteeFailoverMessages.Data
{
    public class RouteeDbContext : DbContext
    {
        public RouteeDbContext(DbContextOptions<RouteeDbContext> options) : base(options) { }

        public DbSet<Failover_Message_Request> Requests { get; set; }
        public DbSet<Failover_Message_Response> Responses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Failover_Message_Request>()
            //    .HasOne(r => r.Response)
            //    .WithOne(res => res.Request)
            //    .HasForeignKey<Failover_Message_Response>(res => res.RequestId)
            //    .OnDelete(DeleteBehavior.Cascade);.
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RouteeDbContext).Assembly);
            modelBuilder.Entity<Failover_Message_Request>().HasNoKey();
            modelBuilder.Entity<Failover_Message_Response>().HasNoKey();
            modelBuilder.Entity<Flow>().HasNoKey();
            modelBuilder.Entity<Callback>().HasNoKey();
            modelBuilder.Entity<Message>().HasNoKey();
            modelBuilder.Entity<Domain.Models.Action>().HasNoKey();


        }
    }
}
