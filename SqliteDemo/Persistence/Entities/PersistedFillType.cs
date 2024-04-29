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
        External = 69,
        Manual = 77,
        Adjustment = 65
    }
}
