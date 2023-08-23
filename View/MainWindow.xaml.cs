using CryptoInfo.ViewModel;
using System.Windows;

namespace CryptoInfo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new CurrencyViewModel();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(DataContext is  CurrencyViewModel viewModel)
            {
                viewModel.InitCurrencies.Execute(null);
            }
        }
    }
}
