using System;

namespace ViolinBtce.Dto
{
    [Serializable]
    public class DtoUserInfo
    {
        public DtoFunds Funds { get; set; }
        public DtoRights Rights { get; set; }
        public int TransactionCount { get; set; }
        public int OpenOrders { get; set; }
        public int ServerTime { get; set; }
    }
}
