using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoInfo.Model
{
    public class CurrencyData
    {
        public ObservableCollection<Currency> Data { get; set; }
    }
}
