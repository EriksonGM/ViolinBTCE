using System;
using System.Threading;
using NUnit.Framework;
using ViolinBtce.Dto;
using ViolinBtce.Dto.Enums;
using ViolinBTCE.Exceptions;


namespace ViolinBTCE.Test
{
    [TestFixture]
    public class ViolinBtceTest
    {
        private const string ValidKey = "RHO4W3VZ-SHGFQMPV-UZSJFZ8T-NDJXX0L2-QG9QHPOQ";
        private const string ValidSecret = "40903af94d97674a327dff12397e7115f3608102a75d3ec88f768a34a21b4793";
        private const double YourUsdBalance = 4.20811865;
        private const double YourNvcBalance = 0;

        [TearDown]
        public void TearDown()
        {
            // This must be ensured because each operation can only use one nonce a nonce can only be generated every one second 
            // due to its UnixTime nature.
            Thread.Sleep(1000);
        }

        #region SetKeyAndSecret
        [Test]
        public void SetKeyAndSecret()
        {
            // Arrange
            ViolinBtce violinBtce = new ViolinBtce();
            Assert.IsFalse(violinBtce.LoggedOnOperationsAreAllowed);

            // Act
            violinBtce.SetKeyAndSecret(ValidKey, ValidSecret);

            // Assert
            Assert.IsTrue(violinBtce.LoggedOnOperationsAreAllowed);
        }
        #endregion

        #region GetInfo
        // To test this case you must add a valid key / secret pair and have 0 open orders in your account
        [TestCase( ValidKey,    ValidSecret,        true )]
        [TestCase("invalidKey", "invalidSecret",    true,   ExpectedException = typeof(OperationCanceledException))]
        [TestCase("",           "",                 false,  ExpectedException = typeof(NullUserInfoException))]
        public void GetInfo(string key, string secret, bool keyAndSecretAreSpecified)
        {
            // Pre requirements
            SetKeyAndSecretSpecificationRequirement(key, secret, keyAndSecretAreSpecified);

            // Arrange
            ViolinBtce violinBtce = keyAndSecretAreSpecified ? new ViolinBtce(key, secret) : new ViolinBtce();

            // Act
            DtoUserInfo result = violinBtce.GetInfo();

            // Assert
            Assert.AreEqual(0, result.OpenOrders);
        }
        #endregion

        #region GetBalance
        [TestCase(ValidKey,     ValidSecret,     Currency.nvc, YourNvcBalance, true)]
        [TestCase(ValidKey,     ValidSecret,     Currency.usd, YourUsdBalance, true)]
        [TestCase("invalidKey", "invalidSecret", Currency.nvc, YourNvcBalance, true,  ExpectedException = typeof(OperationCanceledException))]
        [TestCase("invalidKey", "invalidSecret", Currency.usd, YourUsdBalance, true,  ExpectedException = typeof(OperationCanceledException))]
        [TestCase("",           "",              Currency.usd, YourUsdBalance, false, ExpectedException = typeof(NullUserInfoException))]
        public void GetBalance(string key, string secret, Currency currency, decimal yourBalance, bool keyAndSecretAreSpecified)
        {
            // Pre requirements
            SetKeyAndSecretSpecificationRequirement(key, secret, keyAndSecretAreSpecified);

            // Arrange
            ViolinBtce violinBtce = keyAndSecretAreSpecified ? new ViolinBtce(key, secret) : new ViolinBtce();

            // Act
            var result = violinBtce.GetBalance(currency);

            // Assert
            Assert.AreEqual(yourBalance, result);
        }
        #endregion

        #region Trade
        [TestCase(ValidKey,     ValidSecret,    Pair.eur_usd, true)]
        [TestCase("",           "",             Pair.eur_usd, false, ExpectedException = typeof(NullUserInfoException))]
        public void Trade(string key, string secret, Pair pair, bool keyAndSecretAreSpecified)
        {
            // Pre requirements
            SetKeyAndSecretSpecificationRequirement(key, secret, keyAndSecretAreSpecified);

            // Arrange
            ViolinBtce violinBtce = keyAndSecretAreSpecified ? new ViolinBtce(key, secret) : new ViolinBtce();

            // Act
            DtoTradeAnswer result = violinBtce.Trade(Pair.ltc_usd, TradeType.sell, 100m, 0.1m);

            // Assert
            Assert.Greater(result.OrderId, 0);
        }
        #endregion

        #region GetTicker
        [TestCase(ValidKey,         ValidSecret,        Pair.eur_usd, true )]
        [TestCase("invalidKey",     "invalidSecret",    Pair.eur_usd, true )]
        [TestCase("",               "",                 Pair.eur_usd, false)]
        public void GetTicker(string key, string secret, Pair pair, bool keyAndSecretAreSpecified)
        {
            // Pre requirements
            SetKeyAndSecretSpecificationRequirement(key, secret, keyAndSecretAreSpecified);

            // Arrange
            ViolinBtce violinBtce = keyAndSecretAreSpecified ? new ViolinBtce(key, secret) : new ViolinBtce();

            // Act
            DtoTicker result = violinBtce.GetTicker(pair);

            // Assert
            Assert.Greater(result.Sell, 0);
            Assert.Greater(result.Buy,  0);
        }
        #endregion

        #region GetFee
        [TestCase(ValidKey, ValidSecret, Pair.eur_usd, true)]
        public void GetFee(string key, string secret, Pair pair, bool keyAndSecretAreSpecified)
        {
            // Pre requirements
            SetKeyAndSecretSpecificationRequirement(key, secret, keyAndSecretAreSpecified);

            // Arrange
            ViolinBtce violinBtce = keyAndSecretAreSpecified ? new ViolinBtce(key, secret) : new ViolinBtce();

            // Act
            decimal result = violinBtce.GetFee(Pair.eur_usd);

            // Assert
            Assert.Greater(result, 0);
        }
        #endregion


        #region Shared private methods
        private static void SetKeyAndSecretSpecificationRequirement(string key, string secret,
            bool keyAndSecretAreSpecified)
        {
            if (keyAndSecretAreSpecified) return;

            Assert.IsNullOrEmpty(key);
            Assert.IsNullOrEmpty(secret);
        }
        #endregion

    }
}
