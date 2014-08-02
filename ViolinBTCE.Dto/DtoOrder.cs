using System;
using ViolinBtce.Dto.Enums;

namespace ViolinBtce.Dto
{
    [Serializable]
    public class DtoOrder
    {
        public Pair Pair { get; set; }
        public TradeType Type { get; set; }
        public decimal Amount { get; set; }
        public decimal Rate { get; set; }
        public UInt32 TimestampCreated { get; set; }
        public int Status { get; set; }

        public override bool Equals(object objectBeingTested)
        {
            if (!(objectBeingTested is DtoOrder)) return false;

            DtoOrder castedObject = objectBeingTested as DtoOrder;

            return  Pair                == castedObject.Pair &&
                    Type                == castedObject.Type &&
                    Amount              == castedObject.Amount &&
                    Rate                == castedObject.Rate &&
                    TimestampCreated    == castedObject.TimestampCreated &&
                    Status              == castedObject.Status;
        }
    }
}