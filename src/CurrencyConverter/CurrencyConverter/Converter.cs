using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CurrencyConverter.Enums;
using CurrencyConverter.Helpers;
using CurrencyConverter.Models;

namespace CurrencyConverter
{
    public class Converter
    {
        private string _apiKey { get; }

        public Converter()
        {
        }

        public Converter(string apiKey)
        {
            _apiKey = apiKey;
        }

        public double Convert(double amount, CurrencyType from, CurrencyType to)
        {
            return RequestHelper.ExchangeRate(from, to, _apiKey) * amount;
        }

        public async Task<double> ConvertAsync(double amount, CurrencyType from, CurrencyType to)
        {
            return await Task.Run(() => Convert(amount, from, to));
        }


        public List<Currency> GetAllCurrencies()
        {
            return RequestHelper.GetAllCurrencies(_apiKey);
        }

        public async Task<List<Currency>> GetAllCurrenciesAsync()
        {
            return await Task.Run(() => GetAllCurrencies());
        }


        public List<Country> GetAllCountries()
        {
            return RequestHelper.GetAllCountries(_apiKey);
        }

        public async Task<List<Country>> GetAllCountriesAsync()
        {
            return await Task.Run(() => GetAllCountries());
        }


        public CurrencyHistory GetHistory(CurrencyType from, CurrencyType to, DateTime date)
        {
            return RequestHelper.GetHistory(from, to, date.ToString("yyyy-MM-dd"), _apiKey);
        }

        public async Task<CurrencyHistory> GetHistoryAsync(CurrencyType from, CurrencyType to, DateTime date)
        {
            return await Task.Run(() => GetHistory(from, to, date.ToString("yyyy-MM-dd")));
        }


        public CurrencyHistory GetHistory(CurrencyType from, CurrencyType to, string date)
        {
            return RequestHelper.GetHistory(from, to, date, _apiKey);
        }

        public async Task<CurrencyHistory> GetHistoryAsync(CurrencyType from, CurrencyType to, string date)
        {
            return await Task.Run(() => GetHistory(from, to, date));
        }


        public List<CurrencyHistory> GetHistoryRange(CurrencyType from, CurrencyType to, string startDate, string endDate)
        {
            return RequestHelper.GetHistoryRange(from, to, startDate, endDate, _apiKey);
        }

        public async Task<List<CurrencyHistory>> GetHistoryRangeAsync(CurrencyType from, CurrencyType to, string startDate, string endDate)
        {
            return await Task.Run(() => GetHistoryRange(from, to, startDate, endDate));
        }


        public List<CurrencyHistory> GetHistoryRange(CurrencyType from, CurrencyType to, DateTime startDate, DateTime endDate)
        {
            return GetHistoryRange(from, to, startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"));
        }

        public async Task<List<CurrencyHistory>> GetHistoryRangeAsync(CurrencyType from, CurrencyType to, DateTime startDate, DateTime endDate)
        {
            return await Task.Run(() => GetHistoryRange(from, to, startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd")));
        }

    }
}
