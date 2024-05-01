using SqliteDemo.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteDemo.Persistence.Entities
{
    public enum PersistedFillType
    {
        Undefined = 0,
        External = 1,
        Manual = 2,
        Adjustment = 3
    }
}
