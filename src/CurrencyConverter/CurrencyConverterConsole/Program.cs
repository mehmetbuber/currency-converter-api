using System;
using System.Linq;
using CurrencyConverter;
using CurrencyConverter.Enums;

namespace CurrencyConverterConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Converter object
            var premiumConverter = new Converter("[YOUR_API_KEY]");
            var converter = new Converter();

            //List of currencies
            var currencies = converter.GetAllCurrencies();
            Console.WriteLine(string.Join(",", currencies.Select(p => p.Id).ToArray()));

            //List of countries
            var countries = converter.GetAllCountries();
            Console.WriteLine(string.Join(",", countries.Select(p => p.Name).ToArray()));

            ////Basic conversion
            //var basic = converter.Convert(1, CurrencyType.USD, CurrencyType.EUR);
            //Console.WriteLine(basic);

            ////History Single Date
            //var history = converter.GetHistory(CurrencyType.USD, CurrencyType.EUR, "2018-08-01");
            //Console.WriteLine(history.Date);
            //Console.WriteLine(history.ExchangeRate);

            ////History Date Range
            //var historyRange = converter.GetHistoryRange(CurrencyType.USD, CurrencyType.EUR, "2018-08-01", "2018-08-06");
            //Console.WriteLine(string.Join(",", historyRange.Select(p => p.Date).ToArray()));
            //Console.WriteLine(string.Join(",", historyRange.Select(p => p.ExchangeRate).ToArray()));

            //Async Functions
            //GetAllCurrenciesAsync(converter);
            //GetAllCountriesAsync(converter);
            //ConvertAsync(converter);
            //GetHistoryAsync(converter);
            //GetHistoryRangeAsync(converter);

            Console.Read();
        }

        //List of currencies Async
        private static async void GetAllCurrenciesAsync(Converter converter)
        {
            var result = await converter.GetAllCurrenciesAsync();
            Console.WriteLine(string.Join(",", result.Select(p => p.Id).ToArray()));
        }

        //List of currencies Async
        private static async void GetAllCountriesAsync(Converter converter)
        {
            var result = await converter.GetAllCountriesAsync();
            Console.WriteLine(string.Join(",", result.Select(p => p.Name).ToArray()));
        }

        //Basic Conversion Async
        private static async void ConvertAsync(Converter converter)
        {
            var result = await converter.ConvertAsync(1, CurrencyType.USD, CurrencyType.EUR);
            Console.WriteLine("Exchange rate : {0}", result);
        }

        //History Single Date Async
        private static async void GetHistoryAsync(Converter converter)
        {
            var history = await converter.GetHistoryAsync(CurrencyType.USD, CurrencyType.EUR, "2018-08-01");
            Console.WriteLine(history.Date);
            Console.WriteLine(history.ExchangeRate);
        }

        //History Date Range Async
        private static async void GetHistoryRangeAsync(Converter converter)
        {
            var historyRange = await converter.GetHistoryRangeAsync(CurrencyType.USD, CurrencyType.EUR, "2018-08-01", "2018-08-06");
            Console.WriteLine(string.Join(",", historyRange.Select(p => p.Date).ToArray()));
            Console.WriteLine(string.Join(",", historyRange.Select(p => p.ExchangeRate).ToArray()));
        }
    }
}
