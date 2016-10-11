using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ViolinBtce.Dto;

namespace ViolinBTCE.Dto
{
    [Serializable]
    public class DtoCreateCouponAnswer
    {
        [JsonProperty(PropertyName = "coupon")]
        public string Coupon { get; set; }

        [JsonProperty(PropertyName = "transID")]
        public long TransactionID { get; set; }

        public DtoFunds Funds { get; set; }

        public override string ToString()
        {
            return String.Format("Transaction ID: " + TransactionID + "Coupon: " + Coupon + "\n" + Funds);
        }
    }
}
