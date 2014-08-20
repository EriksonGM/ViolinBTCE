using System;
using Newtonsoft.Json;

namespace ViolinBtce.Dto {

    [Serializable]
	public class DtoTradeAnswer {
		public decimal Received { get; set; }
		public decimal Remains { get; set; }

        [JsonProperty(PropertyName = "order_id")]
		public int OrderId { get; set; }

		public DtoFunds Funds { get; set; }

        public override string ToString()
        {
            return String.Format("Received: {0}\n" +
                                 "Remains: {1}\n" +
                                 "OrderId: {2}\n" +
                                 "Funds: {3}",
                                 Received,
                                 Remains,
                                 OrderId,
                                 Funds);
        }
	}
}
