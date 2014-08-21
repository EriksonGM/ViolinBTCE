using System;
using NUnit.Framework;
using ViolinBtce.Dto;
using ViolinBtce.Dto.Enums;

namespace ViolinBTCE.Test
{
    [TestFixture]
    public class OperationsTest
    {
        [TestCase(Currency.btc)]
        [TestCase(Currency.ltc)]
        [TestCase(Currency.nvc)]
        [TestCase(Currency.eur)]
        [TestCase(Currency.usd)]
        [TestCase(Currency.gbp)]
        [TestCase(Currency.cnh)]
        [TestCase(Currency.rur)]
        [TestCase(Currency.nmc)]
        [TestCase(Currency.trc)]
        [TestCase(Currency.ppc)]
        [TestCase(Currency.ftc)]
        [TestCase(Currency.xpm)]
        [TestCase(Currency.unknown, ExpectedException = typeof(ApplicationException))]
        public void GetBalance(Currency currency)
        {
            // Arrange
            DtoUserInfo dummyUserInfo = new DtoUserInfo
            {
                Funds = new DtoFunds()
                {
                    Btc = 100,
                    Ltc = 101,
                    Nmc = 102,
                    Nvc = 103,
                    Trc = 104,
                    Ppc = 105,
                    Ftc = 106,
                    Usd = 107,
                    Rur = 108,
                    Eur = 109, 
                    Xpm = 110,
                    Cnh = 111,
                    Gbp = 112
                }
            };
            
            // Act
            decimal result = Operations.GetBalance(dummyUserInfo, currency);

            // Assert

            switch (currency)
            {
                // Most used pairs
                case Currency.btc: Assert.AreEqual(dummyUserInfo.Funds.Btc, result); break;
                case Currency.ltc: Assert.AreEqual(dummyUserInfo.Funds.Ltc, result); break;
                case Currency.nvc: Assert.AreEqual(dummyUserInfo.Funds.Nmc, result); break;
                case Currency.eur: Assert.AreEqual(dummyUserInfo.Funds.Eur, result); break;
                case Currency.usd: Assert.AreEqual(dummyUserInfo.Funds.Usd, result); break;
                case Currency.gbp: Assert.AreEqual(dummyUserInfo.Funds.Gbp, result); break;
                case Currency.cnh: Assert.AreEqual(dummyUserInfo.Funds.Cnh, result); break;

                // Exotic pairs
                case Currency.rur: Assert.AreEqual(dummyUserInfo.Funds.Rur, result); break;
                case Currency.nmc: Assert.AreEqual(dummyUserInfo.Funds.Nmc, result); break;
                case Currency.trc: Assert.AreEqual(dummyUserInfo.Funds.Trc, result); break;
                case Currency.ppc: Assert.AreEqual(dummyUserInfo.Funds.Ppc, result); break;
                case Currency.ftc: Assert.AreEqual(dummyUserInfo.Funds.Ftc, result); break;
                case Currency.xpm: Assert.AreEqual(dummyUserInfo.Funds.Xpm, result); break;
            }
        }
    }
}
