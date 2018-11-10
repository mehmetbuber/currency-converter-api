using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CurrencyConverter.Enums;
using CurrencyConverter.Models;
using RestSharp;
using Newtonsoft.Json;
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

            string jsonString = GetResponse(url);

            var data = JObject.Parse(jsonString)["results"].ToArray();
            var list = new List<Currency>();
            foreach (var item in data)
            {
                list.Add(new Currency
                {
                    id = item.First["id"]?.ToString(),
                    currencyName = item.First["currencyName"]?.ToString(),
                    currencySymbol = item.First["currencySymbol"]?.ToString()
                });
            }

            return list;
        }

        public static List<Country> GetAllCountries(string apiKey = null)
        {
            string url;
            if (string.IsNullOrEmpty(apiKey))
                url = FreeBaseUrl + "countries";
            else
                url = PremiumBaseUrl + "countries" + "?apiKey=" + apiKey;

            string jsonString = GetResponse(url);

            var data = JObject.Parse(jsonString)["results"].ToArray();
            var list = new List<Country>();
            foreach (var item in data)
            {
                list.Add(new Country
                {
                    id = item.First["id"]?.ToString(),
                    currencyName = item.First["currencyName"]?.ToString(),
                    currencySymbol = item.First["currencySymbol"]?.ToString(),
                    currencyId = item.First["currencySymbol"]?.ToString(),
                    alpha3 = item.First["alpha3"]?.ToString(),
                    name = item.First["name"]?.ToString()
                });
            }

            return list;
        }

        public static List<CurrencyHistory> GetHistoryRange(CurrencyType from, CurrencyType to, string startDate, string endDate, string apiKey = null)
        {
            string url;
            if (string.IsNullOrEmpty(apiKey))
                url = FreeBaseUrl + "convert?q=" + from + "_" + to + "&compact=ultra&date=" + startDate + "&endDate=" + endDate;
            else
                url = PremiumBaseUrl + "convert?q=" + from + "_" + to + "&compact=ultra&date=" + startDate + "&endDate=" + endDate + "&apiKey=" + apiKey;

            string jsonString = GetResponse(url);
            var list = new List<CurrencyHistory>();
            var data = JObject.Parse(jsonString).First.ToArray();
            foreach (var item in data)
            {
                var obj = (JObject)item;
                foreach (JProperty prop in obj.Properties())
                {
                    list.Add(new CurrencyHistory
                    {
                        Date = prop.Name,
                        ExchangeRate = item[prop.Name].ToObject<double>()
                    });
                }
            }
            return list;
        }

        public static CurrencyHistory GetHistory(CurrencyType from, CurrencyType to, string date, string apiKey = null)
        {
            string url;
            if (string.IsNullOrEmpty(apiKey))
                url = FreeBaseUrl + "convert?q=" + from + "_" + to + "&compact=ultra&date=" + date;
            else
                url = PremiumBaseUrl + "convert?q=" + from + "_" + to + "&compact=ultra&date=" + date + "&apiKey=" + apiKey;

            string jsonString = GetResponse(url);
            var data = JObject.Parse(jsonString);
            var obj = (JObject)data;
            foreach (JProperty prop in obj.Properties())
            {
                return new CurrencyHistory
                {
                    Date = prop.Name,
                    ExchangeRate = data[prop.Name][date].ToObject<double>()
                };
            }

            return null;
        }

        public static double ExchangeRate(CurrencyType from, CurrencyType to, string apiKey = null)
        {
            string url;
            if (string.IsNullOrEmpty(apiKey))
                url = FreeBaseUrl + "convert?q=" + from + "_" + to + "&compact=y";
            else
                url = PremiumBaseUrl + "convert?q=" + from + "_" + to + "&compact=y&apiKey=" + apiKey;

            string jsonString = GetResponse(url);
            return JObject.Parse(jsonString).First.First["val"].ToObject<double>();
        }

        private static string GetResponse(string url)
        {
            string jsonString = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                jsonString = reader.ReadToEnd();
            }

            return jsonString;
        }
    }
}
