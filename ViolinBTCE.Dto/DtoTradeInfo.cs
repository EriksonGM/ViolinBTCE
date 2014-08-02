using System;
using ViolinBtce.Dto.Enums;

namespace ViolinBtce.Dto
{
    [Serializable]
	public class DtoTradeInfo
	{
		public decimal Amount { get; set; }
		public DateTime Date { get; set; }
		public Currency Item { get; set; }
		public decimal Price { get; set; }
		public Currency PriceCurrency { get; set; }
		public UInt32 Tid { get; set; }
		public TradeInfoType Type { get; set; }
	}
}
