using SqliteDemo.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteDemo.Persistence.Entities
{
    public class PersistedAccount : PersistedEntity
    {
        // имя торгового счёта
        public string Name { get; set; } = string.Empty;

        // тип торгового счёта
        public PersistedAccountType Type { get; set; }

        // список Стратегий
        public ICollection<PersistedStrategy> Strategies { get; set; } = new List<PersistedStrategy>();

        // список сделок
        public ICollection<PersistedFill> Fills { get; set; } = new List<PersistedFill>();
    }
}
