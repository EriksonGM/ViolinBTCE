using System;
using System.Collections.Generic;
using ViolinBtce.Dto;
using ViolinBtce.Dto.Enums;
using ViolinBtce.Dto.Helpers;

namespace ViolinBTCE
{
    public class Operations
    {
        protected internal static Dictionary<string, string> GetInfo()
        {
            return new Dictionary<string, string>
            {
                {"method", "getInfo"}
            };
        }

        public static decimal GetBalance(DtoUserInfo userInfo, Currency currency)
        {
            switch (currency)
            {
                // Most used pairs
                case Currency.btc: return userInfo.Funds.Btc;
                case Currency.ltc: return userInfo.Funds.Ltc;
                case Currency.nvc: return userInfo.Funds.Nmc;
                case Currency.eur: return userInfo.Funds.Eur;
                case Currency.usd: return userInfo.Funds.Usd;
                case Currency.gbp: return userInfo.Funds.Gbp;
                case Currency.cnh: return userInfo.Funds.Cnh;

                // Exotic pairs
                case Currency.rur: return userInfo.Funds.Rur;
                case Currency.nmc: return userInfo.Funds.Nmc;
                case Currency.trc: return userInfo.Funds.Trc;
                case Currency.ppc: return userInfo.Funds.Ppc;
                case Currency.ftc: return userInfo.Funds.Ftc;
                case Currency.xpm: return userInfo.Funds.Xpm;
                
                default: throw new ApplicationException("GetBalance(" + currency + ") could not be processed.");
            }
        }

        protected internal static string GetTicker(Pair pair)
        {
            return String.Format("https://btc-e.com/api/2/{0}/ticker", ConvertionHelper.ToString(pair));
        }

        protected internal static string GetFee(Pair pair)
        {
            return string.Format("https://btc-e.com/api/2/{0}/fee", ConvertionHelper.ToString(pair));
        }

        protected internal static Dictionary<string, string> Trade(Pair pair, TradeType type, decimal rate, decimal amount)
        {
            return new Dictionary<string, string>()
            {
                { "method", "Trade" },
                { "pair",   ConvertionHelper.ToString(pair) },
                { "type",   ConvertionHelper.ToString(type) },
                { "rate",   ConvertionHelper.DecimalToString(rate) },
                { "amount", ConvertionHelper.DecimalToString(amount) }
            };
        }


    }
}