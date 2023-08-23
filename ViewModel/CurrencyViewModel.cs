using CryptoInfo.Model;
using Newtonsoft.Json;
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
