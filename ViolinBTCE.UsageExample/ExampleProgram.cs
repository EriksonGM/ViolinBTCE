using System;
using System.Collections.Generic;
using System.Globalization;
using ViolinBtce.Dto;
using ViolinBtce.Dto.Enums;

namespace ViolinBTCE.UsageExample
{
    public class ExampleProgram
    {
        // This account is changed every time I build on the CI server. After that, it is never used again. =)
        const string validKey = "7DLSZIDY-AXLKDEZO-PSSJZ1OP-UWZHN8BB-STZCI2YS";
        const string validSecret = "53b83c2fe2c788a92134ccf264022860fa5151c3dc8dc0e0f0107e2de0422c99";

        readonly static ViolinBtce violinBtce = new ViolinBtce();

        public static void Main(string[] args)
        {
            violinBtce.SetKeyAndSecret(validKey, validSecret);

            // OR ViolinBtce violinBtce = new ViolinBtce("key","secret");
            
            /* Example of Operations */

            //GetTicker();

            //GetFee();

            //GetBalance();

            //GetUserInfo();

            //int orderId = GetTrade();

            //GetOrderList();

            //CancelOrder(orderId);
        }

        // ReSharper disable UnusedMember.Local
        private static int GetTrade()
        {
            DtoTradeAnswer tradeAnswer = violinBtce.Trade(Pair.ltc_eur, TradeType.sell, 100m, 0.1m);

            Console.WriteLine(tradeAnswer);
            Console.ReadLine();

            return tradeAnswer.OrderId;
        }

        private static void GetUserInfo()
        {
            DtoUserInfo info = violinBtce.GetInfo();

            Console.WriteLine(info);
            Console.ReadLine();
        }

        private static void GetBalance()
        {
            decimal balance = violinBtce.GetBalance(Currency.usd);

            Console.WriteLine(balance);
            Console.ReadLine();
        }

        private static void GetFee()
        {
            decimal fee = violinBtce.GetFee(Pair.eur_usd);

            Console.WriteLine(fee);
            Console.ReadLine();
        }

        private static void GetTicker()
        {
            DtoTicker ticker = violinBtce.GetTicker(Pair.eur_usd);

            Console.WriteLine(ticker.ToString());
            Console.ReadLine();

        }

        private static void GetOrderList()
        {
            DtoActiveOrders orderList = violinBtce.GetOrderList();

            foreach (KeyValuePair<int, DtoOrder> kvp in orderList.List)
            {
                Console.WriteLine("Order Id: " +  kvp.Key);
                Console.WriteLine(kvp.Value.ToString());
                Console.ReadLine();
            }

        }

        private static void CancelOrder(int orderId)
        {
            DtoCancelOrderAnswer dtoCancelOrderAnswer = violinBtce.CancelOrder(orderId);

            Console.WriteLine(dtoCancelOrderAnswer);
            Console.ReadLine();
        }
        // ReSharper restore UnusedMember.Local
    }
}
