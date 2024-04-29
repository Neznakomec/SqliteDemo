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
        public string Name { get; set; } = string.Empty;


        public PersistedAccountType Type { get; set; }

        public ICollection<PersistedStrategy> Strategies { get; set; } = new List<PersistedStrategy>();


        public ICollection<PersistedFill> Fills { get; set; } = new List<PersistedFill>();
    }
}
