using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarrowVale.Common.Utilities
{
    public static class CurrencyUtility
    {
        public static string StandardizeCurrency(int baseCurrencyLeftToCount)
        {
            var currency = new List<string>();
            while (baseCurrencyLeftToCount != 0)
            {
                if (baseCurrencyLeftToCount >= 1000)
                {
                    var platinums = baseCurrencyLeftToCount / 1000;
                    baseCurrencyLeftToCount = baseCurrencyLeftToCount % 1000;
                    currency.Add($"{platinums} Purple Coin");
                }
                else if (baseCurrencyLeftToCount >= 100)
                {
                    var gold = baseCurrencyLeftToCount / 100;
                    baseCurrencyLeftToCount = baseCurrencyLeftToCount % 100;
                    currency.Add($"{gold} Gold");
                }
                else if (baseCurrencyLeftToCount >= 10)
                {
                    var silver = baseCurrencyLeftToCount / 10;
                    baseCurrencyLeftToCount = baseCurrencyLeftToCount % 10;
                    currency.Add($"{silver} Silver");
                }
                else if (baseCurrencyLeftToCount > 0)
                {
                    var bronze = baseCurrencyLeftToCount;
                    currency.Add($"{bronze} Bronze");
                    baseCurrencyLeftToCount = 0;
                }
            }

            return string.Join(',',currency);
        }
    }
}
