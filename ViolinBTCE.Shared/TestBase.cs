using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ViolinBtce.Dto;
using ViolinBtce.Dto.Enums;

namespace ViolinBtce.Shared
{
    public class TestBase
    {
        protected Dictionary<Type, Object> DummiesDictionary = GetDummiesDictionary();

        public static object DeepCopy(object other)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, other);
                ms.Position = 0;
                return formatter.Deserialize(ms);
            }
        }

        private static Dictionary<Type, object> GetDummiesDictionary()
        {
            #region Funds
            DtoFunds dtoFunds = new DtoFunds { Btc = 1.0m, Eur = 1.0m, Ftc = 1.0m, Ltc = 1.0m, Nmc = 1.0m, Nvc = 1.0m, Ppc = 1.0m, Rur = 1.0m, Trc = 1.0m, Usd = 1.0m };
            #endregion

            #region DtoRights
            DtoRights dtoRights = new DtoRights { Info = true, Trade = true, Withdraw = true };
            #endregion

            #region DtoUserInfo
            DtoUserInfo dtoUserInfo = new DtoUserInfo
            {
                Funds = dtoFunds,
                Rights = dtoRights,
                OpenOrders = 2,
                ServerTime = 123456798
            };
            #endregion

            #region DtoTicker
            DtoTicker dtoTicker = new DtoTicker
            {
                Average = 10m,
                Buy = 9m,
                High = 11m,
                Last = 10.50m,
                Low = 8m,
                Sell = 7m,
                ServerTime = 123456
            };
            #endregion

            #region DtoCancelOrderAnswer
            DtoCancelOrderAnswer dtoCancelOrderAnswer = new DtoCancelOrderAnswer
            {
                Funds = dtoFunds,
                OrderId = 100
            };
            #endregion

            #region DtoOrderInfo
            DtoOrderInfo dtoOrderInfo = new DtoOrderInfo
            {
                Amount = 5,
                Price = 2
            };
            #endregion

            #region DtoDepth
            DtoDepth dtoDepth = new DtoDepth
            {
                Asks = new List<DtoOrderInfo> { dtoOrderInfo, dtoOrderInfo },
                Bids = new List<DtoOrderInfo> { dtoOrderInfo }
            };
            #endregion

            #region DtoOrder
            DtoOrder dtoOrder = new DtoOrder
            {
                Amount = 5,
                Pair = Pair.btc_eur,
                Rate = 100,
                Status = 1,
                TimestampCreated = 123456,
                Type = TradeType.buy
            };
            #endregion

            #region DtoActiveOrders
            DtoActiveOrders dtoActiveOrders = new DtoActiveOrders
            {
                List = new Dictionary<int, DtoOrder> { { 123456789, dtoOrder } }
            };

            DummyDtoActiveOrders dummyDtoActiveOrders = new DummyDtoActiveOrders
            {
                List = null
            };
            #endregion

            #region DtoTrade
            DtoTrade dtoTrade = new DtoTrade
            {
                Amount = 10,
                IsYourOrder = true,
                OrderId = 123456,
                Pair = Pair.eur_usd,
                Rate = 50,
                Timestamp = 11545,
                Type = TradeType.sell
            };
            #endregion

            #region DtoTradeAnswer
            DtoTradeAnswer dtoTradeAnswer = new DtoTradeAnswer
            {
                Funds = dtoFunds,
                OrderId = 123456,
                Received = 10,
                Remains = 10
            };
            #endregion

            #region DtoTradeHistory
            DtoTradeHistory dtoTradeHistory = new DtoTradeHistory
            {
                List = new Dictionary<int, DtoTrade> { { 123456, dtoTrade } }
            };
            #endregion

            #region DtoTradeInfo
            DtoTradeInfo dtoTradeInfo = new DtoTradeInfo
            {
                Amount = 15,
                Date = DateTime.Now,
                Item = Currency.eur,
                Price = 100,
                PriceCurrency = Currency.ltc,
                Tid = 150
            };
            #endregion

            #region DtoTransaction
            DtoTransaction dtoTransaction = new DtoTransaction
            {
                Amount = 10,
                Currency = Currency.eur,
                Description = "description",
                Status = 1,
                Timestamp = 4587,
                Type = 1
            };
            #endregion

            #region DtoTransHistory
            DtoTransHistory dtoTransHistory = new DtoTransHistory
            {
                List = new Dictionary<int, DtoTransaction> { { 132456, dtoTransaction } }
            };
            #endregion

            Dictionary<Type, Object> dummiesDictionary = new Dictionary<Type, object>
            {
                {typeof (DtoFunds), dtoFunds},
                {typeof (DtoRights), dtoRights},
                {typeof (DtoUserInfo), dtoUserInfo},
                {typeof (DtoTicker), dtoTicker},
                {typeof (DtoCancelOrderAnswer), dtoCancelOrderAnswer},
                {typeof (DtoOrderInfo), dtoOrderInfo},
                {typeof (DtoDepth), dtoDepth},
                {typeof (DtoOrder), dtoOrder},
                {typeof (DtoActiveOrders), dtoActiveOrders},
                {typeof (DummyDtoActiveOrders), dummyDtoActiveOrders},
                {typeof (DtoTrade), dtoTrade},
                {typeof (DtoTradeAnswer), dtoTradeAnswer},
                {typeof (DtoTradeHistory), dtoTradeHistory},
                {typeof (DtoTradeInfo), dtoTradeInfo},
                {typeof (DtoTransaction), dtoTransaction},
                {typeof (DtoTransHistory), dtoTransHistory}
            };

            return dummiesDictionary;
        }
    }
}