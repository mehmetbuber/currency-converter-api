using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using CurrencyConverter.Enums;
using CurrencyConverter.Models;
using Newtonsoft.Json.Linq;

namespace CurrencyConverter.Helpers
{
    public static class RequestHelper
    {
        public const string FreeBaseUrl = "https://free.currencyconverterapi.com/api/v6/";
        public const string PremiumBaseUrl = "https://api.currencyconverterapi.com/api/v6/";

        public static List<Currency> GetAllCurrencies(string apiKey = null)
        {
            string url;
            if (string.IsNullOrEmpty(apiKey))
                url = FreeBaseUrl + "currencies";
            else
                url = PremiumBaseUrl + "currencies" + "?apiKey=" + apiKey;

            var jsonString = GetResponse(url);

            var data = JObject.Parse(jsonString)["results"].ToArray();

            return data.Select(item => new Currency
            {
                id = item.First["id"]?.ToString(),
                currencyName = item.First["currencyName"]?.ToString(),
                currencySymbol = item.First["currencySymbol"]?.ToString()
            }).ToList();
        }

        public static List<Country> GetAllCountries(string apiKey = null)
        {
            string url;
            if (string.IsNullOrEmpty(apiKey))
                url = FreeBaseUrl + "countries";
            else
                url = PremiumBaseUrl + "countries" + "?apiKey=" + apiKey;

            var jsonString = GetResponse(url);

            var data = JObject.Parse(jsonString)["results"].ToArray();

            return data.Select(item => new Country
            {
                id = item.First["id"]?.ToString(),
                currencyName = item.First["currencyName"]?.ToString(),
                currencySymbol = item.First["currencySymbol"]?.ToString(),
                currencyId = item.First["currencySymbol"]?.ToString(),
                alpha3 = item.First["alpha3"]?.ToString(),
                name = item.First["name"]?.ToString()
            })
                .ToList();
        }

        public static List<CurrencyHistory> GetHistoryRange(CurrencyType from, CurrencyType to, string startDate, string endDate, string apiKey = null)
        {
            string url;
            if (string.IsNullOrEmpty(apiKey))
                url = FreeBaseUrl + "convert?q=" + from + "_" + to + "&compact=ultra&date=" + startDate + "&endDate=" + endDate;
            else
                url = PremiumBaseUrl + "convert?q=" + from + "_" + to + "&compact=ultra&date=" + startDate + "&endDate=" + endDate + "&apiKey=" + apiKey;

            var jsonString = GetResponse(url);
            var data = JObject.Parse(jsonString).First.ToArray();
            return (from item in data
                    let obj = (JObject)item
                    from prop in obj.Properties()
                    select new CurrencyHistory
                    {
                        Date = prop.Name,
                        ExchangeRate = item[prop.Name].ToObject<double>()
                    }).ToList();
        }

        public static CurrencyHistory GetHistory(CurrencyType from, CurrencyType to, string date, string apiKey = null)
        {
            string url;
            if (string.IsNullOrEmpty(apiKey))
                url = FreeBaseUrl + "convert?q=" + from + "_" + to + "&compact=ultra&date=" + date;
            else
                url = PremiumBaseUrl + "convert?q=" + from + "_" + to + "&compact=ultra&date=" + date + "&apiKey=" + apiKey;

            var jsonString = GetResponse(url);
            var data = JObject.Parse(jsonString);
            return data.Properties().Select(prop => new CurrencyHistory
            {
                Date = prop.Name,
                ExchangeRate = data[prop.Name][date].ToObject<double>()
            }).FirstOrDefault();
        }

        public static double ExchangeRate(CurrencyType from, CurrencyType to, string apiKey = null)
        {
            string url;
            if (string.IsNullOrEmpty(apiKey))
                url = FreeBaseUrl + "convert?q=" + from + "_" + to + "&compact=y";
            else
                url = PremiumBaseUrl + "convert?q=" + from + "_" + to + "&compact=y&apiKey=" + apiKey;

            var jsonString = GetResponse(url);
            return JObject.Parse(jsonString).First.First["val"].ToObject<double>();
        }

        private static string GetResponse(string url)
        {
            string jsonString;

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (var response = (HttpWebResponse)request.GetResponse())
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                jsonString = reader.ReadToEnd();
            }

            return jsonString;
        }
    }
}
