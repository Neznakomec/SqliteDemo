using SqliteDemo.Persistence;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SqliteDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Hello");
            var p = new PositionsRepository();
            p.InitializeAsync();

            // Пример: Добавить счета, если их нет
            p.StoreAsync(new Persistence.Entities.PersistedAccount()
            {
                Id = 1,
                Name = "My First Account FN12345",
                Type = Persistence.Entities.PersistedAccountType.Trade,
            });
            p.StoreAsync(new Persistence.Entities.PersistedAccount()
            {
                Name = "New account created at " + DateTime.Now.ToString("HH:mm"),
                Type = Persistence.Entities.PersistedAccountType.Trade,
            });

            // Пример: добавить стратегию
            p.StoreAsync(new Persistence.Entities.PersistedStrategy()
            {
                AccountId = 1,
                Id = 1,
                Name = "default",
                AssetPath = "FORTS/Futures/Si",

            });
            //p.StoreAsync(new Persistence.Entities.PersistedFill()
            //{
            //    AccountId = 1,

            //    ExchangeId = "SampleDealId like 12345678",
            //    ExchangeOrderId = "SampleOrderId like 87654321",
            //    AssetPath = "FORTS/Futures/Si",
            //    InstrumentPath = "SiH4",
            //    StrategyName = "default",
            //});
        }
    }
}