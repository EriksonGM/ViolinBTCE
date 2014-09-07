using System;
using Newtonsoft.Json;
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

        [JsonProperty(PropertyName = "timestamp_created")]
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

        public override int GetHashCode()
        {
            return 17 + 31 * Pair.GetHashCode() + 31 * Type.GetHashCode() + 31 * Amount.GetHashCode() +
                        31 * Rate.GetHashCode() + 31 * TimestampCreated.GetHashCode() + 31 * Status.GetHashCode();
        }

        public override string ToString()
        {
            return String.Format("Pair: {0}\n" +
                                 "Type: {1}\n" +
                                 "Amount: {2}\n" +
                                 "Rate: {3}\n" +
                                 "TimeStampCreated: {4}\n" +
                                 "Status: {5}\n",
                                 Pair,
                                 Type,
                                 Amount,
                                 Rate,
                                 TimestampCreated,
                                 Status
                                 );
        }
    }
}