using SqliteDemo.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteDemo.Persistence.Entities
{
    public class PersistedStrategy : PersistedEntity
    {
        public int AccountId { get; set; }

        public string Name { get; set; } = string.Empty;


        public string AssetPath { get; set; } = string.Empty;


        public string? Comment { get; set; }

        public PersistedAccount Account { get; set; }
    }
}
