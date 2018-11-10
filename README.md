# currency-converter-api
Unofficial https://www.currencyconverterapi.com/ C# Library

## Converter Object
### Without API KEY
Requests goes to https://free.currencyconverterapi.com/api/v6/ base url without api key;
```
Converter converter = new Converter();
```

### With API KEY
Requests goes to https://api.currencyconverterapi.com/api/v6/ base url with api key;
```
Converter converter = new Converter("[YOUR_API_KEY]");
```

## Synchronous Functions
### Basic Conversion
```
var result = converter.Convert(1, CurrencyType.USD, CurrencyType.EUR);
```

### History Single Date
```
var history = converter.GetHistory(CurrencyType.USD, CurrencyType.EUR, "2018-08-01");
```

### History Date Range
```
var historyRange = converter.GetHistoryRange(CurrencyType.USD, CurrencyType.EUR, "2018-08-01", "2018-08-06");
```
### List of Currencies
```
var currencies = converter.GetAllCurrencies();
```

### List of Countries
```
var countries = converter.GetAllCountries();
```

## Asynchronous Functions
### Basic Conversion Async
```
var basic = await converter.ConvertAsync(1, CurrencyType.USD, CurrencyType.EUR);
```

### History Single Date Async
```
var history = await converter.GetHistoryAsync(CurrencyType.USD, CurrencyType.EUR, "2018-08-01");
```

### History Date Range Async
```
var historyRange = await converter.GetHistoryRangeAsync(CurrencyType.USD, CurrencyType.EUR, "2018-08-01", "2018-08-06");
```

### List of Currencies Async
```
var currencies = await converter.GetAllCurrenciesAsync();
```

### List of Countries Async
```
var countries = await converter.GetAllCountriesAsync();
```
