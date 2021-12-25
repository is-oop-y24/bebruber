using Bebruber.Domain.Entities;
using Bebruber.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Bebruber.DataAccess
{
    public class BebruberDatabaseContext : DbContext
    {
        public BebruberDatabaseContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Client> Clients { get; private set; } = null!;
        public DbSet<PaymentInfo> PaymentInfos { get; private set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureClient(modelBuilder);
            ConfigurePaymentInfo(modelBuilder);
        }

        private static void ConfigureClient(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().OwnsOne(c => c.Name);
            modelBuilder.Entity<Client>().OwnsOne(nameof(Rating), c => c.Rating);
            modelBuilder.Entity<Client>().OwnsOne(c => c.PaymentAddress);
            modelBuilder.Entity<Client>().Navigation(c => c.PaymentInfos).HasField("_paymentInfos");
        }

        private static void ConfigurePaymentInfo(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaymentInfo>().OwnsOne(nameof(CardNumber), i => i.CardNumber);
            modelBuilder.Entity<PaymentInfo>().OwnsOne(nameof(CvvCode), i => i.CvvCode);
            modelBuilder.Entity<PaymentInfo>().OwnsOne(i => i.ExpirationDate);
        }
    }
}