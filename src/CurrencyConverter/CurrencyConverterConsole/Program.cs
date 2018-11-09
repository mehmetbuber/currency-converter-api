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
            //var list = converter.GetAllCurrencies();
            //Console.WriteLine(String.Join(",", list.Select(p => p.id).ToArray()));

            //Basic conversion
            //double result = converter.Convert(1, CurrencyType.USD, CurrencyType.TRY);
            //Console.WriteLine(result);

            var list = converter.GetHistory(CurrencyType.USD, CurrencyType.TRY, "2018-08-01", "2018-08-06");
            Console.WriteLine(String.Join(",", list.Select(p => p.Date).ToArray()));
            Console.WriteLine(String.Join(",", list.Select(p => p.ExchangeRate).ToArray()));

            Console.Read();
        }
    }
}
