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
        // ID торгового счета
        public int AccountId { get; set; }

        // ID сделки
        public string ExchangeId { get; set; }

        // ID заявки
        public string ExchangeOrderId { get; set; }

        // Время совершения сделки
        public DateTime Timestamp { get; set; }

        // Имя базового актива
        public string AssetPath { get; set; }

        // Полный код торгового инструмента
        public string InstrumentPath { get; set; }

        // Имя стратегии, в которую записана сделка
        public string StrategyName { get; set; }

        // цена сделки
        public decimal Price { get; set; }

        // количество контрактов в сделке
        public int Quantity { get; set; }

        // тип сделки
        public PersistedFillType Type { get; set; }

        // счет, на котором совершена сделка
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
