
using System;
using NUnit.Framework;
using ViolinBtce.Dto.Enums;
using ViolinBtce.Dto.Helpers;

namespace ViolinBtce.Dto.Test
{
    [TestFixture]
    public class ConvertionHelperTest
    {
        #region EnumToString

            #region Currency
            [TestCase(Currency.btc)]
            [TestCase(Currency.eur)]
            [TestCase(Currency.ftc)]
            [TestCase(Currency.ltc)]
            [TestCase(Currency.nmc)]
            [TestCase(Currency.nvc)]
            [TestCase(Currency.ppc)]
            [TestCase(Currency.rur)]
            [TestCase(Currency.trc)]
            [TestCase(Currency.usd)]
            public void ToString_ConvertionOfCurrency(Currency currencyEnum)
            {
                string convertionResult = ConvertionHelper.EnumToString(currencyEnum);

                Assert.AreEqual( currencyEnum.ToString(), convertionResult );
            }
            #endregion

            #region Pair
            [TestCase(Pair.btc_eur)]
            [TestCase(Pair.btc_rur)]
            [TestCase(Pair.btc_usd)]
            [TestCase(Pair.eur_usd)]
            [TestCase(Pair.ftc_btc)]
            [TestCase(Pair.ltc_btc)]
            [TestCase(Pair.ltc_rur)]
            [TestCase(Pair.ltc_usd)]
            [TestCase(Pair.nmc_btc)]
            [TestCase(Pair.nvc_btc)]
            [TestCase(Pair.ppc_btc)]
            [TestCase(Pair.trc_btc)]
            [TestCase(Pair.unknown)]
            public void ToString_ConvertionOfPair(Pair pairEnum)
            {
                string convertionResult = ConvertionHelper.EnumToString(pairEnum);

                Assert.AreEqual(pairEnum.ToString(), convertionResult);
            }
            #endregion

            #region TradeInfoType
            [TestCase(TradeInfoType.ask)]
            [TestCase(TradeInfoType.bid)]
            public void ToString_ConvertionOfTradeInfoType(TradeInfoType tradeInfoTypeEnum)
            {
                string convertionResult = ConvertionHelper.EnumToString(tradeInfoTypeEnum);

                Assert.AreEqual(tradeInfoTypeEnum.ToString(), convertionResult);
            }
            #endregion

            #region TradeType
            [TestCase(TradeType.buy)]
            [TestCase(TradeType.sell)]
            public void ToString_ConvertionOfTradeType(TradeType tradeTypeEnum)
            {
                string convertionResult = ConvertionHelper.EnumToString(tradeTypeEnum);

                Assert.AreEqual(tradeTypeEnum.ToString(), convertionResult);
            }
            #endregion

        #endregion

        #region FromString

            #region Currency
            [TestCase( null,                false, ExpectedException = typeof(ArgumentNullException))]
            [TestCase( "NonExistingValue",  false)]
            [TestCase( "btc",               true)]
            [TestCase( "ltc",               true)]
            [TestCase( "nmc",               true)]
            [TestCase( "nvc",               true)]
            [TestCase( "trc",               true)]
            [TestCase( "ppc",               true)]
            [TestCase( "ftc",               true)]
            [TestCase( "usd",               true)]
            [TestCase( "rur",               true)]
            [TestCase( "eur",               true)]
            [TestCase( "unknown",           true)]
            public void FromString_ConvertToCurrency(string convertableString, bool valueExists)
            {
                bool parsedSuccessfully;
                Currency currency = ConvertionHelper.FromString<Currency>(convertableString, out parsedSuccessfully);

                if (!valueExists)
                {
                    Assert.IsFalse(parsedSuccessfully);
                    Assert.AreEqual(Currency.unknown,currency);
                }
                else
                    Assert.IsTrue(parsedSuccessfully);
            }
            #endregion

            #region Pair
            [TestCase(null,                 false, ExpectedException = typeof(ArgumentNullException))]
            [TestCase("NonExistingValue",   false)]
            [TestCase("unknown",            true)]
            [TestCase("btc_usd",            true)]
            [TestCase("btc_rur",            true)]
            [TestCase("btc_eur",            true)]
            [TestCase("ltc_btc",            true)]
            [TestCase("ltc_usd",            true)]
            [TestCase("ltc_rur",            true)]
            [TestCase("nmc_btc",            true)]
            [TestCase("nvc_btc",            true)]
            [TestCase("usd_rur",            true)]
            [TestCase("eur_usd",            true)]
            [TestCase("trc_btc",            true)]
            [TestCase("ppc_btc",            true)]
            [TestCase("ftc_btc",            true)]
            public void FromString_ConvertToPair(string convertableString, bool valueExists)
            {
                bool parsedSuccessfully;
                Pair currency = ConvertionHelper.FromString<Pair>(convertableString, out parsedSuccessfully);

                if (!valueExists)
                {
                    Assert.IsFalse(parsedSuccessfully);
                    Assert.AreEqual(Pair.unknown, currency);
                }
                else
                    Assert.IsTrue(parsedSuccessfully);
            }
            #endregion

            #region TradeInfoType
            [TestCase(null,                 false, ExpectedException = typeof(ArgumentNullException))]
            [TestCase("NonExistingValue",   false)]
            [TestCase("ask",                true)]
            [TestCase("bid",                true)]
            public void FromString_ConvertToTradeInfoType(string convertableString, bool valueExists)
            {
                bool parsedSuccessfully;
                TradeInfoType currency = ConvertionHelper.FromString<TradeInfoType>(convertableString, out parsedSuccessfully);

                if (!valueExists)
                {
                    Assert.IsFalse(parsedSuccessfully);
                    Assert.AreEqual(TradeInfoType.unknown, currency);
                }
                else
                    Assert.IsTrue(parsedSuccessfully);
            }
            #endregion

            #region TradeType
            [TestCase(null,                 false, ExpectedException = typeof(ArgumentNullException))]
            [TestCase("NonExistingValue",   false)]
            [TestCase("buy",                true)]
            [TestCase("sell",               true)]
            public void FromString_ConvertToTradeType(string convertableString, bool valueExists)
            {
                bool parsedSuccessfully;
                TradeType currency = ConvertionHelper.FromString<TradeType>(convertableString, out parsedSuccessfully);

                if (!valueExists)
                {
                    Assert.IsFalse(parsedSuccessfully);
                    Assert.AreEqual(TradeType.unknown, currency);
                }
                else
                    Assert.IsTrue(parsedSuccessfully);
            }
            #endregion

        #endregion
    }
}
