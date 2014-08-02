using System;

namespace ViolinBtce.Dto
{
    [Serializable]
	public class DtoTicker
	{
		public decimal Average { get; set; }
		public decimal Buy { get; set; }
		public decimal High { get; set; }
		public decimal Last { get; set; }
		public decimal Low { get; set; }
		public decimal Sell { get; set; }
		public decimal Volume { get; set; }
		public decimal VolumeCurrent { get; set; }
		public UInt32 ServerTime { get; set; }
	}
}
