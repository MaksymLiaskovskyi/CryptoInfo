using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoInfo.Model
{
    public class CurrencyHistoryData
    {
        public ObservableCollection<CurrencyHistory> Data { get; set; }
    }
}
