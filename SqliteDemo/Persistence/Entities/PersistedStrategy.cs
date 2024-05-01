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
        // ID торгового счета
        public int AccountId { get; set; }

        // имя стратегии
        public string Name { get; set; } = string.Empty;

        // Имя базового актива
        public string AssetPath { get; set; } = string.Empty;

        // писание к стратегии, необязательное поле
        public string? Comment { get; set; }

        // счет, на котором заведена стратегия
        public PersistedAccount Account { get; set; }
    }
}
