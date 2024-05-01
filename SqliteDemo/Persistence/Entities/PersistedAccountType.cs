using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteDemo.Persistence.Entities
{
    public enum PersistedAccountType
    {
        Undefined = 0,
        // виртуальный счет с виртуальными сделками
        Virtual = 1,
        // реальный или демо счет
        Trade = 2
    }
}
