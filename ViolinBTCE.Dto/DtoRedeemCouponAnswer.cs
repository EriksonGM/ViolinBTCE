using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ViolinBtce.Dto;
using ViolinBtce.Dto.Enums;

namespace ViolinBTCE.Dto
{
    [Serializable]
    public class DtoRedeemCouponAnswer
    {
        [JsonProperty(PropertyName = "couponAmount")]
        public string CouponAmount { get; set; }

        [JsonProperty(PropertyName = "couponCurrency")]
        public Currency CouponCurrency { get; set; }

        [JsonProperty(PropertyName = "transID")]
        public long TransactionID { get; set; }

        public DtoFunds Funds { get; set; }

        public override string ToString()
        {
            return String.Format("Transaction ID: " + TransactionID + "CouponAmount: " + CouponAmount + "CouponCurrency " + CouponCurrency + "\n" + Funds);
        }
    }
}
