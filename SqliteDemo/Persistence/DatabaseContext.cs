using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SqliteDemo.Persistence.Entities;
using System.Data;
using System.Data.Common;

namespace SqliteDemo.Persistence
{
    public sealed class DatabaseContext : DbContext
    {
        private readonly DbConnection _connection;

        public DbSet<PersistedAccount> Accounts { get; set; }

        public DbSet<PersistedFill> Fills { get; set; }

        public DatabaseContext(DbConnection connection, bool ownsConnection = true)
        {
            _connection = connection;
            Initialize();
        }

        public DatabaseContext()
        {
            Initialize();
        }

        public void Initialize()
        {
            // Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                options.UseSqlite(_connection);
                options.LogTo(message => System.Diagnostics.Debug.WriteLine(message));
            }
            else
            {
                options.UseSqlite("Data Source=:memory:;");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PersistedAccount>().ToTable("Accounts");
            builder.Entity<PersistedAccount>().Property((PersistedAccount _) => _.Id).HasColumnName("Id")
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder.Entity<PersistedAccount>().HasKey((PersistedAccount _) => _.Id);
            builder.Entity<PersistedAccount>().Property((PersistedAccount _) => _.Type).HasColumnName("Type")
                .IsRequired();
            builder.Entity<PersistedAccount>().Property((PersistedAccount _) => _.Name).HasColumnName("Name")
                .IsRequired()
                .IsFixedLength(fixedLength: false);
            builder.Entity<PersistedAccount>().HasMany((PersistedAccount _) => _.Strategies).WithOne((PersistedStrategy _) => _.Account)
                .HasForeignKey((PersistedStrategy _) => _.AccountId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<PersistedAccount>().HasMany((PersistedAccount _) => _.Fills).WithOne((PersistedFill _) => _.Account)
                .HasForeignKey((PersistedFill _) => _.AccountId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<PersistedStrategy>().ToTable("Strategies");
            builder.Entity<PersistedStrategy>().Property((PersistedStrategy _) => _.Id).HasColumnName("Id")
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder.Entity<PersistedStrategy>().HasKey((PersistedStrategy _) => _.Id);
            builder.Entity<PersistedStrategy>().Property((PersistedStrategy _) => _.Name).HasColumnName("Name")
                .IsRequired()
                .IsFixedLength(fixedLength: false);
            builder.Entity<PersistedStrategy>().Property((PersistedStrategy _) => _.AssetPath).HasColumnName("Asset")
                .IsRequired()
                .IsFixedLength(fixedLength: false);
            builder.Entity<PersistedStrategy>().Property((PersistedStrategy _) => _.Comment).HasColumnName("Comment")
                .IsFixedLength(fixedLength: false);
            builder.Entity<PersistedStrategy>().HasIndex((PersistedStrategy s) => new { s.Name, s.AccountId, s.AssetPath }).HasName("IX_StrategyUnique")
                .IsUnique();
            builder.Entity<PersistedFill>().ToTable("Fills");
            builder.Entity<PersistedFill>().Property((PersistedFill _) => _.Id).HasColumnName("Id")
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder.Entity<PersistedFill>().HasKey((PersistedFill _) => _.Id);
            builder.Entity<PersistedFill>().Property((PersistedFill _) => _.ExchangeId).HasColumnName("ExchangeId")
                .IsRequired()
                .IsFixedLength(fixedLength: false);
            builder.Entity<PersistedFill>().Property((PersistedFill _) => _.ExchangeOrderId).HasColumnName("ExchangeOrderId")
                .IsFixedLength(fixedLength: false);
            builder.Entity<PersistedFill>().Property((PersistedFill _) => _.InstrumentPath).HasColumnName("Instrument")
                .IsRequired()
                .IsFixedLength(fixedLength: false);
            builder.Entity<PersistedFill>().Property((PersistedFill _) => _.AssetPath).HasColumnName("Asset")
                .IsRequired()
                .IsFixedLength(fixedLength: false);
            builder.Entity<PersistedFill>().Property((PersistedFill _) => _.StrategyName).HasColumnName("Strategy")
                .IsFixedLength(fixedLength: false);
            builder.Entity<PersistedFill>().HasOne((PersistedFill _) => _.Account).WithMany((PersistedAccount _) => _.Fills)
                .HasForeignKey((PersistedFill _) => _.AccountId);
            builder.Entity<PersistedFill>().Property((PersistedFill _) => _.Price).HasColumnName("Price")
                .IsRequired();
            builder.Entity<PersistedFill>().Property((PersistedFill _) => _.Quantity).HasColumnName("Quantity")
                .IsRequired();
            builder.Entity<PersistedFill>().Property((PersistedFill _) => _.Type).HasColumnName("Type")
                .IsRequired();
        }
    }
}
