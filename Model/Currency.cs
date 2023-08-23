using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace CryptoInfo.Model
{
    public class Currency: INotifyPropertyChanged
    {
        private string id;
        private int rank;
        private string symbol;
        private string name;
        private double supply;
        private double? maxSupply;
        private double marketCapUsd;
        private double volumeUsd24Hr;
        private double priceUsd;
        private double changePercent24Hr;
        private double vwap24Hr;

        public string Id { get { return id; } set {  id = value; OnPropertyChanged("Id"); } }
        public int Rank { get { return rank; } set { rank = value; OnPropertyChanged("Rank"); } }
        public string Symbol { get { return symbol; } set { symbol = value; OnPropertyChanged("Symbol"); } }
        public string Name { get { return name; } set { name = value; OnPropertyChanged("Name"); } }
        public double Supply { get { return supply; } set { supply = value; OnPropertyChanged("Supply"); } }
        public double? MaxSupply { get { return maxSupply; } set { maxSupply = value; OnPropertyChanged("MaxSupply"); } }
        public double MarketCapUsd { get { return marketCapUsd; } set { marketCapUsd = value; OnPropertyChanged("MarketCapUsd"); } }
        public double VolumeUsd24Hr { get { return volumeUsd24Hr; } set { volumeUsd24Hr = value; OnPropertyChanged("VolumeUsd24Hr"); } }
        public double PriceUsd { get { return priceUsd; } set { priceUsd = value; OnPropertyChanged("PriceUsd"); } }
        public double ChangePercent24Hr { get { return changePercent24Hr; } set { changePercent24Hr = value; OnPropertyChanged("ChangePercent24Hr"); } }
        public double Vwap24Hr { get { return vwap24Hr; } set { vwap24Hr = value; OnPropertyChanged("Vwap24Hr"); } }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
