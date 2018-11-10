using System;
using System.Linq;
using CurrencyConverter;
using CurrencyConverter.Enums;

namespace CurrencyConverterConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //Converter object
            var premiumConverter = new Converter("[YOUR_API_KEY]");
            var converter = new Converter();

            //List of currencies
            var currencies = converter.GetAllCurrencies();
            Console.WriteLine(String.Join(",", currencies.Select(p => p.id).ToArray()));

            //List of countries
            var countries = converter.GetAllCountries();
            Console.WriteLine(String.Join(",", countries.Select(p => p.name).ToArray()));

            //Basic conversion
            double basic = converter.Convert(1, CurrencyType.USD, CurrencyType.EUR);
            Console.WriteLine(basic);

            //History Single Date
            var history = converter.GetHistory(CurrencyType.USD, CurrencyType.EUR, "2018-08-01");
            Console.WriteLine(history.Date);
            Console.WriteLine(history.ExchangeRate);

            //History Date Range
            var historyRange = converter.GetHistoryRange(CurrencyType.USD, CurrencyType.EUR, "2018-08-01", "2018-08-06");
            Console.WriteLine(String.Join(",", historyRange.Select(p => p.Date).ToArray()));
            Console.WriteLine(String.Join(",", historyRange.Select(p => p.ExchangeRate).ToArray()));

            //Async Functions
            GetAllCurrenciesAsync(converter);
            GetAllCountriesAsync(converter);
            ConvertAsync(converter);
            GetHistoryAsync(converter);
            GetHistoryRangeAsync(converter);

            Console.Read();
        }

        //List of currencies Async
        static async void GetAllCurrenciesAsync(Converter converter)
        {
            var result = await converter.GetAllCurrenciesAsync();
            Console.WriteLine(String.Join(",", result.Select(p => p.id).ToArray()));
        }

        //List of currencies Async
        static async void GetAllCountriesAsync(Converter converter)
        {
            var result = await converter.GetAllCountriesAsync();
            Console.WriteLine(String.Join(",", result.Select(p => p.name).ToArray()));
        }

        //Basic Conversion Async
        static async void ConvertAsync(Converter converter)
        {
            var result = await converter.ConvertAsync(1, CurrencyType.USD, CurrencyType.EUR);
            Console.WriteLine("Exchange rate : {0}", result);
        }

        //History Single Date Async
        static async void GetHistoryAsync(Converter converter)
        {
            var history = await converter.GetHistoryAsync(CurrencyType.USD, CurrencyType.EUR, "2018-08-01");
            Console.WriteLine(history.Date);
            Console.WriteLine(history.ExchangeRate);
        }

        //History Date Range Async
        static async void GetHistoryRangeAsync(Converter converter)
        {
            var historyRange = await converter.GetHistoryRangeAsync(CurrencyType.USD, CurrencyType.EUR, "2018-08-01", "2018-08-06");
            Console.WriteLine(String.Join(",", historyRange.Select(p => p.Date).ToArray()));
            Console.WriteLine(String.Join(",", historyRange.Select(p => p.ExchangeRate).ToArray()));
        }
    }
}
