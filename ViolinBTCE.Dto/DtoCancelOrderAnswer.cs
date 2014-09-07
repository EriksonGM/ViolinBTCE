using System;
using Newtonsoft.Json;

namespace ViolinBtce.Dto
{
    [Serializable]
	public class DtoCancelOrderAnswer
	{
        [JsonProperty(PropertyName = "order_id")]
		public int OrderId { get; set; }
		public DtoFunds Funds { get; set; }

        public override string ToString()
        {
            return String.Format("Order Id: " + OrderId + "\n" + Funds);
        }
	}
}
