using System;
using ViolinBtce.Dto;
using ViolinBtce.Dto.Enums;

namespace ViolinBTCE.UsageExample
{
    public class ExampleProgram
    {
        public static void Main(string[] args)
        {
            ViolinBtce violinBtce = new ViolinBtce("key","secret");

            DtoTicker ticker = violinBtce.GetTicker(Pair.eur_usd);

            Console.WriteLine(ticker.ToString());
            Console.ReadLine();
        }
    }
}
