using System.Collections.Generic;
using ViolinBtce.Dto;
using ViolinBtce.Dto.Enums;
using ViolinBtce.Shared;

namespace ViolinBTCE
{
    public class ViolinBtce
    {
        readonly WebApi _webApi;
        private DtoUserInfo _userInfo;

        public ViolinBtce(string apiKey, string apiSecret, bool getInfoOnInstantiation )
        {
            _webApi = new WebApi(apiKey,apiSecret);

            if (getInfoOnInstantiation)
                GetInfo();
        }

        public DtoUserInfo GetInfo()
        {
            var getInfoOperation = Operations.GetInfo();
            _userInfo = PerformOperation<DtoUserInfo>(getInfoOperation);
            
            return _userInfo;
        }
        
        public decimal GetBalance(Currency currency)
        {
            _userInfo = GetInfo();

            var balance = Operations.GetBalance(_userInfo, currency);

            return balance;
        }

        public DtoTradeAnswer Trade(Pair pair, TradeType type, decimal rate, decimal amount)
        {
            var tradeOperation = Operations.Trade(pair, type, rate, amount);
            DtoTradeAnswer tradeAnswer = PerformOperation<DtoTradeAnswer>(tradeOperation);

            return tradeAnswer;
        }

        public DtoTicker GetTicker(Pair pair)
        {
            var getTickerOperation = Operations.GetTicker(pair);
            DtoTicker ticker = PerformOperation<DtoTicker>(getTickerOperation);

            return ticker;
        }

        public decimal GetFee(Pair pair)
        {
            var getFeeOperation = Operations.GetFee(pair);
            decimal fee = PerformOperation<decimal>(getFeeOperation, "trade");

            return fee;
        }

        public T PerformOperation<T>(string url, string specialName = null)
        {
            var answerString = WebApi.RequestHttpInformation(url);
            T deserializedObject = WebApi.Deserialize<T>(answerString, specialName);

            return deserializedObject;
        }

        private T PerformOperation<T>(Dictionary<string, string> operations)
        {
            var jsonString = _webApi.GetAnswerAsJsonString(operations, "https://btc-e.com/tapi");
            var deserializedObject = WebApi.Deserialize<T>(jsonString);

            return deserializedObject;
        }
    }
}
