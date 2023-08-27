using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CryptoInfo.Model
{
    public class CurrencyMarket: INotifyPropertyChanged
    {
        private string exchangeId;
        private string baseId;
        private string quoteId;
        private string baseSymbol;
        private string quoteSymbol;
        private double volumeUsd24Hr;
        private double priceUsd;
        private double volumePercent;

        public string ExchangeId { get { return exchangeId; } set { exchangeId = value; OnPropertyChanged("ExchangeId"); } }
        public string BaseId { get { return baseId; } set { baseId = value; OnPropertyChanged("BaseId"); } }
        public string QuoteId { get { return quoteId; } set { quoteId = value; OnPropertyChanged("QuoteId"); } }
        public string BaseSymbol { get { return baseSymbol; } set { baseSymbol = value; OnPropertyChanged("BaseSymbol"); } }
        public string QuoteSymbol { get { return quoteSymbol; } set { quoteSymbol = value; OnPropertyChanged("QuoteSymbol"); } }
        public double VolumeUsd24Hr { get { return volumeUsd24Hr; } set { volumeUsd24Hr = value; OnPropertyChanged("VolumeUsd24Hr"); } }
        public double PriceUsd { get { return priceUsd; } set { priceUsd = value; OnPropertyChanged("PriceUsd"); } }
        public double VolumePercent { get { return volumePercent; } set { volumePercent = value; OnPropertyChanged("VolumePercent"); } }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
