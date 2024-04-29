using Microsoft.EntityFrameworkCore;
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
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                options.UseSqlite(_connection);
            }
            else
            {
                options.UseSqlite("Data Source=:memory:;");
            }
        }
    }
}
