# currency-converter-api
https://free.currencyconverterapi.com/ Unofficial C# Library

## Examples
### Converter Object
```
Converter converter = new Converter();
```

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
