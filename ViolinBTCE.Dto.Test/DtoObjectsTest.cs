using System;
using NUnit.Framework;
using ViolinBtce.Shared;

namespace ViolinBtce.Dto.Test
{
    [TestFixture]
    public class DtoObjectsTest : TestBase
    {

        [TestCase(typeof(DtoActiveOrders),      typeof(DtoActiveOrders),        true  )]
        [TestCase(typeof(DummyDtoActiveOrders), typeof(DummyDtoActiveOrders),   true  )]
        [TestCase(typeof(DtoActiveOrders),      typeof(DtoActiveOrders),        false )]
        [TestCase(typeof(DtoActiveOrders),      typeof(DtoDepth),               false )]

        [TestCase( typeof(DtoOrderInfo),        typeof(DtoOrderInfo),           true  )]
        [TestCase( typeof(DtoOrderInfo),        typeof(DtoOrderInfo),           false )]
        [TestCase( typeof(DtoOrderInfo),        typeof(DtoDepth),               false )]

        [TestCase( typeof(DtoRights),           typeof(DtoRights),              true  )]
        [TestCase( typeof(DtoRights),           typeof(DtoRights),              false )]
        [TestCase( typeof(DtoRights),           typeof(DtoDepth),               false )]

        [TestCase( typeof(DtoFunds),            typeof(DtoFunds),               true  )]
        [TestCase( typeof(DtoFunds),            typeof(DtoFunds),               false )]
        [TestCase( typeof(DtoFunds),            typeof(DtoDepth),               false )]

        [TestCase(typeof(DtoOrder),             typeof(DtoOrder),               true  )]
        [TestCase(typeof(DtoOrder),             typeof(DtoOrder),               false )]
        [TestCase(typeof(DtoOrder),             typeof(DtoDepth),               false)]
        public void Equals(Type firstDtoType, Type secondDtoType, bool sameContent)
        {
            var dto1 = DummiesDictionary[firstDtoType];

            if (firstDtoType == secondDtoType)
            {
                if (sameContent)
                {
                    var dto2 = DeepCopy(dto1);
                    Assert.AreEqual(dto1, dto2);
                }    
                else
                {
                    var dto2 = Activator.CreateInstance(secondDtoType);
                    Assert.AreNotEqual(dto1, dto2);
                }
            }
            else
            {
                Assert.IsFalse(sameContent, "Different DTOs do not have the same content");
                
                var dto2 = Activator.CreateInstance(secondDtoType);
                Assert.AreNotEqual(dto1, dto2);
            }
        }
    }
}
