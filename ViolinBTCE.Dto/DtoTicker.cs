using System;
using Newtonsoft.Json;

namespace ViolinBtce.Dto
{
    [Serializable]
	public class DtoTicker
	{
        [JsonProperty(PropertyName = "avg")]
		public decimal Average { get; set; }
		public decimal Buy { get; set; }
		public decimal High { get; set; }
		public decimal Last { get; set; }
		public decimal Low { get; set; }
		public decimal Sell { get; set; }

        [JsonProperty(PropertyName = "vol")]
        public decimal Volume { get; set; }

        [JsonProperty(PropertyName = "vol_cur")]
		public decimal VolumeCurrent { get; set; }

        [JsonProperty(PropertyName = "server_time")]
        public UInt32 ServerTime { get; set; }

        public UInt32 Updated { get; set; }

        public override string ToString()
        {
            return String.Format("Buy: {0}\n" +
                                 "Sell:{1}\n" +
                                 "Low: {2}\n" +
                                 "Average: {3}\n" +
                                 "High: {4}\n" +
                                 "Last: {5}\n" +
                                 "Volume: {6}\n" +
                                 "Current Volume: {7}\n" +
                                 "Server Time: {8}\n" +
                                 "Updated: {9}",
                                 Buy,
                                 Sell,
                                 Low,
                                 Average,
                                 High,
                                 Last,
                                 Volume,
                                 VolumeCurrent,
                                 ServerTime,
                                 Updated);
        }
	}
}
