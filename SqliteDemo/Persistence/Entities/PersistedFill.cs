using SqliteDemo.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteDemo.Persistence.Entities
{
    public class PersistedFill : PersistedEntity, IEquatable<PersistedFill>
    {
        public int AccountId { get; set; }

        public string ExchangeId { get; set; }

        public string ExchangeOrderId { get; set; }

        public DateTime Timestamp { get; set; }

        public string AssetPath { get; set; }

        public string InstrumentPath { get; set; }

        public string StrategyName { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public PersistedFillType Type { get; set; }

        public PersistedAccount Account { get; set; }

        public bool Equals(PersistedFill other)
        {
            if ((object)other == null)
            {
                return false;
            }
            if ((object)this != other)
            {
                if (AccountId == other.AccountId && string.Equals(AssetPath, other.AssetPath) && string.Equals(InstrumentPath, other.InstrumentPath) && string.Equals(StrategyName, other.StrategyName) && string.Equals(ExchangeId, other.ExchangeId) && Type == other.Type && Price == other.Price)
                {
                    return Quantity == other.Quantity;
                }
                return false;
            }
            return true;
        }
    }
}
