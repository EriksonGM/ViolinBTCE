using System;

namespace ViolinBtce.Dto {

    [Serializable]
	public class DtoTradeAnswer {
		public decimal Received { get; set; }
		public decimal Remains { get; set; }
		public int OrderId { get; set; }
		public DtoFunds DtoFunds { get; set; }
	}
}
