using System;
using System.Collections.Generic;
using ViolinBtce.Dto;
using ViolinBtce.Dto.Enums;
using ViolinBTCE.Exceptions;
using ViolinBtce.Shared;

namespace ViolinBTCE
{
    // TODO implement PostSharp "UserInfoRequired" attribute
    public class ViolinBtce
    {
        BTCEWebApi _btceWebApi;
        private DtoUserInfo _userInfo;
        public  bool LoggedOnOperationsAreAllowed { get; private set; }

        /// <summary>
        /// Creates an instance of the API and sets the key and secret to define the account to be used for further operations.
        /// </summary>
        /// <param name="apiKey">The key that the Market Place Website (eg.: BTC-e) issues to you.</param>
        /// <param name="apiSecret">The secret that the Market Place Website (eg.: BTC-e) issues to you.</param>
        /// <exception cref="ArgumentNullException">Thrown when API key and/or secret are null or empty.</exception>
        public ViolinBtce(string apiKey, string apiSecret)
        {
            _btceWebApi = new BTCEWebApi(apiKey,apiSecret);
            LoggedOnOperationsAreAllowed = true;
        }

        /// <summary>
        /// Creates an instance of the API without setting key and secret. Only operations which do not require these properties are allowed.
        /// </summary>
        public ViolinBtce()
        {
            _btceWebApi = new BTCEWebApi("dummyKey", "dummySecret");
            LoggedOnOperationsAreAllowed = false;
        }

        /// <summary>
        /// Sets the key and secret to define the account to be used for further operations.
        /// </summary>
        /// <param name="apiKey">The key that the Market Place Website (eg.: BTC-e) issues to you.</param>
        /// <param name="apiSecret">The secret that the Market Place Website (eg.: BTC-e) issues to you.</param>
        /// <exception cref="ArgumentNullException">Thrown when API key and/or secret are null or empty.</exception>
        public void SetKeyAndSecret(string apiKey, string apiSecret)
        {
            _btceWebApi = new BTCEWebApi(apiKey, apiSecret);

            LoggedOnOperationsAreAllowed = true;
        }

        /// <summary>
        /// Gets a DtoUserInfo object of the owner of the key and secret passed on the constructor
        /// </summary>
        /// <returns>DtoUserInfo</returns>
        /// <exception cref="OperationCanceledException">Thrown when requesting information from server fails.</exception>
        /// <exception cref="NullUserInfoException">Thrown when API key and/or secret are undefined.</exception>
        public DtoUserInfo GetInfo()
        {
            if(!LoggedOnOperationsAreAllowed)
                throw new NullUserInfoException();

            var getInfoOperation = Operations.GetInfo();
            _userInfo = PerformOperation<DtoUserInfo>(getInfoOperation);
            
            return _userInfo;
        }
        
        /// <summary>
        /// Gets a decimal value corresponding to the balance in the specified currency
        /// </summary>
        /// <param name="currency">A Currency enumeration value</param>
        /// <exception cref="OperationCanceledException">Thrown when requesting information from server fails.</exception>
        /// <exception cref="NullUserInfoException">Thrown when API key and/or secret are undefined.</exception>
        public decimal GetBalance(Currency currency)
        {
            if (!LoggedOnOperationsAreAllowed)
                throw new NullUserInfoException();

            _userInfo = GetInfo();

            var balance = Operations.GetBalance(_userInfo, currency);

            return balance;
        }

        /// <summary>
        /// Trades specified pair if funds are enough to perform the operation.
        /// </summary>
        /// <param name="pair">Pair to trade.</param>
        /// <param name="type">Type of operation.</param>
        /// <param name="rate">Rate to transact for.</param>
        /// <param name="amount">Amount of units of pair to buy for the specified rate</param>
        /// <returns></returns>
        public DtoTradeAnswer Trade(Pair pair, TradeType type, decimal rate, decimal amount)
        {
            if (!LoggedOnOperationsAreAllowed)
                throw new NullUserInfoException();

            var tradeOperation = Operations.Trade(pair, type, rate, amount);
            DtoTradeAnswer tradeAnswer = PerformOperation<DtoTradeAnswer>(tradeOperation);

            return tradeAnswer;
        }

        public DtoCancelOrderAnswer CancelOrder(int orderId)
        {
            if (!LoggedOnOperationsAreAllowed)
                throw new NullUserInfoException();

            var cancelOrderOperation = Operations.CancelOrder(orderId);
            
            DtoCancelOrderAnswer cancelOrderAnswer = PerformOperation<DtoCancelOrderAnswer>(cancelOrderOperation);

            return cancelOrderAnswer;
        }

        public DtoActiveOrders GetOrderList()
        {
            var getOrderListOperation = Operations.GetOrderList();

            var dtoActiveOrders = new DtoActiveOrders
            {
                List = PerformOperation<Dictionary<int, DtoOrder>>(getOrderListOperation)
            };
            
            return dtoActiveOrders;
        }

        
        /// <summary>
        /// Gets a DtoTicker object containing information about the current market prices of the given pair
        /// </summary>
        /// <param name="pair">Pair to get the ticker for.</param>
        /// <exception cref="OperationCanceledException">Thrown when requesting information from server fails.</exception>
        public DtoTicker GetTicker(Pair pair)
        {
            var getTickerOperation = Operations.GetTicker(pair);
            DtoTicker ticker = PerformOperation<DtoTicker>(getTickerOperation, "ticker");

            return ticker;
        }

        /// <summary>
        /// Gets a decimal value corresponding to the percentual fee due to trading the specified pair. Eg.: 0.2 means 0.2%
        /// </summary>
        /// <param name="pair">Pair to get the fee for.</param>
        /// <exception cref="OperationCanceledException">Thrown when requesting information from server fails.</exception>
        public decimal GetFee(Pair pair)
        {
            var getFeeOperation = Operations.GetFee(pair);
            decimal fee = PerformOperation<decimal>(getFeeOperation, "trade");

            return fee;
        }

        private T PerformOperation<T>(string url, string specialName = null)
        {
            var answerString = _btceWebApi.RequestHttpInformation(url);
            T deserializedObject = _btceWebApi.Deserialize<T>(answerString, specialName);

            return deserializedObject;
        }

        private T PerformOperation<T>(Dictionary<string, string> operations)
        {
            var jsonString = _btceWebApi.GetAnswerAsJsonString(operations, "https://btc-e.com/tapi");
            var deserializedObject = _btceWebApi.Deserialize<T>(jsonString);

            return deserializedObject;
        }
    }
}
