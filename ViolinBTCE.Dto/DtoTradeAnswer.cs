using System;

namespace ViolinBtce.Dto {

    [Serializable]
	public class DtoTradeAnswer {
		public decimal Received { get; set; }
		public decimal Remains { get; set; }
		public int Order_Id { get; set; }
		public DtoFunds Funds { get; set; }
	}
}
