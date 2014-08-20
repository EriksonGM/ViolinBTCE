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
            const string validKey       = "GKM3NTU5-2DLZM7DA-TEJE1TY5-VHZO6MN5-AKXDQ5EI";
            const string validSecret    = "8fe6e0f1b5f303c3f8a3465554b1c765116f0e619e611819e2fb97c6f1a5a1c4";

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
