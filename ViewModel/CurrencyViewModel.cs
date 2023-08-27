using CryptoInfo.Model;
using CryptoInfo.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace CryptoInfo.ViewModel
{
    class CurrencyViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Currency> Currencies { get; set; } = new ObservableCollection<Currency>();
        public ObservableCollection<CurrencyMarket> Markets { get; set; } = new ObservableCollection<CurrencyMarket>();
        public ObservableCollection<CurrencyHistory> History { get; set; } = new ObservableCollection<CurrencyHistory>();

        private RelayCommand openInfoCommand;
        public RelayCommand OpenInfoCommand
        {
            get
            {
                return openInfoCommand ?? (
                    openInfoCommand = new RelayCommand(obj =>
                    {
                        var CurInfoWindow = new CurrencyInfoWindow((Currency)obj);
                        CurInfoWindow.ShowDialog();
                    }));
            }
        }

        private RelayCommand initCurrencyHistory;
        public RelayCommand InitCurrencyHistory
        {
            get
            {
                return initCurrencyHistory ?? (
                    initCurrencyHistory = new RelayCommand(async obj =>
                    {
                        if (obj is Dictionary<string, object> args &&
                        args.TryGetValue("id", out var idObj) && idObj is string id &&
                        args.TryGetValue("from", out var fromObj) && fromObj is DateTime from &&
                        args.TryGetValue("interval", out var intervalObj) && intervalObj is string interval)
                        {
                            await GetCurrencyHistoryAsync(id, from, interval);
                        }
                    }));
            }
        }

        private RelayCommand initCurrencyMarket;
        public RelayCommand InitCurrencyMarket
        {
            get
            {
                return initCurrencyMarket ?? (
                    initCurrencyMarket = new RelayCommand(async obj =>
                    {
                        if (obj is Dictionary<string, object> args &&
                        args.TryGetValue("id", out var idObj) && idObj is string id &&
                        args.TryGetValue("page", out var pageObj) && pageObj is int page)
                            await GetCurrencyMarketAsync(id, page);
                    }));
            }
        }

        private RelayCommand initCurrencies;
        public RelayCommand InitCurrencies
        {
            get {
                return initCurrencies ??
                    (initCurrencies = new RelayCommand(async obj =>
                    {
                        await GetCurrenciesAsync();
                    }));
                }
        }

        public async Task GetCurrenciesAsync()
        {
            HttpClient client = new HttpClient();
            string apiUrl = "https://api.coincap.io/v2/assets?limit=10";
            CurrencyData? currencyData;
            HttpResponseMessage response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();

                currencyData = JsonConvert.DeserializeObject<CurrencyData>(responseBody);

                if(currencyData != null)
                {
                    foreach (var currency in currencyData.Data)
                    {
                        Currencies.Add(currency);
                    }
                }
            }
        }

        public async Task GetCurrencyMarketAsync(string id, int page)
        {
            HttpClient client = new HttpClient();
            string apiUrl = $"https://api.coincap.io/v2/assets/{id}/markets?limit=5&offset={page * 5}";
            CurrencyMarketData? currencyMarketData;
            HttpResponseMessage response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();

                currencyMarketData = JsonConvert.DeserializeObject<CurrencyMarketData>(responseBody);

                if(Markets != null)
                {
                    foreach(var market in currencyMarketData.Data)
                    {
                        Markets.Add(market);
                    }
                }
            }
        }

        public async Task GetCurrencyHistoryAsync(string id, DateTime from, string interval)
        {
            HttpClient client = new HttpClient();
            string apiUrl = $"https://api.coincap.io/v2/assets/{id}/history?interval={interval}&start={(long)(from - new DateTime(1970, 1, 1)).TotalMilliseconds}&end={(long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds}";
            CurrencyHistoryData? currencyHistoryData;
            HttpResponseMessage response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();

                currencyHistoryData = JsonConvert.DeserializeObject<CurrencyHistoryData>(responseBody);

                if(currencyHistoryData != null)
                {
                    foreach(var h in currencyHistoryData.Data)
                    {
                        History.Add(h);
                    }
                }
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
