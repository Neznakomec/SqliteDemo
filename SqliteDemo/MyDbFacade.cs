using Microsoft.EntityFrameworkCore;
using SqliteDemo.Persistence.Entities;

namespace SqliteDemo
{
    public sealed class DatabaseContext : DbContext
    {
        public DbSet<PersistedFill> Fills { get; set; }
    }
}
