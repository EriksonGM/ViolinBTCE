using System;
using System.Collections.Generic;
using System.Globalization;
using ViolinBtce.Dto;
using ViolinBtce.Dto.Enums;
using ViolinBtce.Dto.Helpers;
using ViolinBtce.Shared;

namespace ViolinBTCE
{
    public class ViolinBtce
    {
        readonly WebApi _webApi;
        private DtoUserInfo _userInfo;

        public ViolinBtce(string apiKey, string apiSecret)
        {
            _webApi = new WebApi(apiKey,apiSecret);
        }

        public DtoUserInfo GetInfo()
        {
            var getInfo = new Dictionary<string, string>
            {
                {"method", "getInfo"}
            };
            _userInfo = PerformOperation<DtoUserInfo>(getInfo);
            return _userInfo;
        }

        public decimal GetBalance(Currency currency)
        {
            switch (currency)
            {
                // Most used pairs
                case Currency.btc: return _userInfo.Funds.Btc;
                case Currency.ltc: return _userInfo.Funds.Ltc;
                case Currency.nvc: return _userInfo.Funds.Nmc;
                case Currency.eur: return _userInfo.Funds.Eur;
                case Currency.usd: return _userInfo.Funds.Usd;
                // Exotic pairs
                case Currency.rur: return _userInfo.Funds.Rur;
                case Currency.nmc: return _userInfo.Funds.Nmc;
                case Currency.trc: return _userInfo.Funds.Trc;
                case Currency.ppc: return _userInfo.Funds.Ppc;
                case Currency.ftc: return _userInfo.Funds.Ftc;

                default: throw new ApplicationException("GetBalance(" + currency + ") could not be processed.");
            }
        }

        public DtoTicker GetTicker(Pair pair)
        {
            string tickerString = string.Format("https://btc-e.com/api/2/{0}/ticker", ConvertionHelper.ToString(pair));
            
            string jsonString = WebApi.RequestHttpInformation(tickerString );

            DtoTicker ticker = WebApi.Deserialize<DtoTicker>(jsonString);
            
            return ticker;
        }

        public decimal GetFee(Pair pair)
        {
            string feeString = string.Format("https://btc-e.com/api/2/{0}/fee", ConvertionHelper.ToString(pair));

            string jsonString = WebApi.RequestHttpInformation(feeString);

            decimal fee = WebApi.Deserialize<decimal>(jsonString, "trade");

            return fee;
        }

        public DtoTradeAnswer Trade(Pair pair, TradeType type, decimal rate, decimal amount)
        {
            var trade = new Dictionary<string, string>()
            {
                { "method", "Trade" },
                { "pair",   ConvertionHelper.ToString(pair) },
                { "type",   ConvertionHelper.ToString(type) },
                { "rate",   DecimalToString(rate) },
                { "amount", DecimalToString(amount) }
            };

            var tradeAnswer = PerformOperation<DtoTradeAnswer>(trade);

            return tradeAnswer;
        }
        
        private static string DecimalToString(decimal d)
        {
            return d.ToString(CultureInfo.InvariantCulture);
        }

        private T PerformOperation<T>(Dictionary<string, string> operations)
        {
            var jsonString = _webApi.GetAnswerAsJsonString(operations, "https://btc-e.com/tapi");
            var deserializedObject = WebApi.Deserialize<T>(jsonString);

            return deserializedObject;
        }
    }
}
