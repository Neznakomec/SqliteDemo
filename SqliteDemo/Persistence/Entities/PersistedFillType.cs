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
        // сделка из внешней системы
        External = 1,
        // сделка записана вручную (виртуальная сделка)
        Manual = 2,
        // (не используется) сделки корректировки совокупной позиции к определенной цене или количеству
        Adjustment = 3
    }
}
