using System;
using ViolinBtce.Dto;
using ViolinBtce.Dto.Enums;

namespace ViolinBTCE.UsageExample
{
    public class ExampleProgram
    {
        public static void Main(string[] args)
        {
            // This account is changed every time I build on the CI server. After that, it is never used again. =)
            const string validKey       = "RHO4W3VZ-SHGFQMPV-UZSJFZ8T-NDJXX0L2-QG9QHPOQ";
            const string validSecret    = "40903af94d97674a327dff12397e7115f3608102a75d3ec88f768a34a21b4793";

            ViolinBtce violinBtce = new ViolinBtce();
            violinBtce.SetKeyAndSecret(validKey, validSecret);
            // OR ViolinBtce violinBtce = new ViolinBtce("key","secret");
            
            /* Example of Operations */

            //GetTicker(violinBtce);

            //GetFee(violinBtce);

            //GetBalance(violinBtce);

            //GetUserInfo(violinBtce);

            //GetTrade(violinBtce);
        }

        // ReSharper disable UnusedMember.Local
        private static void GetTrade(ViolinBtce violinBtce)
        {
            DtoTradeAnswer tradeAnswer = violinBtce.Trade(Pair.ltc_eur, TradeType.sell, 100m, 0.1m);

            Console.WriteLine(tradeAnswer);
            Console.ReadLine();
        }

        private static void GetUserInfo(ViolinBtce violinBtce)
        {
            DtoUserInfo info = violinBtce.GetInfo();

            Console.WriteLine(info);
            Console.ReadLine();
        }

        private static void GetBalance(ViolinBtce violinBtce)
        {
            decimal balance = violinBtce.GetBalance(Currency.usd);

            Console.WriteLine(balance);
            Console.ReadLine();
        }

        private static void GetFee(ViolinBtce violinBtce)
        {
            decimal fee = violinBtce.GetFee(Pair.eur_usd);

            Console.WriteLine(fee);
            Console.ReadLine();
        }

        public static void GetTicker(ViolinBtce violinBtce)
        {
            DtoTicker ticker = violinBtce.GetTicker(Pair.eur_usd);

            Console.WriteLine(ticker.ToString());
            Console.ReadLine();

        }
        // ReSharper restore UnusedMember.Local
    }
}
