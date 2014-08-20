using System;
using Newtonsoft.Json;

namespace ViolinBtce.Dto
{
    [Serializable]
    public class DtoUserInfo
    {
        public DtoFunds Funds { get; set; }
        public DtoRights Rights { get; set; }

        [JsonProperty(PropertyName = "transaction_count")]
        public int TransactionCount { get; set; }

        [JsonProperty(PropertyName = "open_orders")]
        public int OpenOrders { get; set; }

        [JsonProperty(PropertyName = "server_time")]
        public int ServerTime { get; set; }

        public override string ToString()
        {
            return String.Format("Funds: {0}\n" +
                                 "Rights: {1}\n" +
                                 "Transaction Count: {2}\n" +
                                 "Open Orders: {3}\n" +
                                 "ServerTime: {4}",
                                 Funds,
                                 Rights,
                                 TransactionCount,
                                 OpenOrders,
                                 ServerTime);
        }
    }
}
