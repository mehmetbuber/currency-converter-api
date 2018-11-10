# currency-converter-api
https://free.currencyconverterapi.com/ Unofficial C# Library

## Converter Object
### Without API KEY
```
Converter converter = new Converter();
```
### With API KEY
```
Converter converter = new Converter("[YOUR_API_KEY]");
```

## Synchronous Functions
### List of Currencies
```
var currencies = converter.GetAllCurrencies();
```

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

## Asynchronous Functions
### List of Currencies Async
```
var currencies = await converter.GetAllCurrenciesAsync();
```

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
