using CryptoInfo.Model;
using CryptoInfo.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace CryptoInfo.View
{
    /// <summary>
    /// Логика взаимодействия для CurrencyInfoWindow.xaml
    /// </summary>
    public partial class CurrencyInfoWindow : Window
    {
        private Currency selectedCurrency;
        public CurrencyInfoWindow()
        {
            InitializeComponent();
            DataContext = new CurrencyViewModel();
        }

        public CurrencyInfoWindow(Currency currency)
        {
            InitializeComponent();
            selectedCurrency = currency;
            NameTextBlock.Text = currency.Name;
            PriceTextBlock.Text = "$" + currency.PriceUsd.ToString("0.####");
            ChangesTextBlock.Text = (currency.PriceUsd * currency.ChangePercent24Hr / 100).ToString("0.##") + "(" + currency.ChangePercent24Hr.ToString("0.####") + "%)";
            if (currency.ChangePercent24Hr < 0)
                ChangesTextBlock.Foreground = Brushes.Red;
            else
                ChangesTextBlock.Foreground = Brushes.Green;
            MarketCapTextBlock.Text = currency.MarketCapUsd.ToString("0.##");
            SupplyTextBlock.Text = currency.Supply.ToString("0.##");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is CurrencyViewModel viewModel)
            {
                var args = new Dictionary<string, object>
                {
                    { "id", selectedCurrency.Id },
                    { "page", 1 }
                };
                viewModel.InitCurrencyMarket.Execute(args) ;


                args = new Dictionary<string, object>{
                    { "id", selectedCurrency.Id },
                    { "from", DateTime.UtcNow.AddDays(-1) },
                    { "interval", "h1" }
                };
                viewModel.InitCurrencyHistory.Execute(args);
                
            }
        }
    }
}
