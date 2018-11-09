using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyConverter;
using CurrencyConverter.Enums;
using CurrencyConverter.Helpers;

namespace CurrencyConverterConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //Converter object
            Converter converter = new Converter();

            //List of currencies
            var currencies = converter.GetAllCurrencies();
            Console.WriteLine(String.Join(",", currencies.Select(p => p.id).ToArray()));

            //Basic conversion
            double basic = converter.Convert(1, CurrencyType.USD, CurrencyType.TRY);
            Console.WriteLine(basic);

            //History Single Date
            var history = converter.GetHistory(CurrencyType.USD, CurrencyType.TRY, "2018-08-01");
            Console.WriteLine(history.Date);
            Console.WriteLine(history.ExchangeRate);

            //History Date Range
            var historyRange = converter.GetHistoryRange(CurrencyType.USD, CurrencyType.TRY, "2018-08-01", "2018-08-06");
            Console.WriteLine(String.Join(",", historyRange.Select(p => p.Date).ToArray()));
            Console.WriteLine(String.Join(",", historyRange.Select(p => p.ExchangeRate).ToArray()));

            Console.Read();
        }
    }
}
