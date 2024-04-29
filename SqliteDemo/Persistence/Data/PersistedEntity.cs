using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteDemo.Persistence.Data
{
    public abstract class PersistedEntity
    {
        public int Id { get; set; }
    }
}