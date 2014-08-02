using System;

namespace ViolinBtce.Dto
{
    [Serializable]
    public class DtoOrderInfo
    {
        public decimal Price { get; set; }
        public decimal Amount { get; set; }

        public override bool Equals(object objectBeingTested)
        {
            if (!(objectBeingTested is DtoOrderInfo)) return false;

            DtoOrderInfo castedObject = objectBeingTested as DtoOrderInfo;

            return  Price == castedObject.Price &&
                    Amount == castedObject.Amount;
        }
    }
}