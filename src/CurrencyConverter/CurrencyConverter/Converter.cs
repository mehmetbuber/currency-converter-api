using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyConverter.Enums;
using CurrencyConverter.Helpers;
using CurrencyConverter.Models;

namespace CurrencyConverter
{
    public class Converter
    {
        public double Convert(double amount, CurrencyType from, CurrencyType to)
        {
            return RequestHelper.ExchangeRate(from, to) * amount;
        }

        public List<Currency> GetAllCurrencies()
        {
            return RequestHelper.GetAllCurrencies();
        }

        public List<CurrencyHistory> GetHistory(CurrencyType from, CurrencyType to, string startDate, string endDate)
        {
            return RequestHelper.GetRange(from, to, startDate, endDate);
        }
    }
}
