using System;
using ViolinBtce.Dto.Enums;

namespace ViolinBtce.Dto
{
    [Serializable]
    public class DtoTrade
    {
        public Pair Pair { get; set; }
        public TradeType Type { get; set; }
        public decimal Amount { get; set; }
        public decimal Rate { get; set; }
        public int OrderId { get; set; }
        public bool IsYourOrder { get; set; }
        public UInt32 Timestamp { get; set; }
    }
}