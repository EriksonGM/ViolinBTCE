using System;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using ViolinBtce.Dto;

namespace ViolinBtce.Shared.Test
{
    [TestFixture]
    public class JObjectParserTest : TestBase
    {
        [TestCase(typeof(DtoActiveOrders))]
        [TestCase(typeof(DtoCancelOrderAnswer))]
        [TestCase(typeof(DtoDepth))]
        [TestCase(typeof(DtoFunds))]
        [TestCase(typeof(DtoOrder))]
        [TestCase(typeof(DtoOrderInfo))]
        [TestCase(typeof(DtoRights))]
        [TestCase(typeof(DtoTicker))]
        [TestCase(typeof(DtoTrade))]
        [TestCase(typeof(DtoTradeAnswer))]
        [TestCase(typeof(DtoTradeHistory))]
        [TestCase(typeof(DtoTradeInfo))]
        [TestCase(typeof(DtoTransaction))]
        [TestCase(typeof(DtoTransHistory))]
        [TestCase(typeof(DtoUserInfo))]
        [Test]
        public void ReadFromJObject(Type dtoType)
        {
            // Arrange

            var dtoToBeConverted = DummiesDictionary[dtoType];
           
            var receivedDtoJson = JsonConvert.SerializeObject(dtoToBeConverted);

            var jObject = JObject.Parse(receivedDtoJson);

            // Act
            var dto = JObjectParser.ReadFromJObject(jObject,dtoType);
            
            // Assert
            AssertAllPropertiesAreAsExpected(dtoType, dtoToBeConverted, dto);
        }

        private static void AssertAllPropertiesAreAsExpected(Type dtoType, object dtoToBeConverted, object dto)
        {
            var propertyInfos = dtoType.GetProperties();
            foreach (var propertyInfo in propertyInfos)
            {
                var expectedDtoProperty = propertyInfo.GetValue(dtoToBeConverted);
                var actualDtoProperty = propertyInfo.GetValue(dto);

                if (expectedDtoProperty is IDictionary && actualDtoProperty is IDictionary)
                    AssertDictionariesAreEqual(expectedDtoProperty, actualDtoProperty);
                else
                    Assert.AreEqual(expectedDtoProperty, actualDtoProperty);
            }
        }

        private static void AssertDictionariesAreEqual(object expectedDtoProperty, object actualDtoProperty)
        {
            var expectedDictionary = ((IDictionary) expectedDtoProperty);
            var actualDictionary = ((IDictionary) actualDtoProperty);
            Assert.AreEqual(expectedDictionary.Count, actualDictionary.Count);

            foreach (var key in actualDictionary.Keys)
                Assert.IsTrue(expectedDictionary.Contains(key));
        }
    }
}
