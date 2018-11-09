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
        public static List<Currency> GetAllCurrencies()
        {
            string url = @"https://free.currencyconverterapi.com/api/v6/currencies";
            string jsonString = GetJson(url);

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

        public static List<CurrencyHistory> GetHistoryRange(CurrencyType from, CurrencyType to, string startDate, string endDate)
        {
            string url = @"https://free.currencyconverterapi.com/api/v6/convert?q=" + from + "_" + to + "&compact=ultra&date=" + startDate + "&endDate=" + endDate;
            string jsonString = GetJson(url);
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

        public static CurrencyHistory GetHistory(CurrencyType from, CurrencyType to, string date)
        {
            string url = @"https://free.currencyconverterapi.com/api/v6/convert?q=" + from + "_" + to + "&compact=ultra&date=" + date;
            string jsonString = GetJson(url);
            var data = JObject.Parse(jsonString).First;
            var obj = (JObject)data;
            foreach (JProperty prop in obj.Properties())
            {
                return new CurrencyHistory
                {
                    Date = prop.Name,
                    ExchangeRate = data[prop.Name].ToObject<double>()
                };
            }

            return null;
        }
        
        public static double ExchangeRate(CurrencyType from, CurrencyType to)
        {
            string url = @"https://free.currencyconverterapi.com/api/v6/convert?q=" + from + "_" + to + "&compact=y";
            string jsonString = GetJson(url);
            return JObject.Parse(jsonString).First.First["val"].ToObject<double>();
        }

        private static string GetJson(string url)
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
