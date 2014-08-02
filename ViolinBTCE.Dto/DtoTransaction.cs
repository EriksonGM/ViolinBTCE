using System;
using ViolinBtce.Dto.Enums;

namespace ViolinBtce.Dto
{
    [Serializable]
    public class DtoTransaction
    {
        public int Type { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public UInt32 Timestamp { get; set; }
    }
}