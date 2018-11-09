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
        public List<Tuple<string, string>> Conversions { get; set; }

        public Converter()
        {
            Conversions = new List<Tuple<string, string>>();
        }

        public void AddConversion(CurrencyType from, CurrencyType to)
        {
            Conversions.Add(Tuple.Create(from.ToString(), to.ToString()));
        }

        public double Convert(double amount, CurrencyType from, CurrencyType to)
        {
            return RequestHelper.ExchangeRate(from, to) * amount;
        }

        public List<Currency> GetAllCurrencies()
        {
            return RequestHelper.GetAllCurrencies();
        }

        public CurrencyHistory GetHistory(CurrencyType from, CurrencyType to, DateTime date)
        {
            return RequestHelper.GetHistory(from, to, date.ToString("yyyy-MM-dd"));
        }

        public CurrencyHistory GetHistory(CurrencyType from, CurrencyType to, string date)
        {
            return RequestHelper.GetHistory(from, to, date);
        }

        public List<CurrencyHistory> GetHistoryRange(CurrencyType from, CurrencyType to, DateTime startDate, DateTime endDate)
        {
            return RequestHelper.GetHistoryRange(from, to, startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"));
        }

        public List<CurrencyHistory> GetHistoryRange(CurrencyType from, CurrencyType to, string startDate, string endDate)
        {
            return RequestHelper.GetHistoryRange(from, to, startDate, endDate);
        }
    }
}
